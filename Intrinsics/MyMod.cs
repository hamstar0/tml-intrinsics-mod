using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.ID;
using HamstarHelpers.Helpers.TModLoader.Mods;
using Intrinsics.Items;
using Intrinsics.UI;
using Intrinsics.UI.Elements;


namespace Intrinsics {
	partial class IntrinsicsMod : Mod {
		public static IntrinsicsMod Instance { get; private set; }



		////////////////
		
		internal Item TradeItem = new Item();
		internal bool IsTrading = false;
		internal UIIntrinsicsDialog ControlPanelDialog;

		internal ModHotKey ControlPanelHotkey;

		private UserInterface UIContext;
		internal UIIntrinsicsHUD HUDComponents;

		private Texture2D BlankContractTex;


		////////////////

		public IntrinsicsConfig Config => ModContent.GetInstance<IntrinsicsConfig>();



		////////////////

		public IntrinsicsMod() {
			IntrinsicsMod.Instance = this;
		}

		////////////////

		public override void Load() {
			ModContent.GetInstance<ImpartmentContractItem>()?.LoadContent();

			this.ControlPanelHotkey = this.RegisterHotKey( "Control Panel", "P" );

			// All code below runs only if we're not loading on a server
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.ControlPanelDialog = new UIIntrinsicsDialog();
				this.InitializeUI();
			}
		}

		public override void Unload() {
			ModContent.GetInstance<ImpartmentContractItem>()?.UnloadContent();
			IntrinsicsMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateHelpers.HandleModCall( typeof( IntrinsicsAPI ), args );
		}
	}
}
