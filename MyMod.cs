using HamstarHelpers.Components.Config;
using HamstarHelpers.Helpers.TmlHelpers.ModHelpers;
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

		public JsonConfig<IntrinsicsConfigData> ConfigJson { get; private set; }
		public IntrinsicsConfigData Config => this.ConfigJson.Data;



		////////////////

		public IntrinsicsMod() {
			this.ConfigJson = new JsonConfig<IntrinsicsConfigData>(
				IntrinsicsConfigData.ConfigFileName,
				ConfigurationDataBase.RelativePath,
				new IntrinsicsConfigData()
			);
		}

		////////////////

		public override void Load() {
			IntrinsicsMod.Instance = this;

			this.GetItem<ImpartmentContractItem>()?.LoadContent();

			this.LoadConfig();

			if( !Main.dedServ ) {
				this.IntrinsicsDialog = new UIIntrinsicsDialog();
				this.InitializeControlsUI();
			}
		}

		private void LoadConfig() {
			if( !this.ConfigJson.LoadFile() ) {
				this.Config.SetDefaults();
				this.ConfigJson.SaveFile();
				ErrorLogger.Log( "Intrinsics config " + this.Version.ToString() + " created." );
			}

			if( this.Config.UpdateToLatestVersion() ) {
				ErrorLogger.Log( "Intrinsics updated to " + this.Version.ToString() );
				this.ConfigJson.SaveFile();
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
