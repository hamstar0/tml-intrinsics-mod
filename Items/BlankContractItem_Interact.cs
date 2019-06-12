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
/*Main.NewText( "CanAddItem - "+
	"is new? "+this.IntrinsicItemUids.Contains( ItemIdentityHelpers.GetProperUniqueId(item.type) )+
	", is buff? "+(item.buffType != 0)+
	", is acc? "+item.accessory+
	", is armor? "+ItemAttributeHelpers.IsArmor( item )
);*/
			if( this.IntrinsicItemUids.Contains( ItemIdentityHelpers.GetProperUniqueId(item.type) ) ) {
				return false;
			}
			return IntrinsicsLogic.ItemHasIntrinsics( item );
		}

		public bool AddItem( Player player, Item item ) {
			this.IntrinsicItemUids.Add( ItemIdentityHelpers.GetProperUniqueId(item.type) );

			return ImpartmentContractItem.Create( player, player.Center, this.IntrinsicItemUids ) != -1;
		}
	}
}
