using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using Intrinsics.NetProtocols;


namespace Intrinsics {
	partial class IntrinsicsPlayer : ModPlayer {
		internal ISet<string> IntrinsicItemUids = new HashSet<string>();

		internal IDictionary<int, Item> IntrinsicArmItem = new Dictionary<int, Item>();
		internal IDictionary<int, Item> IntrinsicAccItem = new Dictionary<int, Item>();
		internal IDictionary<int, Item> IntrinsicBuffItem = new Dictionary<int, Item>();

		internal IDictionary<int, bool> IntrinsicToggle = new Dictionary<int, bool>();

		private bool IsScribeMode = false;



		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey("item_count") ) {
				return;
			}

			int count = tag.GetInt( "item_count" );

			this.ClearIntrinsics( false );
			this.IntrinsicToggle.Clear();

			for( int i=0; i<count; i++ ) {
				string itemUid = tag.GetString( "item_" + i );
				bool isEnabled = true;

				if( tag.ContainsKey("item_"+i+"_on") ) {
					isEnabled = tag.GetBool( "item_" + i + "_on" );
				}

				try {
					this.ApplyIntrinsic( itemUid, isEnabled );
				} catch( Exception e ) {
					LogHelpers.Warn( "Error applying intrinsic for "+this.player.name+" ("+this.player.whoAmI+")"
							+" for item "+itemUid+" (on? "+isEnabled+") - "+e.ToString() );
				}
			}
		}

		public override TagCompound Save() {
			int count = this.IntrinsicItemUids.Count;
			var tag = new TagCompound { { "item_count", count } };

			int i = 0;
			foreach( string itemUid in this.IntrinsicItemUids ) {
				tag[ "item_" + i ] = itemUid;
				tag[ "item_" + i + "_on" ] = this.IntrinsicToggle[ ItemID.TypeFromUniqueKey(itemUid) ] ;
				i++;
			}

			return tag;
		}


		////////////////

		public override void clientClone( ModPlayer clientClone ) {
			var myclone = (IntrinsicsPlayer)clientClone;
			myclone.IntrinsicItemUids = new HashSet<string>( this.IntrinsicItemUids );
			myclone.IntrinsicArmItem = new Dictionary<int, Item>( this.IntrinsicArmItem );
			myclone.IntrinsicAccItem = new Dictionary<int, Item>( this.IntrinsicAccItem );
			myclone.IntrinsicBuffItem = new Dictionary<int, Item>( this.IntrinsicBuffItem );
			myclone.IntrinsicToggle = new Dictionary<int, bool>( this.IntrinsicToggle );
			myclone.IsScribeMode = this.IsScribeMode;
		}

		public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				if( newPlayer ) {
					IntrinsicsSyncProtocol.SyncFromMe();
					IntrinsicsSyncProtocol.SyncToMe();
				}
			}
			/*else if( Main.netMode == 2 ) {
				if( toWho == -1 && fromWho == this.player.whoAmI ) {
					this.OnConnectServer();
				}
			}*/
		}

		//public override void SendClientChanges( ModPlayer clientPlayer ) {
		//	var myClientPlayer = (IntrinsicsPlayer)clientPlayer;
		//
		//	if( this.IntrinsicItemUids.Count != myClientPlayer.IntrinsicItemUids.Count ) {
		//		IntrinsicsSyncProtocol.SyncFromMe();
		//	}
		//}


		////////////////

		public override void OnEnterWorld( Player player ) {
			if( player.whoAmI != Main.myPlayer ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			if( Main.netMode == NetmodeID.SinglePlayer ) {
				this.OnConnectSingle();
			} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
				this.OnConnectClient();
			}

			//this.TestItem = new Item();
			//this.TestItem.SetDefaults( ItemID.WingsSolar );
		}


		////////////////

		private void OnConnectSingle() {
		}

		private void OnConnectClient() {
			IntrinsicsSyncProtocol.SyncFromMe();
		}

		private void OnConnectServer() {
		}


		////////////////
		
		public override void ProcessTriggers( TriggersSet triggersSet ) {
			if( IntrinsicsMod.Instance.ControlPanelHotkey.JustPressed ) {
				IntrinsicsMod.Instance.ControlPanelDialog.Open();
			}
		}
	}
}
