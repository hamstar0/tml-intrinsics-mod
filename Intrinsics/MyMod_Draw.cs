using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using Intrinsics.NPCs;
using Intrinsics.UI;


namespace Intrinsics {
	partial class IntrinsicsMod : Mod {
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
			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Text" ) );
			if( idx == -1 ) { return; }

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

			////

			var tradeLayer = new LegacyGameInterfaceLayer( "Intrinsics: Trade UI", tradeUI, InterfaceScaleType.UI );
			layers.Insert( idx, tradeLayer );
		}
	}
}
