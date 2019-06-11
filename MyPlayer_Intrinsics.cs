using HamstarHelpers.Helpers.ItemHelpers;
using Intrinsics.NetProtocols;
using System;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics {
	partial class IntrinsicsPlayer : ModPlayer {
		private Item LoadIntrinsicItem( int itemId ) {
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

			return item;
		}
		
		/////

		public void ApplyIntrinsic( string itemUid ) {
			this.IntrinsicItemUids.Add( itemUid );
			
			int itemId;
			if( Libraries.Helpers.Items.ItemIdentityHelpers.TryGetTypeByUid( itemUid, out itemId ) ) {
				Item item = this.LoadIntrinsicItem( itemId );
				string colorHex = ItemAttributeHelpers.RarityColor[ item.rare ].Hex3();

				Main.NewText( "The deal is made. Imparting [c/" + colorHex + ":"+item.HoverName+"]..." );
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
