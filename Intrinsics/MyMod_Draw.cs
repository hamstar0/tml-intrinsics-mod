using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using Intrinsics.UI;


namespace Intrinsics {
	partial class IntrinsicsMod : Mod {
		private void InitializeUI() {
			this.InitializeControlsUI();

			this.BlankContractTex = this.GetTexture( "Items/BlankContractItem" );
		}

		private void InitializeControlsUI() {
			this.UIContext = new UserInterface();
			this.HUDComponents = new UIIntrinsicsHUD();

			this.HUDComponents.Activate();
			this.UIContext.SetState( this.HUDComponents );
		}


		////////////////

		public override void UpdateUI( GameTime gameTime ) {
			// Update HUD button when dialog not open
			if( !this.ControlPanelDialog.IsOpen ) {
				this.UIContext?.Update( gameTime );
			}
		}


		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			GameInterfaceDrawMethod tradeUI = () => {
				var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );

				if( Main.playerInventory ) {
					if( this.IsTrading ) {
						this.DrawTradeUI();
					}

					// Draw HUD button when dialog not open
					if( !this.ControlPanelDialog.IsOpen ) {
						//if( myplayer.IntrinsicItemUids.Count > 0 ) {
						this.UIContext.Draw( Main.spriteBatch, Main._drawInterfaceGameTime );
						//mymod.ControlsUI?.Draw( Main.spriteBatch );
						//}
					}
				}
				return true;
			};
			
			GameInterfaceDrawMethod cursorUI = () => {
				var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );

				if( myplayer.IsScribeMode ) {
					Main.spriteBatch.Draw(
						texture: this.BlankContractTex,
						position: Main.MouseScreen + new Vector2(12, 16),
						color: Color.White
					);
				}

				return true;
			};

			//

			int mouseTextIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Text" ) );
			if( mouseTextIdx != -1 ) {
				var tradeLayer = new LegacyGameInterfaceLayer( "Intrinsics: Trade UI", tradeUI, InterfaceScaleType.UI );
				layers.Insert( mouseTextIdx, tradeLayer );
			}

			int cursorIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Cursor" ) );
			if( cursorIdx != -1 ) {
				var cursorLayer = new LegacyGameInterfaceLayer( "Intrinsics: Scribe Cursor", cursorUI, InterfaceScaleType.UI );
				layers.Insert( cursorIdx + 1, cursorLayer );
			}
		}
	}
}
