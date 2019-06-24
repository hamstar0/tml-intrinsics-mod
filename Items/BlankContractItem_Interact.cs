using HamstarHelpers.Helpers.ItemHelpers;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics.Items {
	public partial class BlankContractItem : ModItem {
		public override bool ConsumeItem( Player player ) {
			return false;
		}


		////////////////

		public bool CanAddItem( Item item ) {
/*Main.NewText( "CanAddItem - "+
	"is new? "+this.IntrinsicItemUids.Contains( ItemIdentityHelpers.GetProperUniqueId(item.type) )+
	", is buff? "+(item.buffType != 0)+
	", is acc? "+item.accessory+
	", is armor? "+ItemAttributeHelpers.IsArmor( item )
);*/
			return IntrinsicsLogic.ItemHasIntrinsics( item );
		}

		public bool CreateImpartmentContract( Player player, Item item ) {
			var items = new HashSet<string> { ItemIdentityHelpers.GetProperUniqueId( item.type ) };

			return ImpartmentContractItem.Create( player, player.Center, items ) != -1;
		}
	}
}
