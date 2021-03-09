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
			Item contractItem = PlayerItemFinderHelpers.FindFirstOfPossessedItemFor(
				player,
				new HashSet<int> { ModContent.ItemType<BlankContractItem>() },
				false
			);
			var myContractItem = item.modItem as BlankContractItem;
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

			bool isAdded = false;

			if( myContractItem.CanAddItem( item ) ) {
				bool hasMadeContract = myContractItem.CreateImpartmentContract( player, item );

				if( IntrinsicsConfig.Instance.DebugModeInfo ) {
					Main.NewText( "Impartment contract created? "+hasMadeContract );
				}

				if( hasMadeContract ) {
					PlayerItemHelpers.RemoveInventoryItemQuantity( player, item.type, 1 );
					Main.mouseItem = new Item();

					isAdded = true;
				}
			}

			return isAdded;
		}



		////////////////

		public void SetScribeMode( bool on ) {
			this.IsScribeMode = on;
		}
	}
}
