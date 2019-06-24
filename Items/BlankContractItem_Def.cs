using HamstarHelpers.Helpers.ItemHelpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Intrinsics.Items {
	public partial class BlankContractItem : ModItem {
		public int MyLastInventoryPosition { get; private set; }

		public override bool CloneNewInstances => false;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Blank Contract" );
			this.Tooltip.SetDefault( "Write your own destiny.\n"+
				"Drop a valid item onto this contract to bind it."
			);
		}

		public override void SetDefaults() {
			this.item.width = 24;
			this.item.height = 24;
			this.item.value = Item.buyPrice( 1, 0, 0, 0 );
			this.item.rare = ItemAttributeHelpers.HighestVanillaRarity;
			this.item.consumable = true;
			this.item.useStyle = 4;
			this.item.useTime = 30;
			this.item.useAnimation = 30;
			this.item.UseSound = SoundID.Item4;
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
