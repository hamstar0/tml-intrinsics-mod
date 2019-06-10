using HamstarHelpers.Helpers.TmlHelpers;
using Libraries.Intrinsics.Components.Protocol.Packet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace Intrinsics.NetProtocols {
	class IntrinsicsSyncProtocol : PacketProtocolSyncClient {
		public static void SyncFromMe() {
			PacketProtocolSyncClient.SyncFromMe<IntrinsicsSyncProtocol>();
		}
		public static void SyncToMe() {
			PacketProtocolSyncClient.SyncToMe<IntrinsicsSyncProtocol>( -1 );
		}



		////////////////

		public string[] ItemUids;



		////////////////

		private IntrinsicsSyncProtocol() { }

		////////////////

		protected override void InitializeClientSendData() {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );
			this.ItemUids = myplayer.IntrinsicItemUids.ToArray();
		}
		
		protected override void InitializeServerRequestReplyDataOfClient( int toWho, int fromWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.player[fromWho] );
			this.ItemUids = myplayer.IntrinsicItemUids.ToArray();
		}


		////////////////

		protected override void Receive( int fromWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );
			myplayer.IntrinsicItemUids = new HashSet<string>( this.ItemUids );
		}
	}
}
