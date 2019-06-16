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
			var mymod = (IntrinsicsMod)this.mod;

			if( !mymod.Config.DebugModeCheat ) {
				caller.Reply( "Cheat mode not active. See configs.", Color.Red );
				return;
			}

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

			if( args.Length < 1 ) {
				caller.Reply( "Insufficient arguments.", Color.Red );
				return;
			}

			string itemName = string.Join( " ", args );
			int itemId;

			if( !ItemIdentityHelpers.NamesToIds.ContainsKey(itemName) ) {
				if( !Libraries.Helpers.Items.ItemIdentityHelpers.TryGetTypeByUid(itemName, out itemId) ) {
					caller.Reply( "Invalid item name: " + itemName, Color.Red );
					return;
				}
			} else {
				itemId = ItemIdentityHelpers.NamesToIds[itemName];
			}

			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );

			myplayer.RemoveIntrinsic( ItemIdentityHelpers.GetProperUniqueId(itemId) );

			caller.Reply( "Intrinsic removed.", Color.Lime );
		}
	}
}
