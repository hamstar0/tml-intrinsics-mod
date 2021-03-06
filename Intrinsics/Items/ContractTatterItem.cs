using HamstarHelpers.Helpers.Items.Attributes;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics.Items {
	public partial class ContractTatterItem : ModItem {
		public override bool CloneNewInstances => false;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Contract Tatter" );
			this.Tooltip.SetDefault( "To mortal eyes it looks only like a piece of paper..." );
		}

		public override void SetDefaults() {
			this.item.width = 32;
			this.item.height = 16;
			this.item.value = Item.buyPrice( 1, 0, 0, 0 );
			this.item.rare = ItemRarityAttributeHelpers.HighestVanillaRarity;
			this.item.material = true;
			this.item.maxStack = 99;
		}
	}
}
