using Terraria;
using Terraria.ModLoader;


namespace Intrinsics.Items {
	public partial class ImpartmentContractItem : ModItem {
		public override bool ConsumeItem( Player player ) {
			if( Main.mouseRight ) {
				return false;
			}
			
			Main.NewText( string.Join(", ", this.IntrinsicItemUids) );

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
	}
}
