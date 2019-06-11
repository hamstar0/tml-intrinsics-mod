using HamstarHelpers.Helpers.DebugHelpers;
using Intrinsics.Items;
using Intrinsics.NetProtocols;
using Intrinsics.NPCs;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace Intrinsics {
	partial class IntrinsicsPlayer : ModPlayer {
		private static bool AttemptBlankContractAddCurrentItem( Player player ) {
			var contractItem = Main.mouseItem.modItem as BlankContractItem;
			if( contractItem == null ) {
				return false;
			}

			if( contractItem.MyLastInventoryPosition == -1 ) {
				return false;
			}
			Item item = player.inventory[contractItem.MyLastInventoryPosition];
			if( item == null ) {
				return false;
			}

			bool isAdded = false;

			if( contractItem.CanAddItem( item ) ) {
				if( contractItem.AddItem( player, item ) ) {
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

		private Item PrevSelectedItem = null;



		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey("item_count") ) {
				return;
			}

			int count = tag.GetInt( "item_count" );

			for( int i=0; i<count; i++ ) {
				string itemUid = tag.GetString( "item_" + i );
				this.ApplyIntrinsic( itemUid );
			}
		}

		public override TagCompound Save() {
			int count = this.IntrinsicItemUids.Count;
			var tag = new TagCompound { { "item_count", count } };

			int i = 0;
			foreach( string itemUid in this.IntrinsicItemUids ) {
				tag[ "item_" + i ] = itemUid;
				i++;
			}

			return tag;
		}


		////////////////

		public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) {
			if( Main.netMode == 1 ) {
				if( newPlayer ) {
					IntrinsicsSyncProtocol.SyncToMe();
				}
			} else if( Main.netMode == 2 ) {
				if( toWho == -1 && fromWho == this.player.whoAmI ) {
					this.OnConnectServer();
				}
			}
		}

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
			var mymod = (IntrinsicsMod)this.mod;

			if( !mymod.ConfigJson.LoadFile() ) {
				mymod.ConfigJson.SaveFile();
			}
		}

		private void OnConnectClient() {
			IntrinsicsSyncProtocol.SyncFromMe();
			ModSettingsProtocol.QuickRequest();
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
			Player plr = this.player;
			if( plr.whoAmI != Main.myPlayer ) { return; }

			if( Main.mouseItem != null && !Main.mouseItem.IsAir ) {
				if( plr.HeldItem.type == this.mod.ItemType<BlankContractItem>() ) {
					if( this.PrevSelectedItem != null ) {
						IntrinsicsPlayer.AttemptBlankContractAddCurrentItem( plr );
						this.PrevSelectedItem = null;
					}
				} else {
					this.PrevSelectedItem = plr.HeldItem;
				}
			} else {
				this.PrevSelectedItem = null;
			}
		}

		public override void UpdateEquips( ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff ) {
			this.UpdateIntrinsicEquips();
		}
	}
}
