using HamstarHelpers.Components.Network;


namespace Intrinsics.NetProtocols {
	class ModSettingsProtocol : PacketProtocolRequestToServer {
		public static void QuickRequest() {
			PacketProtocolRequestToServer.QuickRequest<ModSettingsProtocol>( -1 );
		}



		////////////////

		public IntrinsicsConfigData ModSettings;



		////////////////

		private ModSettingsProtocol() { }


		////////////////

		protected override void InitializeServerSendData( int who ) {
			this.ModSettings = IntrinsicsMod.Instance.Config;
		}

		////////////////
		
		protected override void ReceiveReply() {
			IntrinsicsMod.Instance.ConfigJson.SetData( this.ModSettings );
		}
	}
}
