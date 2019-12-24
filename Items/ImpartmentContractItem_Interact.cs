using HamstarHelpers.Helpers.TModLoader;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics.Items {
	public partial class ImpartmentContractItem : ModItem {
		public override bool ConsumeItem( Player player ) {
			if( Main.mouseRight ) {
				return false;
			}

			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( player );

			foreach( string itemUid in this.IntrinsicItemUids ) {
				myplayer.ApplyIntrinsic( itemUid, true );
			}
			
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
