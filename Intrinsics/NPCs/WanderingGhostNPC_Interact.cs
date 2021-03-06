using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI.Gamepad;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Items;
using HamstarHelpers.Helpers.Items.Attributes;
using Intrinsics.Items;


namespace Intrinsics.NPCs {
	partial class WanderingGhostNPC : ModNPC {
		internal static void UpdateTradingState() {
			var mymod = IntrinsicsMod.Instance;
			var plr = Main.player[ Main.myPlayer ];

			mymod.IsTrading = Main.playerInventory
				&& mymod.IsTrading
				&& !plr.dead
				&& WanderingGhostNPC.CanTrade();

			if( !mymod.IsTrading ) {
				Item item = mymod.TradeItem;

				if( !item.IsAir ) {
					int itemIdx = Item.NewItem( (int)plr.position.X, (int)plr.position.Y, item.width, item.height, item.type, item.stack, false, (int)item.prefix, true, false );

					Main.item[itemIdx] = item.Clone();
					Main.item[itemIdx].newAndShiny = false;
					Main.item[itemIdx].position = plr.Center;
					if( Main.netMode == 1 ) {
						NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemIdx, 1f, 0f, 0f, 0, 0, 0 );
					}

					mymod.TradeItem = new Item();
				}
			}
		}

		////

		public static bool CanTrade() {
			return WanderingGhostNPC.GetNearestGhost( Main.LocalPlayer ) != null;
		}

		public static bool AttemptTrade( ref Item tradeItem ) {
			var mymod = IntrinsicsMod.Instance;
			var itemDef = new ItemDefinition( tradeItem.type );

			if( mymod.Config.TradeItemContractTatters.ContainsKey(itemDef) ) {
				int tatterNum = mymod.Config.TradeItemContractTatters[itemDef];
				NPC npc = WanderingGhostNPC.GetNearestGhost( Main.LocalPlayer );

				ItemHelpers.CreateItem( npc.position, ModContent.ItemType<ContractTatterItem>(), tatterNum, 16, 16 );
				ItemHelpers.ReduceStack( tradeItem, 1 );

				return true;
			}

			return false;
		}



		////////////////

		public override void OnChatButtonClicked( bool firstButton, ref bool shop ) {
			if( firstButton ) {
				var mymod = (IntrinsicsMod)this.mod;
				mymod.IsTrading = true;

				Main.playerInventory = true;
				Main.npcChatText = "";
				Main.PlaySound( 12, -1, -1, 1, 1f, 0f );
				UILinkPointNavigator.GoToDefaultPage( 0 );
			} else {
				this.UpdateChatWithTip();
			}
		}

		public override void AI() {
			var mymod = (IntrinsicsMod)this.mod;

			if( mymod.IsTrading ) {
				if( this.npc.ai[0] == 1f ) {
					this.npc.ai[0] = 0f;
				}
			}
//DebugHelpers.Print( "ghostai_"+npc.whoAmI, string.Join(", ", npc.ai.Select(a=>a.ToString("N2"))), 20 );
		}


		////////////////

		public void UpdateChatWithTip() {
			if( this.CurrentChat != WanderingGhostNPC.ChatText ) { return; }

			var mymod = (IntrinsicsMod)this.mod;
			int itemCount = mymod.Config.TradeItemContractTatters.Count;
			ItemDefinition itemDef = mymod.Config.TradeItemContractTatters
				.Keys
				.ToArray()
				[ Main.rand.Next(itemCount) ];

			if( itemDef.Type == 0 ) { return; }

			string itemName = ItemAttributeHelpers.GetQualifiedName( itemDef.Type );

			this.CurrentChat = "..." + itemName + "...";
			Main.npcChatText = this.CurrentChat;
		}
	}
}