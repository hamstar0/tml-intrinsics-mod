using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace Intrinsics.UI.Elements {
	class UIIntrinsicsDialog : UIDialog {
		private UIList BuffList;
		private UIList ArmList;
		private UIList AccList;
		private UIPanel CloseButton;



		////////////////

		public UIIntrinsicsDialog() : base( UITheme.Vanilla, 480, 560 ) { }


		////////////////

		public override void InitializeComponents() {
			Color bgColor = this.Theme.ButtonBgColor;

			////

			var closeText = new UIText( "X" );
			closeText.Left.Set( -2f, 0f );
			closeText.Top.Set( -4f, 0f );

			this.CloseButton = new UIPanel();
			this.CloseButton.Height.Set( 32f, 0f );
			this.CloseButton.Width.Set( 32f, 0f );
			this.CloseButton.Left.Set( -24f, 1f );
			this.CloseButton.Top.Set( -8f, 0f );
			this.CloseButton.Append( closeText );
			this.CloseButton.OnMouseOver += ( _, __ ) => {
				this.CloseButton.BackgroundColor = bgColor * 1.3f;
			};
			this.CloseButton.OnMouseOut += ( _, __ ) => {
				this.CloseButton.BackgroundColor = bgColor;
			};
			this.CloseButton.OnClick += ( _, __ ) => {
				this.Close();
			};
			this.InnerContainer.Append( this.CloseButton );

			////

			var title = new UIText( "Active intrinsics by item:" );
			this.InnerContainer.Append( (UIElement)title );

			////

			var buffListWrap = new UIPanel();
			buffListWrap.Top.Set( 24f, 0f );
			buffListWrap.Height.Set( 172f, 0f );
			buffListWrap.Width.Set( 0f, 1f );
			this.InnerContainer.Append( (UIElement)buffListWrap );

			var armListWrap = new UIPanel();
			armListWrap.Top.Set( 196f, 0f );
			armListWrap.Height.Set( 172f, 0f );
			armListWrap.Width.Set( 0f, 1f );
			this.InnerContainer.Append( (UIElement)armListWrap );

			var accListWrap = new UIPanel();
			accListWrap.Top.Set( 368f, 0f );
			accListWrap.Height.Set( 172f, 0f );
			accListWrap.Width.Set( 0f, 1f );
			this.InnerContainer.Append( (UIElement)accListWrap );

			////

			this.BuffList = new UIList();
			this.BuffList.Width.Set( 0f, 1f );
			this.BuffList.Height.Set( 0f, 1f );
			this.BuffList.ListPadding = 4f;
			buffListWrap.Append( this.BuffList );

			this.ArmList = new UIList();
			this.ArmList.Width.Set( 0f, 1f );
			this.ArmList.Height.Set( 0f, 1f );
			this.ArmList.ListPadding = 4f;
			armListWrap.Append( this.ArmList );

			this.AccList = new UIList();
			this.AccList.Width.Set( 0f, 1f );
			this.AccList.Height.Set( 0f, 1f );
			this.AccList.ListPadding = 4f;
			accListWrap.Append( this.AccList );

			////

			var buffListScroll = new UIScrollbar();
			buffListScroll.Top.Set( 0f, 0f );
			buffListScroll.Height.Set( 0f, 1f );
			buffListScroll.SetView( 100f, 1000f );
			buffListScroll.HAlign = 1f;
			this.BuffList.SetScrollbar( buffListScroll );
			buffListWrap.Append( (UIElement)buffListScroll );

			var armListScroll = new UIScrollbar();
			armListScroll.Top.Set( 0f, 0f );
			armListScroll.Height.Set( 0f, 1f );
			armListScroll.SetView( 100f, 1000f );
			armListScroll.HAlign = 1f;
			this.ArmList.SetScrollbar( armListScroll );
			armListWrap.Append( (UIElement)armListScroll );

			var accListScroll = new UIScrollbar();
			accListScroll.Top.Set( 0f, 0f );
			accListScroll.Height.Set( 0f, 1f );
			accListScroll.SetView( 100f, 1000f );
			accListScroll.HAlign = 1f;
			this.AccList.SetScrollbar( accListScroll );
			accListWrap.Append( (UIElement)accListScroll );

			////

			//this.UpdateLists();
		}


		////////////////

		public override void Open() {
			base.Open();

			this.UpdateLists();
		}


		////////////////

		public void UpdateLists() {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );

			////
			
			var buffs = myplayer.IntrinsicBuffItem.Values
				.Select( item => this.GetItemIntrinsic( item ) )
				.ToList();
			var arms = myplayer.IntrinsicArmItem.Values
				.Select( item => this.GetItemIntrinsic( item ) )
				.ToList();
			var accs = myplayer.IntrinsicAccItem.Values
				.Select( item => this.GetItemIntrinsic( item ) )
				.ToList();

			////

			this.BuffList.Clear();
			this.ArmList.Clear();
			this.AccList.Clear();

			this.BuffList.Recalculate();
			this.ArmList.Recalculate();
			this.AccList.Recalculate();

			this.BuffList.AddRange( buffs );
			this.ArmList.AddRange( arms );
			this.AccList.AddRange( accs );
		}


		////////////////

		private UIElement GetItemIntrinsic( Item item ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );
			bool isEnabled = myplayer.IntrinsicToggle.GetOrDefault( item.type );

			return new UIIntrinsicItemButton( item, isEnabled );
		}


		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			foreach( UIElement elem in this.BuffList._items ) {
				if( !(elem is UIIntrinsicItemButton) ) { continue; }
				((UIIntrinsicItemButton)elem).DrawOver();
			}
			foreach( UIElement elem in this.ArmList._items ) {
				if( !( elem is UIIntrinsicItemButton ) ) { continue; }
				( (UIIntrinsicItemButton)elem ).DrawOver();
			}
			foreach( UIElement elem in this.AccList._items ) {
				if( !( elem is UIIntrinsicItemButton ) ) { continue; }
				( (UIIntrinsicItemButton)elem ).DrawOver();
			}
		}
	}
}
