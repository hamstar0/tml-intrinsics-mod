using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.ItemHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using HamstarHelpers.Helpers.UserHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics.Commands {
	class RemoveIntrinsicCommand : ModCommand {
		public override CommandType Type {
			get {
				if( Main.netMode == 0 && !Main.dedServ ) {
					return CommandType.World;
				}
				return CommandType.Console | CommandType.World;
			}
		}
		public override string Command => "intrinsic-remove";
		public override string Usage => "/" + this.Command +" \"Solar Wings\"";
		public override string Description => "Removes a specific intrinsic from self." +
			"\n   Parameters: <item name>";



		////////////////

		public override void Action( CommandCaller caller, string input, string[] args ) {
			if( Main.netMode == 1 ) {
				LogHelpers.Warn( "Not supposed to run on client." );
				return;
			}

			if( Main.netMode == 2 && caller.CommandType != CommandType.Console ) {
				bool hasPriv = UserHelpers.HasBasicServerPrivilege( caller.Player );

				if( !hasPriv ) {
					caller.Reply( "Access denied.", Color.Red );
					return;
				}
			}

			var mymod = (IntrinsicsMod)this.mod;

			if( args.Length < 1 ) {
				caller.Reply( "Insufficient arguments.", Color.Red );
				return;
			}

			string itemName = string.Join( " ", args );

			if( !ItemIdentityHelpers.NamesToIds.ContainsKey(itemName) ) {
				caller.Reply( "Invalid item name: "+itemName, Color.Red );
				return;
			}

			int itemId = ItemIdentityHelpers.NamesToIds[ itemName ];
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );

			myplayer.RemoveIntrinsic( ItemIdentityHelpers.GetProperUniqueId(itemId) );

			caller.Reply( "Intrinsic removed.", Color.Lime );
		}
	}
}
