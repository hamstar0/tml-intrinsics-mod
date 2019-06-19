using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.ItemHelpers;
using HamstarHelpers.Helpers.UserHelpers;
using Intrinsics.Items;
using Intrinsics.Libraries.Helpers.Commands;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics.Commands {
	class CreateContractCommand : ModCommand {
		public override CommandType Type {
			get {
				if( Main.netMode == 0 && !Main.dedServ ) {
					return CommandType.World;
				}
				return CommandType.Console | CommandType.World;
			}
		}
		public override string Command => "intrinsic-create-contract";
		public override string Usage => "/" + this.Command +" \"Solar Wings\" \"Frostspark Boots\"";
		public override string Description => "Creates an Impartment Contract to specification." +
			"\n   Parameters: <quote-wrapped item name 1> [<quote-wrapped item name 2> ...]";



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

			if( args[0].Length == 0 || args[0][0] != '\"' ) {
				caller.Reply( "Invalid first item name: "+args[0], Color.Red );
				return;
			}

			IList<Item> items = new List<Item>();

			int argNextIdx = 0;
			string itemName;

			while( CommandsHelpers.GetQuotedStringFromArgsAt(args, argNextIdx, out argNextIdx, out itemName) ) {
				int itemId;

				if( !ItemIdentityHelpers.NamesToIds.ContainsKey(itemName) ) {
					if( !Libraries.Helpers.Items.ItemIdentityHelpers.TryGetTypeByUid(itemName, out itemId) ) {
						caller.Reply( "Invalid item name: " + itemName, Color.Red );
						return;
					}
				} else {
					itemId = ItemIdentityHelpers.NamesToIds[itemName];
				}

				var item = new Item();
				item.SetDefaults( itemId );

				items.Add( item );
			}

			IEnumerable<string> itemNames = items.Select( i => ItemIdentityHelpers.GetProperUniqueId( i.type ) );
			ImpartmentContractItem.Create( Main.LocalPlayer, Main.LocalPlayer.Center, new HashSet<string>( itemNames ) );

			caller.Reply( "Created Impartment Contract.", Color.Lime );
		}
	}
}
