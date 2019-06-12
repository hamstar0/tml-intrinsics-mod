using HamstarHelpers.Helpers.ItemHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using Intrinsics.Items;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace Intrinsics {
	public static partial class IntrinsicsAPI {
		public static IntrinsicsConfigData GetModSettings() {
			return IntrinsicsMod.Instance.Config;
		}

		public static void SaveModSettingsChanges() {
			IntrinsicsMod.Instance.ConfigJson.SaveFile();
		}


		////////////////

		public static int CreateContract( Player player, params Item[] items ) {
			IEnumerable<string> itemNames = items.Select( i => ItemIdentityHelpers.GetProperUniqueId( i.type ) );
			return ImpartmentContractItem.Create( player, player.Center, new HashSet<string>( itemNames ) );
		}


		////////////////

		public static bool CanApplyIntrinsic( Item intrinsicItem ) {
			return IntrinsicsLogic.ItemHasIntrinsics( intrinsicItem );
		}

		public static bool ApplyIntrinsic( Player player, Item intrinsicItem, bool forceApply=false ) {
			bool canApply = forceApply || IntrinsicsLogic.ItemHasIntrinsics( intrinsicItem );

			if( canApply ) {
				var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( player );

				myplayer.ApplyIntrinsic( ItemIdentityHelpers.GetProperUniqueId(intrinsicItem) );
			}

			return canApply;
		}

		public static void RemoveIntrinsic( Player player, Item item ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( player );
			myplayer.RemoveIntrinsic( ItemIdentityHelpers.GetProperUniqueId(item) );
		}
	}
}
