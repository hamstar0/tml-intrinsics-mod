using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using HamstarHelpers.Helpers.Debug;
using Intrinsics.NPCs;


namespace Intrinsics {
	partial class IntrinsicsMod : Mod {
		private void DrawTradeUI() {
			string text = "Give a rare or hard-to-get item";

			int x = this.Config.TradeUIPositionX >= 0 ?
				this.Config.TradeUIPositionX :
				Main.screenWidth + this.Config.TradeUIPositionX;
			int y = this.Config.TradeUIPositionY >= 0 ?
				this.Config.TradeUIPositionY :
				Main.screenHeight + this.Config.TradeUIPositionY;

			if( ModLoader.GetMod("ExtensibleInventory") != null ) {
				y += 40;
			}

			var textPos = new Vector2( x + 64, y );
			var color = Color.White * ( (float)Main.mouseTextColor / 255f );

			float oldInvScale = Main.inventoryScale;
			Main.inventoryScale = 1f;

			ChatManager.DrawColorCodedStringWithShadow(
				Main.spriteBatch,
				Main.fontMouseText,
				text,
				textPos,
				color,
				0f,
				Vector2.Zero,
				Vector2.One,
				-1f,
				2f
			);

			int maxX = (int)( x + ((float)Main.inventoryBackTexture.Width * Main.inventoryScale) );
			int maxY = (int)( y + ((float)Main.inventoryBackTexture.Height * Main.inventoryScale) );
			
			if( Main.mouseX >= x && Main.mouseX <= maxX && Main.mouseY >= y && Main.mouseY <= maxY && !PlayerInput.IgnoreMouseInterface ) {
				Main.LocalPlayer.mouseInterface = true;
				Main.craftingHide = true;

				if( Main.mouseLeftRelease && Main.mouseLeft ) {
					ItemSlot.LeftClick( ref this.TradeItem, 0 );
					WanderingGhostNPC.AttemptTrade( ref this.TradeItem );
					Recipe.FindRecipes();
				} else {
					ItemSlot.RightClick( ref this.TradeItem, 0 );
				}
				ItemSlot.MouseHover( ref this.TradeItem, 0 );
			}

			ItemSlot.Draw(
				Main.spriteBatch,
				ref this.TradeItem,
				ItemSlot.Context.GuideItem,
				new Vector2(x, y),
				default(Color)
			);

			Main.inventoryScale = oldInvScale;
		}
	}
}
