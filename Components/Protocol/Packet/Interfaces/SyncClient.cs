﻿using HamstarHelpers.Components.Errors;
using HamstarHelpers.Components.Network;
using System;
using Terraria;


namespace Intrinsics.Components.Protocol.Packet.Interfaces {
	public abstract class PacketProtocolSyncClient : PacketProtocol {
		public static void SyncFromMe<T>() where T : PacketProtocolSyncClient {
			PacketProtocol.QuickSendToServer<T>();
		}
		public static void SyncToMe<T>( int retries ) where T : PacketProtocolSyncClient {
			PacketProtocol.QuickRequestToServer<T>( retries );
		}



		////////////////

		protected sealed override bool ReceiveRequestWithServer( int fromWho ) {
			for( int i=0; i<Main.player.Length; i++ ) {
				if( i == fromWho ) { continue; }

				this.InitializeServerRequestReplyDataOfClient( fromWho, i );
				this.OnClone();

				this.SendToClient( fromWho, -1 );
			}

			return true;
		}

		////////////////

		protected abstract void InitializeClientSendData();
		protected sealed override void SetClientDefaults() {
			this.InitializeClientSendData();
		}

		protected abstract void InitializeServerRequestReplyDataOfClient( int toWho, int fromWho );
		protected sealed override void SetServerDefaults( int toWho ) {
		}

		protected abstract void Receive( int fromWho );
		protected sealed override void ReceiveWithServer( int fromWho ) {
			this.Receive( fromWho );
		}


		////////////////

		protected sealed override void ReceiveWithClient() {
			throw new HamstarException( "Not implemented" );
		}
		protected sealed override bool ReceiveRequestWithClient() {
			throw new HamstarException( "Not implemented" );
		}
	}
}
