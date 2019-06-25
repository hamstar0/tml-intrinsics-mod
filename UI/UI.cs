using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
			this.Label = new UIText( "Show active intrinsics" );
			this.ButtonOpenDialog = new UIImageButton( this.ButtonPageAddTex );

			bool isLabelHover = false;
			bool isButtonHover = false;

			////

			this.Label.OnMouseOver += ( _, __ ) => {
				if( isLabelHover ) { return; }
				isLabelHover = true;
				this.Label.TextColor = Color.White;
				this.ButtonOpenDialog?.MouseOver(_);
				isLabelHover = false;
			};
			this.Label.OnMouseOut += ( _, __ ) => {
				if( isLabelHover ) { return; }
				isLabelHover = true;
				this.ButtonOpenDialog?.MouseOut( _ );
				isLabelHover = false;
			};

			this.ButtonOpenDialog.OnMouseOver += ( _, __ ) => {
				if( isButtonHover ) { return; }
				isButtonHover = true;
				this.Label.TextColor = Color.White;
				this.Label?.MouseOver( _ );
				isButtonHover = false;
			};
			this.ButtonOpenDialog.OnMouseOut += ( _, __ ) => {
				if( isButtonHover ) { return; }
				isButtonHover = true;
				this.Label?.MouseOut( _ );
				isButtonHover = false;
			};

			////

			this.Label.OnClick += ( evt, elem ) => {
				IntrinsicsMod.Instance.IntrinsicsDialog.Open();
			};
			this.ButtonOpenDialog.OnClick += ( evt, elem ) => {
				IntrinsicsMod.Instance.IntrinsicsDialog.Open();
			};

			////

			this.UpdateLayout();

			////

			base.Append( this.Label );
			base.Append( this.ButtonOpenDialog );
		}


		////////////////

		public override void Update( GameTime gameTime ) {
			this.UpdateLayout();

			if( !this.Label.IsMouseHovering ) {
				this.Label.TextColor = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor) * 0.85f;
			}

			base.Update( gameTime );
		}


		////////////////

		public void UpdateLayout() {
			var mymod = IntrinsicsMod.Instance;

			int x = mymod.Config.ControlsPositionX >= 0 ?
				mymod.Config.ControlsPositionX :
				Main.screenWidth + mymod.Config.ControlsPositionX;
			int y = mymod.Config.ControlsPositionY >= 0 ?
				mymod.Config.ControlsPositionY :
				Main.screenHeight + mymod.Config.ControlsPositionY;

			this.Recalculate();

			this.ButtonOpenDialog.Left.Set( x, 0f );
			this.ButtonOpenDialog.Top.Set( y - 8f, 0f );
			this.Label.Left.Set( x + 24f, 0f );
			this.Label.Top.Set( y - 6f, 0f );

			this.Recalculate();
		}
	}
}
