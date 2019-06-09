using HamstarHelpers.Components.Config;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;


namespace Intrinsics {
	partial class IntrinsicsMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-intrinsics-mod";

		public static string ConfigFileRelativePath {
			get { return ConfigurationDataBase.RelativePath + Path.DirectorySeparatorChar + IntrinsicsConfigData.ConfigFileName; }
		}
		public static void ReloadConfigFromFile() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reload configs outside of single player." );
			}
			if( !IntrinsicsMod.Instance.ConfigJson.LoadFile() ) {
				IntrinsicsMod.Instance.ConfigJson.SaveFile();
			}
		}

		public static void ResetConfigFromDefaults() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reset to default configs outside of single player." );
			}

			var configData = new IntrinsicsConfigData();
			configData.SetDefaults();

			IntrinsicsMod.Instance.ConfigJson.SetData( configData );
			IntrinsicsMod.Instance.ConfigJson.SaveFile();
		}
	}
}
