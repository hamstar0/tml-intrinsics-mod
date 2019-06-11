using Intrinsics.Libraries.Helpers.Items;
using Intrinsics.NetProtocols;
using System;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics {
	partial class IntrinsicsPlayer : ModPlayer {
		private void LoadIntrinsicItem( int itemId ) {
			var item = new Item();
			item.SetDefaults( itemId );

			switch( IntrinsicsLogic.GetItemIntrinsicType( item ) ) {
			case IntrinsicType.Armor:
				this.IntrinsicArmItem[ itemId ] = item;
				break;
			case IntrinsicType.Accessory:
				this.IntrinsicAccItem[itemId] = item;
				break;
			case IntrinsicType.Buff:
				this.IntrinsicBuffItem[itemId] = item;
				break;
			}
		}
		
		/////

		public void ApplyIntrinsic( string itemUid ) {
			this.IntrinsicItemUids.Add( itemUid );
			
			int itemId;
			if( ItemIdentityHelpers.TryGetTypeByUid( itemUid, out itemId ) ) {
				this.LoadIntrinsicItem( itemId );
			}

			if( Main.netMode == 1 ) {
				IntrinsicsSyncProtocol.SyncFromMe();
			}
		}


		////////////////

		private void UpdateIntrinsicBuffs() {
			foreach( Item item in this.IntrinsicBuffItem.Values ) {
				int buffIdx = this.player.FindBuffIndex( item.buffType );
				if( buffIdx == -1 ) {
					this.player.AddBuff( item.buffType, 3 );
				} else {
					this.player.buffTime[ buffIdx ] = 3;
				}
			}
		}

		private void UpdateIntrinsicEquips() {
			bool _ = false;
			foreach( Item item in this.IntrinsicAccItem.Values ) {
				this.player.VanillaUpdateAccessory( this.player.whoAmI, item, false, ref _, ref _, ref _ );
			}
			foreach( Item item in this.IntrinsicArmItem.Values ) {
				this.player.VanillaUpdateEquip( item );
			}
			//VanillaUpdateInventory( this.inventory[j] );
		}
	}
}
