using HamstarHelpers.Components.UI;
using HamstarHelpers.Components.UI.Elements;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace Intrinsics.UI {
	class UIIntrinsicsDialog : UIDialog {
		public static void DrawButton() {

		}



		////////////////

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
