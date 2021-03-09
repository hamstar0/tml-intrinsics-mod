using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.Items.Attributes;
using Intrinsics.NetProtocols;


namespace Intrinsics {
	partial class IntrinsicsPlayer : ModPlayer {
		private Item LoadIntrinsicItem( int itemId ) {
			var item = new Item();
			item.SetDefaults( itemId );

			this.LoadIntrinsicItemInternal( item, true );

			return item;
		}

		private void LoadIntrinsicItemInternal( Item item, bool itemState ) {
			switch( IntrinsicsLogic.GetItemIntrinsicType( item ) ) {
			case IntrinsicType.Armor:
				this.IntrinsicArmItem[ item.type ] = item;
				break;
			case IntrinsicType.Accessory:
				this.IntrinsicAccItem[ item.type ] = item;
				break;
			case IntrinsicType.Buff:
				this.IntrinsicBuffItem[ item.type ] = item;
				break;
			}

			this.IntrinsicToggle[ item.type ] = itemState;
		}

		/////

		public void SyncIntrinsicItemsToMe( IEnumerable<string> itemUids, IDictionary<int, bool> itemStates ) {
			this.IntrinsicItemUids = new HashSet<string>( itemUids );

			foreach( string itemUid in itemUids ) {
				int itemType = ItemID.TypeFromUniqueKey( itemUid );

				bool itemState;
				itemStates.TryGetValue( itemType, out itemState );

				var item = new Item();
				item.SetDefaults( itemType );

				this.LoadIntrinsicItemInternal( item, itemState );
			}
		}


		////////////////////

		public void ApplyIntrinsic( string itemUid, bool isEnabled ) {
			this.IntrinsicItemUids.Add( itemUid );

			int itemId = ItemID.TypeFromUniqueKey( itemUid );
			if( itemId != 0 ) {
				Item item = this.LoadIntrinsicItem( itemId );
				string colorHex = ItemRarityAttributeHelpers.RarityColor[item.rare].Hex3();

				Main.NewText( "The deal is made. Imparting [c/" + colorHex + ":" + item.HoverName + "]..." );
			}

			this.IntrinsicToggle[ itemId ] = isEnabled;

			if( Main.netMode == 1 ) {
				if( this.player.whoAmI == Main.myPlayer ) {
					IntrinsicsSyncProtocol.SyncFromMe();
				} else {
					IntrinsicsSyncProtocol.SyncFromOther( this.player.whoAmI );
				}
			}
		}


		public void RemoveIntrinsic( string itemUid ) {
			this.IntrinsicItemUids.Remove( itemUid );

			int itemId = ItemID.TypeFromUniqueKey( itemUid );
			if( itemId != 0 ) {
				if( this.IntrinsicBuffItem.ContainsKey( itemId ) ) {
					this.IntrinsicBuffItem.Remove( itemId );
				}
				if( this.IntrinsicArmItem.ContainsKey( itemId ) ) {
					this.IntrinsicArmItem.Remove( itemId );
				}
				if( this.IntrinsicAccItem.ContainsKey( itemId ) ) {
					this.IntrinsicAccItem.Remove( itemId );
				}
			}
		}


		////////////////

		public void ClearIntrinsics( bool sync ) {
			this.IntrinsicAccItem.Clear();
			this.IntrinsicArmItem.Clear();
			this.IntrinsicBuffItem.Clear();
			this.IntrinsicItemUids.Clear();
			this.IntrinsicToggle.Clear();

			if( sync && Main.netMode == 1 ) {
				if( this.player.whoAmI == Main.myPlayer ) {
					IntrinsicsSyncProtocol.SyncFromMe();
				} else {
					IntrinsicsSyncProtocol.SyncFromOther( this.player.whoAmI );
				}
			}
		}

		public bool ToggleIntrinsic( int itemType ) {
			if( !this.IntrinsicToggle.ContainsKey(itemType) ) {
				return false;
			}

			this.IntrinsicToggle[itemType] = !this.IntrinsicToggle[itemType];

			if( Main.netMode == 1 ) {
				if( this.player.whoAmI == Main.myPlayer ) {
					IntrinsicsSyncProtocol.SyncFromMe();
				} else {
					IntrinsicsSyncProtocol.SyncFromOther( this.player.whoAmI );
				}
			}

			return this.IntrinsicToggle[itemType];
		}
	}
}
