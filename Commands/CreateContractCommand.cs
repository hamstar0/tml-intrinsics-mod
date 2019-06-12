using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.ItemHelpers;
using HamstarHelpers.Helpers.TmlHelpers.CommandsHelpers;
using HamstarHelpers.Helpers.UserHelpers;
using Intrinsics.Items;
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

			if( args[0].Length == 0 || args[0][0] != '\"' ) {
				caller.Reply( "Invalid first item name: "+args[0], Color.Red );
				return;
			}

			IList<Item> items = new List<Item>();

			int nextArgIdx = 0;
			do {
				string itemName = CommandsHelpers.GetQuotedStringFromArgsAt( args, nextArgIdx, out nextArgIdx );
				if( !ItemIdentityHelpers.NamesToIds.ContainsKey(itemName) ) {
					caller.Reply( "Invalid item name: "+itemName, Color.Red );
					return;
				}

				int itemId = ItemIdentityHelpers.NamesToIds[ itemName ];
				var item = new Item();
				item.SetDefaults( itemId );

				items.Add( item );
			} while( nextArgIdx != -1 );

			IEnumerable<string> itemNames = items.Select( i => ItemIdentityHelpers.GetProperUniqueId( i.type ) );
			ImpartmentContractItem.Create( Main.LocalPlayer, Main.LocalPlayer.Center, new HashSet<string>( itemNames ) );

			caller.Reply( "Created Impartment Contract.", Color.Lime );
		}
	}
}
