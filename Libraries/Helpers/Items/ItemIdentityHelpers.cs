using HamstarHelpers.Helpers.DebugHelpers;
using System;
using Terraria.ModLoader;


namespace Intrinsics.Libraries.Helpers.Items {
	public class ItemIdentityHelpers {
		public static bool TryGetTypeByUid( string itemUid, out int itemId ) {
			string[] split = itemUid.Split( '.' );

			if( split[0] == "Terraria" ) {
				return Int32.TryParse( split[1], out itemId );
			} else {
				Mod mod = ModLoader.GetMod( split[0] );
				if( mod == null ) {
					itemId = -1;
					return false;
				}
				itemId = mod.ItemType( split[1] );
				return true;
			}
		}
	}
}
