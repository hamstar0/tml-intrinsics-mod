﻿using HamstarHelpers.Helpers.Items;
using HamstarHelpers.Helpers.TModLoader;
using Intrinsics.Items;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;


namespace Intrinsics {
	public static partial class IntrinsicsAPI {
		public static int CreateContract( Player player, params Item[] items ) {
			IEnumerable<string> itemNames = items.Select( i => ItemID.GetUniqueKey( i.type ) );   //TODO GetProperUniqueId
			return ImpartmentContractItem.Create( player, player.Center, new HashSet<string>( itemNames ) );
		}


		////////////////

		public static bool CanApplyIntrinsic( Item intrinsicItem ) {
			return IntrinsicsLogic.ItemHasIntrinsics( intrinsicItem );
		}

		public static bool ApplyIntrinsic( Player player, Item intrinsicItem, bool isEnabled, bool forceApply=false ) {
			bool canApply = forceApply || IntrinsicsLogic.ItemHasIntrinsics( intrinsicItem );

			if( canApply ) {
				var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( player );

				myplayer.ApplyIntrinsic( ItemID.GetUniqueKey( intrinsicItem), isEnabled );   //TODO GetProperUniqueId
			}

			return canApply;
		}

		public static void RemoveIntrinsic( Player player, Item item ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( player );
			myplayer.RemoveIntrinsic( ItemID.GetUniqueKey(item) );   //TODO GetProperUniqueId
		}
	}
}
