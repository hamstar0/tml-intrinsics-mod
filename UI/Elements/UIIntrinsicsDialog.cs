using HamstarHelpers.Components.UI;
using HamstarHelpers.Components.UI.Elements;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
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



		////////////////

		public UIIntrinsicsDialog() : base( UITheme.Vanilla, 360, 420 ) { }


		////////////////

		public override void InitializeComponents() {
			var title = new UIText( "Active intrinsics by item:" );
			this.InnerContainer.Append( (UIElement)title );

			////

			var buffListWrap = new UIPanel();
			buffListWrap.Top.Set( 32f, 0f );
			buffListWrap.Height.Set( 120f, 0f );
			buffListWrap.Width.Set( 0f, 1f );
			this.InnerContainer.Append( (UIElement)buffListWrap );

			var armListWrap = new UIPanel();
			armListWrap.Top.Set( 280f, 0f );
			armListWrap.Height.Set( 120f, 0f );
			armListWrap.Width.Set( 0f, 1f );
			this.InnerContainer.Append( (UIElement)armListWrap );

			var accListWrap = new UIPanel();
			accListWrap.Top.Set( 156f, 0f );
			accListWrap.Height.Set( 120f, 0f );
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
			var elem = new UIPanel();
			elem.Width.Set( -16f, 1f );
			elem.Height.Set( 40f, 0f );

			var img = new UIImage( Main.itemTexture[item.type] );
			img.Top.Set( -10f, 0f );
			img.Left.Set( -8f, 0f );
			img.Width.Set( 24f, 0f );
			img.Height.Set( 24f, 0f );

			var label = new UIText( item.HoverName );
			label.Top.Set( -4f, 0f );
			label.Left.Set( 24f, 0f );

			elem.Append( img );
			elem.Append( label );

			return elem;
		}
	}
}
