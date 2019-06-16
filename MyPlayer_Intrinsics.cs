using HamstarHelpers.Components.DataStructures;
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
			
			this.IntrinsicToggle[ itemId ] = true;

			return item;
		}

		/////

		public void ApplyIntrinsic( string itemUid ) {
			this.IntrinsicItemUids.Add( itemUid );

			int itemId;
			if( Libraries.Helpers.Items.ItemIdentityHelpers.TryGetTypeByUid( itemUid, out itemId ) ) {
				Item item = this.LoadIntrinsicItem( itemId );
				string colorHex = ItemAttributeHelpers.RarityColor[item.rare].Hex3();

				Main.NewText( "The deal is made. Imparting [c/" + colorHex + ":" + item.HoverName + "]..." );
			}

			if( Main.netMode == 1 ) {
				if( this.player.whoAmI == Main.myPlayer ) {
					IntrinsicsSyncProtocol.SyncFromMe();
				} else {
					IntrinsicsSyncProtocol.SyncFromOther( this.player.whoAmI );
				}
			}
		}


		public void RemoveIntrinsic( string itemUid ) {
			this.IntrinsicItemUids.Remove( itemUid );

			int itemId;
			if( Libraries.Helpers.Items.ItemIdentityHelpers.TryGetTypeByUid( itemUid, out itemId ) ) {
				if( this.IntrinsicBuffItem.ContainsKey( itemId ) ) {
					this.IntrinsicBuffItem.Remove( itemId );
				}
				if( this.IntrinsicArmItem.ContainsKey( itemId ) ) {
					this.IntrinsicArmItem.Remove( itemId );
				}
				if( this.IntrinsicAccItem.ContainsKey( itemId ) ) {
					this.IntrinsicAccItem.Remove( itemId );
				}
			}
		}


		////////////////

		public bool ToggleIntrinsic( int itemType ) {
			if( !this.IntrinsicToggle.ContainsKey(itemType) ) {
				return false;
			}

			this.IntrinsicToggle[itemType] = !this.IntrinsicToggle[itemType];
			return this.IntrinsicToggle[itemType];
		}


		////////////////

		private void UpdateIntrinsicBuffs() {
			foreach( Item item in this.IntrinsicBuffItem.Values ) {
				if( !this.IntrinsicToggle.GetOrDefault(item.type) ) {
					continue;
				}
				
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
				if( !this.IntrinsicToggle.GetOrDefault(item.type) ) {
					continue;
				}
				this.player.VanillaUpdateEquip( item );
				this.player.VanillaUpdateAccessory( this.player.whoAmI, item, false, ref _, ref _, ref _ );
			}

			foreach( Item item in this.IntrinsicArmItem.Values ) {
				if( !this.IntrinsicToggle.GetOrDefault(item.type) ) {
					continue;
				}
				this.player.VanillaUpdateEquip( item );
			}
			//VanillaUpdateInventory( this.inventory[j] );
		}
	}
}
