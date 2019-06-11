using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics.Items {
	public partial class ImpartmentContractItem : ModItem {
		private int FrameDelay = 0;
		private int Frame = 0;



		////////////////

		public override bool PreDrawInInventory( SpriteBatch sb, Vector2 pos, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale ) {
			var mymod = IntrinsicsMod.Instance;
			Texture2D tex = ModLoader.GetTexture( this.Texture );
			Texture2D texOverlay = ImpartmentContractItem.Overlay;
			if( texOverlay == null ) {
				return true;
			}

			int height = tex.Width;
			var srcRect = new Rectangle( 0, this.Frame * height, tex.Width, height );
			Color overlayColor = Color.White;

			sb.Draw( tex, pos, new Rectangle(0, 0, tex.Width, tex.Height), drawColor, 0f, default(Vector2), scale, SpriteEffects.None, 1f );
			sb.Draw( texOverlay, pos, srcRect, overlayColor, 0f, default( Vector2 ), scale, SpriteEffects.None, 1f );

			if( ++this.FrameDelay >= 4 ) {
				this.FrameDelay = 0;
				if( ++this.Frame >= 8 ) {
					this.Frame = 0;
				}
			}

			return false;
		}

		public override bool PreDrawInWorld( SpriteBatch sb, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI ) {
			var mymod = IntrinsicsMod.Instance;
			Texture2D tex = ModLoader.GetTexture( this.Texture );
			Texture2D texOverlay = ImpartmentContractItem.Overlay;
			if( texOverlay == null ) {
				return true;
			}

			Vector2 pos = this.item.position - Main.screenPosition;
			int height = tex.Width;
			var srcRect = new Rectangle( 0, this.Frame * height, tex.Width, height );
			Color overlayColor = Color.Lerp( lightColor, Color.White, 0.5f );

			sb.Draw( tex, pos, new Rectangle(0, 0, tex.Width, tex.Height), lightColor, 0f, default(Vector2), scale, SpriteEffects.None, 1f );
			sb.Draw( texOverlay, pos, srcRect, overlayColor, 0f, default(Vector2), scale, SpriteEffects.None, 1f );

			if( ++this.FrameDelay >= 4 ) {
				this.FrameDelay = 0;
				if( ++this.Frame >= 8 ) {
					this.Frame = 0;
				}
			}

			return false;
		}
	}
}
