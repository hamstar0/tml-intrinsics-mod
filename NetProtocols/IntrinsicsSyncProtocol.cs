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
		
		public static void SyncFromOther( int playerWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.player[playerWho] );
			var protocol = new IntrinsicsSyncProtocol();

			protocol.ItemUids = myplayer.IntrinsicItemUids.ToArray();
			protocol.SendToServer( true );
		}

		public static void SyncToMe() {
			PacketProtocolSyncClient.SyncToMe<IntrinsicsSyncProtocol>( -1 );
		}



		////////////////

		public string[] ItemUids;
		public int PlayerWho;



		////////////////

		private IntrinsicsSyncProtocol() { }

		////////////////

		protected override void InitializeClientSendData() {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );
			this.ItemUids = myplayer.IntrinsicItemUids.ToArray();
			this.PlayerWho = Main.myPlayer;
		}
		
		protected override void InitializeServerRequestReplyDataOfClient( int toWho, int fromWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.player[fromWho] );
			this.ItemUids = myplayer.IntrinsicItemUids.ToArray();
			this.PlayerWho = fromWho;
		}


		////////////////

		protected override void ReceiveOnClient() {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.player[this.PlayerWho] );
			myplayer.IntrinsicItemUids = new HashSet<string>( this.ItemUids );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.player[this.PlayerWho] );
			myplayer.IntrinsicItemUids = new HashSet<string>( this.ItemUids );
		}
	}
}
