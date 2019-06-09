using HamstarHelpers.Helpers.ItemHelpers;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics.Items {
	public partial class BlankContractItem : ModItem {
		public override bool ConsumeItem( Player player ) {
			if( Main.mouseRight ) {
				return false;
			}
			
			// Blah

			return true;
		}

		////

		public override bool UseItem( Player player ) {
			if( player.itemAnimation > 0 && player.itemTime == 0 ) {
				player.itemTime = item.useTime;
				return true;
			}
			return base.UseItem( player );
		}


		////////////////

		public bool CanAddItem( Item item ) {
			if( this.IntrinsicItemUids.Contains( ItemIdentityHelpers.GetProperUniqueId(item.type) ) ) {
				return false;
			}
			return item.buffType != 0 || item.accessory || ItemAttributeHelpers.IsArmor( item );
		}

		public bool AddItem( Player player, Item item ) {
			this.IntrinsicItemUids.Add( ItemIdentityHelpers.GetProperUniqueId(item.type) );

			return ImpartmentContractItem.Create( player, this.IntrinsicItemUids ) != -1;
		}
	}
}
