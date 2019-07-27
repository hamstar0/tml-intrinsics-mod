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

		public IntrinsicsConfig Config => this.GetConfig<IntrinsicsConfig>();



		////////////////

		public IntrinsicsMod() {
			IntrinsicsMod.Instance = this;
		}

		////////////////

		public override void Load() {
			this.GetItem<ImpartmentContractItem>()?.LoadContent();

			if( !Main.dedServ ) {
				this.IntrinsicsDialog = new UIIntrinsicsDialog();
				this.InitializeControlsUI();
			}
		}

		public override void Unload() {
			this.GetItem<ImpartmentContractItem>()?.UnloadContent();
			IntrinsicsMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateHelpers.HandleModCall( typeof( IntrinsicsAPI ), args );
		}
	}
}
