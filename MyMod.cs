using HamstarHelpers.Components.Config;
using HamstarHelpers.Helpers.TmlHelpers.ModHelpers;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics {
	partial class IntrinsicsMod : Mod {
		public static IntrinsicsMod Instance { get; private set; }



		////////////////
		
		public JsonConfig<IntrinsicsConfigData> ConfigJson { get; private set; }
		public IntrinsicsConfigData Config => this.ConfigJson.Data;

		internal Item TradeItem = new Item();
		internal bool IsTrading = false;



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

			this.LoadConfig();
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
			IntrinsicsMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateHelpers.HandleModCall( typeof( IntrinsicsAPI ), args );
		}
	}
}
