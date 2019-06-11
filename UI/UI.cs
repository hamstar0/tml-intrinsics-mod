using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace Intrinsics.UI {
	partial class IntrinsicsControlsUI : UIState {
		internal Texture2D ButtonPageAddTex;
		
		private UIText Label;
		private UIImageButton ButtonOpenDialog;



		////////////////

		public IntrinsicsControlsUI() : base() {
			var mymod = IntrinsicsMod.Instance;

			this.ButtonPageAddTex = mymod.GetTexture( "UI/Button" );
		}


		////////////////

		public override void OnInitialize() {
			var mymod = IntrinsicsMod.Instance;

			this.Label = new UIText( "Show active intrinsics" );

			this.ButtonOpenDialog = new UIImageButton( this.ButtonPageAddTex );
			this.ButtonOpenDialog.OnClick += ( evt, elem ) => {
				IntrinsicsMod.Instance.IntrinsicsDialog.Open();
			};

			////

			int x = mymod.Config.ControlsPositionX >= 0 ?
				mymod.Config.ControlsPositionX :
				Main.screenWidth + mymod.Config.ControlsPositionX;
			int y = mymod.Config.ControlsPositionY >= 0 ?
				mymod.Config.ControlsPositionY :
				Main.screenHeight + mymod.Config.ControlsPositionY;

			this.Label.Left.Set( x, 0f );
			this.Label.Top.Set( y, 0f );
			this.ButtonOpenDialog.Left.Set( x, 0f );
			this.ButtonOpenDialog.Top.Set( y + 32f, 0f );

			////

			base.Append( this.Label );
			base.Append( this.ButtonOpenDialog );
		}
	}
}
