using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.DebugHelpers;
using Intrinsics.Items;
using Intrinsics.NetProtocols;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics {
	class IntrinsicsPlayer : ModPlayer {
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

		public ISet<string> IntrinsicItemUids = new HashSet<string>();

		private Item PrevSelectedItem = null;



		////////////////

		public override bool CloneNewInstances => false;



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
			PacketProtocolRequestToServer.QuickRequest<ModSettingsProtocol>( -1 );
		}

		private void OnConnectServer() {
		}

		
		////////////////

		public override void PostUpdate() {
			Player plr = this.player;
			if( plr.whoAmI != Main.myPlayer ) { return; }

			if( Main.mouseItem != null && !Main.mouseItem.IsAir ) {
				if( plr.HeldItem.type == this.mod.ItemType<BlankContractItem>() ) {
					if( this.PrevSelectedItem != null ) {
						this.AttemptBlankContractAddCurrentItem( plr );
						this.PrevSelectedItem = null;
					}
				} else {
					this.PrevSelectedItem = plr.HeldItem;
				}
			} else {
				this.PrevSelectedItem = null;
			}
		}


		////////////////

		/*public override void PreUpdate() {
			Player plr = this.player;
			if( plr.whoAmI != Main.myPlayer ) { return; }
			if( plr.dead ) { return; }

			var mymod = (IntrinsicsMod)this.mod;
		}*/

		public override void UpdateEquips( ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff ) {
			//if( this.TestItem != null ) {
			//	bool _ = false;
			//	//VanillaUpdateInventory( this.inventory[j] );
			//	this.player.VanillaUpdateEquip( this.TestItem );
			//	this.player.VanillaUpdateAccessory( this.player.whoAmI, this.TestItem, false, ref _, ref _, ref _ );
			//}
		}
	}
}
