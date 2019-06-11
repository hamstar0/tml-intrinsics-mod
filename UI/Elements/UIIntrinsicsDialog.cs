using HamstarHelpers.Components.UI;
using HamstarHelpers.Components.UI.Elements;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace Intrinsics.UI.Elements {
	class UIIntrinsicsDialog : UIDialog {
		public UIIntrinsicsDialog() : base( UITheme.Vanilla, 320, 240 ) {
		}

		////////////////

		public override void InitializeComponents() {
			var self = this;

			var title = new UIText( "Test" );
			this.InnerContainer.Append( (UIElement)title );
		}
	}
}
