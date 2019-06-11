using HamstarHelpers.Components.Config;
using System;


namespace Intrinsics {
	public class IntrinsicsConfigData : ConfigurationDataBase {
		public readonly static string ConfigFileName = "Intrinsics Config.json";



		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugModeInfo = false;

		public string[] BlankContractRecipeIngredients = new string[0];
		//public string BlankContractRecipeStation = "";	//TODO

		public float GhostNpcSpawnChance = 0.015f;

		public int TradeUIPositionX = 64;
		public int TradeUIPositionY = 256;



		////////////////

		public void SetDefaults() {
		}


		////////////////

		public bool UpdateToLatestVersion() {
			var mymod = IntrinsicsMod.Instance;
			var newConfig = new IntrinsicsConfigData();
			newConfig.SetDefaults();

			var versSince = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( versSince >= mymod.Version ) {
				return false;
			}

			if( this.VersionSinceUpdate == "" ) {
				this.SetDefaults();
			}

			this.VersionSinceUpdate = mymod.Version.ToString();

			return true;
		}
	}
}
