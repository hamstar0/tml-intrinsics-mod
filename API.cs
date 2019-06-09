using Terraria;


namespace Intrinsics {
	public static partial class IntrinsicsAPI {
		public static IntrinsicsConfigData GetModSettings() {
			return IntrinsicsMod.Instance.Config;
		}

		public static void SaveModSettingsChanges() {
			IntrinsicsMod.Instance.ConfigJson.SaveFile();
		}
	}
}
