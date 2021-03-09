using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;
using Intrinsics.Items;


namespace Intrinsics {
	partial class IntrinsicsPlayer : ModPlayer {
		private static bool ScribeBlankContractIf( Player player, Item item ) {
			int blankContractType = ModContent.ItemType<BlankContractItem>();
			Item contractItem = PlayerItemFinderHelpers.FindFirstOfPossessedItemFor(
				player,
				new HashSet<int> { blankContractType },
				false
			);

			var myContractItem = contractItem.modItem as BlankContractItem;
			if( contractItem == null || myContractItem == null ) {
				Main.NewText( "No blank contract item in player's possession.", Color.Yellow );
				return false;
			}

			if( item?.active != true ) {
				if( IntrinsicsConfig.Instance.DebugModeInfo ) {
					Main.NewText( "BlankContractItem reports it cannot swap selected item" );
				}
				return false;
			}

			if( !myContractItem.CanAddItem(item) ) {
				Main.NewText( "This item cannot be scribed.", Color.Yellow );
				return false;
			}

			bool hasMadeContract = myContractItem.CreateImpartmentContract( player, item );

			if( IntrinsicsConfig.Instance.DebugModeInfo ) {
				Main.NewText( "Impartment contract created? "+hasMadeContract );
			}

			if( !hasMadeContract ) {
				return false;
			}

			PlayerItemHelpers.RemoveInventoryItemQuantity( player, item.type, 1 );
			PlayerItemHelpers.RemoveInventoryItemQuantity( player, blankContractType, 1 );
			Main.mouseItem = new Item();

			return hasMadeContract;
		}



		////////////////

		public void SetScribeMode( bool on ) {
			this.IsScribeMode = on;
		}
	}
}
