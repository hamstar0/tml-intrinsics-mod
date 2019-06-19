using Intrinsics.Libraries.Helpers.Items;
using Terraria.ID;
using Terraria.ModLoader;


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
			this.AddIngredient( mymod.ItemType<ContractTatterItem>(), mymod.Config.BlankContractRecipeContractTattersNeeded );

			this.SetResult( myitem );
		}


		public override bool RecipeAvailable() {
			var mymod = (IntrinsicsMod)this.mod;
			return mymod.Config.BlankContractRecipeContractTattersNeeded > 0;
		}
	}



	class BlankContractAltRecipe : ModRecipe {
		public BlankContractAltRecipe( IntrinsicsMod mymod, BlankContractItem myitem ) : base( mymod ) {
			int itemId;

			foreach( var kv in mymod.Config.BlankContractAltRecipeIngredients ) {
				string itemUid = kv.Key;
				int count = kv.Value;

				if( count > 0 && ItemIdentityHelpers.TryGetTypeByUid(itemUid, out itemId ) ) {
					this.AddIngredient( itemId, count );
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
