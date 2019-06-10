using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;


namespace Intrinsics {
	partial class IntrinsicsMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Text" ) );
			if( idx == -1 ) { return; }

			GameInterfaceDrawMethod tradeUI = () => {
				if( this.IsTrading ) {
DebugHelpers.Print("blah", "", 20);
					this.DrawTradeUI();
				}
				return true;
			};

			////

			var tradeLayer = new LegacyGameInterfaceLayer( "Intrinsics: Trade UI", tradeUI, InterfaceScaleType.UI );
			layers.Insert( idx, tradeLayer );
		}


		////////////////

		private void DrawTradeUI() {
			string text = "blah";
			int context = 23;
			int x = 40;
			int y = 40;
			var pos = new Vector2( x + 50, y );
			var color = Color.White * ( (float)Main.mouseTextColor / 255f );

			ChatManager.DrawColorCodedStringWithShadow( Main.spriteBatch, Main.fontMouseText, text, pos, color, 0f, Vector2.Zero, Vector2.One, -1f, 2f );

			int maxX = (int)( (float)( x + Main.inventoryBackTexture.Width ) * Main.inventoryScale );
			int maxY = (int)( (float)( y + Main.inventoryBackTexture.Height ) * Main.inventoryScale );

			if( Main.mouseX >= x && Main.mouseX <= maxX && Main.mouseY >= y && (float)Main.mouseY <= maxY && !PlayerInput.IgnoreMouseInterface ) {
				Main.LocalPlayer.mouseInterface = true;
				Main.craftingHide = true;

				if( Main.mouseLeftRelease && Main.mouseLeft ) {
					ItemSlot.LeftClick( ref this.TradeItem, context );
					Recipe.FindRecipes();
				} else {
					ItemSlot.RightClick( ref this.TradeItem, context );
				}
				ItemSlot.MouseHover( ref this.TradeItem, context );
			}

			ItemSlot.Draw( Main.spriteBatch, ref this.TradeItem, context, new Vector2(x, y), default(Color) );
		}
	}
}
