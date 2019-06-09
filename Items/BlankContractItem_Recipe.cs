using Terraria.ID;
using Terraria.ModLoader;


namespace Intrinsics.Items {
	public partial class BlankContractItem : ModItem {
		public override void AddRecipes() {
			var mymod = (IntrinsicsMod)this.mod;
			if( mymod.Config.BlankContractRecipeIngredients.Length > 0 ) {
				return;
			}

			ModRecipe recipe = new ModRecipe( mod );

			foreach( string itemUid in mymod.Config.BlankContractRecipeIngredients ) {
				recipe.AddIngredient( ItemID.DirtBlock, 10 );
			}

			//if( !string.IsNullOrEmpty(mymod.Config.BlankContractRecipeStation) ) {
			//	recipe.AddTile( TileIdentityHelpers.GetVanillaTileName mymod.Config.BlankContractRecipeStation );
			//}

			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}
