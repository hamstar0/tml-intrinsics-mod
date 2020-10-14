using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using Intrinsics.Items;
using Intrinsics.NetProtocols;
using Intrinsics.NPCs;


namespace Intrinsics {
	partial class IntrinsicsPlayer : ModPlayer {
		private static bool AttemptBlankContractAddCurrentItem( Player player ) {
			var contractItem = Main.mouseItem.modItem as BlankContractItem;
			if( contractItem == null ) {
				if( IntrinsicsConfig.Instance.DebugModeInfo ) {
					Main.NewText("Swapped item not a BlankContractItem");
				}
				return false;
			}

			if( contractItem.MyLastInventoryPosition == -1 ) {
				if( IntrinsicsConfig.Instance.DebugModeInfo ) {
					Main.NewText( "BlankContractItem does not know its last inventory position" );
				}
				return false;
			}
			Item item = player.inventory[contractItem.MyLastInventoryPosition];
			if( item?.active != true ) {
				if( IntrinsicsConfig.Instance.DebugModeInfo ) {
					Main.NewText( "BlankContractItem reports it is not swapping with an item" );
				}
				return false;
			}

			bool isAdded = false;

			if( contractItem.CanAddItem( item ) ) {
				bool hasMadeContract = contractItem.CreateImpartmentContract( player, item );

				if( IntrinsicsConfig.Instance.DebugModeInfo ) {
					Main.NewText( "Impartment contract created? "+hasMadeContract );
				}

				if( hasMadeContract ) {
					player.inventory[ contractItem.MyLastInventoryPosition ] = new Item();
				} else {
					player.inventory[ contractItem.MyLastInventoryPosition ] = Main.mouseItem;
				}

				Main.mouseItem = new Item();

				isAdded = true;
			}

			return isAdded;
		}



		////////////////

		internal ISet<string> IntrinsicItemUids = new HashSet<string>();

		internal IDictionary<int, Item> IntrinsicArmItem = new Dictionary<int, Item>();
		internal IDictionary<int, Item> IntrinsicAccItem = new Dictionary<int, Item>();
		internal IDictionary<int, Item> IntrinsicBuffItem = new Dictionary<int, Item>();

		internal IDictionary<int, bool> IntrinsicToggle = new Dictionary<int, bool>();

		private Item PrevSelectedItem = null;



		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey("item_count") ) {
				return;
			}

			int count = tag.GetInt( "item_count" );

			this.ClearIntrinsics( false );
			this.IntrinsicToggle.Clear();

			for( int i=0; i<count; i++ ) {
				string itemUid = tag.GetString( "item_" + i );
				bool isEnabled = true;

				if( tag.ContainsKey("item_"+i+"_on") ) {
					isEnabled = tag.GetBool( "item_" + i + "_on" );
				}

				try {
					this.ApplyIntrinsic( itemUid, isEnabled );
				} catch( Exception e ) {
					LogHelpers.Warn( "Error applying intrinsic for "+this.player.name+" ("+this.player.whoAmI+")"
							+" for item "+itemUid+" (on? "+isEnabled+") - "+e.ToString() );
				}
			}
		}

		public override TagCompound Save() {
			int count = this.IntrinsicItemUids.Count;
			var tag = new TagCompound { { "item_count", count } };

			int i = 0;
			foreach( string itemUid in this.IntrinsicItemUids ) {
				tag[ "item_" + i ] = itemUid;
				tag[ "item_" + i + "_on" ] = this.IntrinsicToggle[ ItemID.TypeFromUniqueKey(itemUid) ] ;
				i++;
			}

			return tag;
		}


		////////////////

		public override void clientClone( ModPlayer clientClone ) {
			var myclone = (IntrinsicsPlayer)clientClone;
			myclone.IntrinsicItemUids = new HashSet<string>( this.IntrinsicItemUids );
			myclone.IntrinsicArmItem = new Dictionary<int, Item>( this.IntrinsicArmItem );
			myclone.IntrinsicAccItem = new Dictionary<int, Item>( this.IntrinsicAccItem );
			myclone.IntrinsicBuffItem = new Dictionary<int, Item>( this.IntrinsicBuffItem );
			myclone.IntrinsicToggle = new Dictionary<int, bool>( this.IntrinsicToggle );
			myclone.PrevSelectedItem = this.PrevSelectedItem?.Clone();
		}

		public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) {
			if( Main.netMode == 1 ) {
				if( newPlayer ) {
					IntrinsicsSyncProtocol.SyncFromMe();
					IntrinsicsSyncProtocol.SyncToMe();
				}
			} else if( Main.netMode == 2 ) {
				if( toWho == -1 && fromWho == this.player.whoAmI ) {
					this.OnConnectServer();
				}
			}
		}

		//public override void SendClientChanges( ModPlayer clientPlayer ) {
		//	var myClientPlayer = (IntrinsicsPlayer)clientPlayer;
		//
		//	if( this.IntrinsicItemUids.Count != myClientPlayer.IntrinsicItemUids.Count ) {
		//		IntrinsicsSyncProtocol.SyncFromMe();
		//	}
		//}


		////////////////

		public override void OnEnterWorld( Player player ) {
			if( player.whoAmI != Main.myPlayer ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			var mymod = (IntrinsicsMod)this.mod;

			if( Main.netMode == 0 ) {
				this.OnConnectSingle();
			} else if( Main.netMode == 1 ) {
				this.OnConnectClient();
			}

			//this.TestItem = new Item();
			//this.TestItem.SetDefaults( ItemID.WingsSolar );
		}


		////////////////

		private void OnConnectSingle() {
		}

		private void OnConnectClient() {
			IntrinsicsSyncProtocol.SyncFromMe();
		}

		private void OnConnectServer() {
		}


		////////////////

		public override void PreUpdate() {
			var mymod = (IntrinsicsMod)this.mod;
			Player plr = this.player;
			//if( plr.whoAmI != Main.myPlayer ) { return; }
			//if( plr.dead ) { return; }

			if( plr.whoAmI == Main.myPlayer ) {
				WanderingGhostNPC.UpdateTradingState();
			}

			this.UpdateIntrinsicBuffs();
		}

		public override void PostUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			this.UpdateContractInteractions();
		}

		public override void UpdateAutopause() {
			if( !Main.gamePaused ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }
			this.UpdateContractInteractions();
		}


		public override void UpdateEquips( ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff ) {
			this.UpdateIntrinsicEquips();
		}


		////////////////

		private void UpdateContractInteractions() {
			if( Main.mouseItem == null || Main.mouseItem.IsAir ) {
				this.PrevSelectedItem = null;
				return;
			}

//DebugHelpers.Print("uci", "mouse:"+Main.mouseItem.HoverName+", held:"+plr.HeldItem?.HoverName+", PrevSelectedItem:"+this.PrevSelectedItem?.HoverName);
			if( Main.mouseItem.type == ModContent.ItemType<BlankContractItem>() ) {
				if( this.PrevSelectedItem != null ) {
					IntrinsicsPlayer.AttemptBlankContractAddCurrentItem( this.player );
					this.PrevSelectedItem = null;
				}
			} else {
				this.PrevSelectedItem = Main.mouseItem;
			}
		}


		////////////////

		public override void ProcessTriggers( TriggersSet triggersSet ) {
			if( IntrinsicsMod.Instance.ControlPanelHotkey.JustPressed ) {
				IntrinsicsMod.Instance.ControlPanelDialog.Open();
			}
		}
	}
}
