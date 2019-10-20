using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Protocols.Packet.Interfaces;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET;
using HamstarHelpers.Helpers.TModLoader;
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

			protocol.Who = playerWho;
			protocol.ItemUids = myplayer.IntrinsicItemUids.ToArray();
			protocol.ItemStates = myplayer.IntrinsicToggle
				.SafeSelect( kv => kv.Value ? kv.Key : -kv.Key )
				.ToArray();

			protocol.SendToServer( true );
		}

		public static void SyncToMe() {
			PacketProtocolSyncClient.SyncToMe<IntrinsicsSyncProtocol>( -1 );
		}



		////////////////

		public string[] ItemUids;
		public int[] ItemStates;
		public int Who = Main.myPlayer;



		////////////////

		private IntrinsicsSyncProtocol() { }

		////////////////

		protected override void InitializeClientSendData() {
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( Main.LocalPlayer );

			this.ItemUids = myplayer.IntrinsicItemUids.ToArray();
			this.ItemStates = myplayer.IntrinsicToggle
				.SafeSelect( kv => kv.Value ? kv.Key : -kv.Key )
				.ToArray();
		}
		
		protected override void InitializeServerRequestReplyDataOfClient( int toWho, int fromWho ) {
			Player plr = Main.player[fromWho];
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( plr );

			this.Who = fromWho;
			this.ItemUids = myplayer.IntrinsicItemUids.ToArray();
			this.ItemStates = myplayer.IntrinsicToggle
				.SafeSelect( kv => kv.Value ? kv.Key : -kv.Key )
				.ToArray();
		}


		////////////////

		protected override void ReceiveOnClient() {
			Player plr = Main.player[ this.Who ];
			if( Main.myPlayer == this.Who ) {
				LogHelpers.Warn( "Receiving our own data?" );
				return;
			}

			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( plr );
			IDictionary<int, bool> itemStatesMap = this.ItemStates
				.ToDictionary( (i) => Math.Abs(i), (i) => i>=0 );

			myplayer.SyncIntrinsicItemsToMe( this.ItemUids, itemStatesMap );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			Player plr = Main.player[ this.Who ];  //fromWho
			var myplayer = TmlHelpers.SafelyGetModPlayer<IntrinsicsPlayer>( plr );
			IDictionary<int, bool> itemStatesMap = this.ItemStates
				.ToDictionary( (i) => Math.Abs(i), (i) => i>=0 );

			myplayer.SyncIntrinsicItemsToMe( this.ItemUids, itemStatesMap );
		}
	}
}
