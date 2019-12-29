using HamstarHelpers.Helpers.Items.Attributes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Intrinsics.Items {
	public partial class BlankContractItem : ModItem {
		public int MyLastInventoryPosition { get; protected set; }



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Blank Contract" );
			this.Tooltip.SetDefault( "Write your own destiny."+
				"\nDrop a valid item onto this contract to bind it."
			);
		}

		public override void SetDefaults() {
			this.item.width = 24;
			this.item.height = 24;
			this.item.value = Item.buyPrice( 1, 0, 0, 0 );
			this.item.rare = ItemRarityAttributeHelpers.HighestVanillaRarity;
		}


		////////////////
		
		public override void UpdateInventory( Player player ) {
			this.MyLastInventoryPosition = -1;

			for( int i = 0; i < player.inventory.Length; i++ ) {
				if( player.inventory[i] == this.item ) {
					this.MyLastInventoryPosition = i;
					break;
				}
			}
		}
	}
}
