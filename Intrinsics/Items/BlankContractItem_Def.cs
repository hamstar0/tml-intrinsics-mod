using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Items.Attributes;
using HamstarHelpers.Services.Timers;


namespace Intrinsics.Items {
	public partial class BlankContractItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Blank Contract" );
			this.Tooltip.SetDefault( "Write your own destiny."
				+"\nRight-click to begin picking a scribe item"
				+"\nLeft-click with another item to scribe it into the contract"
			);
		}

		public override void SetDefaults() {
			this.item.width = 24;
			this.item.height = 24;
			this.item.value = Item.buyPrice( 1, 0, 0, 0 );
			this.item.rare = ItemRarityAttributeHelpers.HighestVanillaRarity;
		}

		////

		public override bool CanRightClick() {
			Timers.SetTimer( "PKEMeterToggleBlocker", 2, true, () => {
				var myplayer = Main.LocalPlayer.GetModPlayer<IntrinsicsPlayer>();
				myplayer.SetScribeMode( true );

				return false;
			} );
			return false;
		}
	}
}
