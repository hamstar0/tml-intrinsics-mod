using HamstarHelpers.Helpers.Items;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace Intrinsics.Items {
	public partial class BlankContractItem : ModItem {
		public override void AddRecipes() {
			var mymod = (IntrinsicsMod)this.mod;

			if( mymod.Config.BlankContractRecipeContractTattersNeeded > 0 ) {
				var blankContractRecipe = new BlankContractRecipe( mymod, this );
				blankContractRecipe.AddRecipe();
			}

			if( mymod.Config.BlankContractAltRecipeIngredients.Count > 0 ) {
				var otherBlankContractRecipe = new BlankContractAltRecipe( mymod, this );
				otherBlankContractRecipe.AddRecipe();
			}
		}
	}



	class BlankContractRecipe : ModRecipe {
		public BlankContractRecipe( IntrinsicsMod mymod, BlankContractItem myitem ) : base( mymod ) {
			this.AddTile( TileID.WorkBenches );
			this.AddIngredient( ModContent.ItemType<ContractTatterItem>(), mymod.Config.BlankContractRecipeContractTattersNeeded );

			this.SetResult( myitem );
		}


		public override bool RecipeAvailable() {
			var mymod = (IntrinsicsMod)this.mod;
			return mymod.Config.BlankContractRecipeContractTattersNeeded > 0;
		}
	}



	class BlankContractAltRecipe : ModRecipe {
		public BlankContractAltRecipe( IntrinsicsMod mymod, BlankContractItem myitem ) : base( mymod ) {
			foreach( var kv in mymod.Config.BlankContractAltRecipeIngredients ) {
				ItemDefinition itemDef = kv.Key;
				int count = kv.Value;

				if( count > 0 && itemDef.Type != 0 ) {
					this.AddIngredient( itemDef.Type, count );
				}
			}

			//if( !string.IsNullOrEmpty(mymod.Config.BlankContractRecipeStation) ) {
			//	this.AddTile( TileIdentityHelpers.GetVanillaTileName mymod.Config.BlankContractRecipeStation );
			//}

			this.SetResult( myitem );
		}


		public override bool RecipeAvailable() {
			var mymod = (IntrinsicsMod)this.mod;
			return mymod.Config.BlankContractAltRecipeIngredients.Count > 0;
		}
	}
}
