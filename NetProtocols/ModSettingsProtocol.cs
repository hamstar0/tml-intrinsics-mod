using HamstarHelpers.Components.Network;


namespace Intrinsics.NetProtocols {
	class ModSettingsProtocol : PacketProtocolRequestToServer {
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
