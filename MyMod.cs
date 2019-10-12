using HamstarHelpers.Helpers.ModHelpers;
using HamstarHelpers.Helpers.TModLoader.Mods;
using Intrinsics.Items;
using Intrinsics.UI.Elements;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics {
	partial class IntrinsicsMod : Mod {
		public static IntrinsicsMod Instance { get; private set; }



		////////////////
		
		internal Item TradeItem = new Item();
		internal bool IsTrading = false;
		internal UIIntrinsicsDialog IntrinsicsDialog;

		////////////////

		public IntrinsicsConfig Config => ModContent.GetInstance<IntrinsicsConfig>();



		////////////////

		public IntrinsicsMod() {
			IntrinsicsMod.Instance = this;
		}

		////////////////

		public override void Load() {
			ModContent.GetInstance<ImpartmentContractItem>()?.LoadContent();

			if( !Main.dedServ ) {
				this.IntrinsicsDialog = new UIIntrinsicsDialog();
				this.InitializeControlsUI();
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
