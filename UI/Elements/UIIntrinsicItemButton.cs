using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;


namespace Intrinsics.UI.Elements {
	public class UIIntrinsicItemButton : UIPanel {
		private Item MyItem;
		private bool IsEnabled;

		private int CurrentFrame = 0;
		private int CurrentFrameDuration = 15;



		////////////////

		public UIIntrinsicItemButton( Item item, bool isEnabled ) {
			this.MyItem = item;
			this.IsEnabled = isEnabled;

			var label = new UIText( this.MyItem.HoverName );
			label.Top.Set( 0f, 0f );
			label.Left.Set( 22f, 0f );
			label.TextColor = this.IsEnabled ? Color.White : new Color( 96, 96, 96 );

			this.Width.Set( -16f, 1f );
			this.Height.Set( 40f, 0f );
			this.Append( label );
			this.OnClick += ( _, __ ) => {
				var mymod = IntrinsicsMod.Instance;
				if( !mymod.Config.ToggleableIntrinsics ) {
					return;
				}

				var myplayer2 = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );
				this.IsEnabled = myplayer2.ToggleIntrinsic( this.MyItem.type );

				label.TextColor = this.IsEnabled ? Color.White : new Color( 96, 96, 96 );
			};
		}


		////////////////

		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			try {
				Texture2D itemTex = Main.itemTexture[this.MyItem.type];
				int frames = Main.itemAnimations[this.MyItem.type]?.FrameCount ?? 1;
				int ticks = Main.itemAnimations[this.MyItem.type]?.TicksPerFrame ?? 0;
				int height = itemTex.Height / frames;

				var srcRect = new Rectangle( 0, this.CurrentFrame * height, itemTex.Width, height );
				var destPos = new Vector2( 8f + this.GetOuterDimensions().X, 8f + this.GetOuterDimensions().Y );

				float scale = itemTex.Width > itemTex.Height ? itemTex.Width : itemTex.Height;
				scale = 24f/ scale;

				sb.Draw( itemTex, destPos, new Rectangle?(srcRect), Color.White, 0f, default(Vector2), scale, SpriteEffects.None, 1f );

				if( frames > 0 && --this.CurrentFrameDuration <= 0 ) {
					this.CurrentFrameDuration = ticks;

					this.CurrentFrame = this.CurrentFrame + 1;
					if( this.CurrentFrame >= frames ) {
						this.CurrentFrame = 0;
					}
				}
			} catch( Exception e ) {
				Main.NewText( e.ToString(), Color.Red );
			}
		}
	}
}
