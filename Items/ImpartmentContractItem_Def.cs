using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.ItemHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace Intrinsics.Items {
	public partial class ImpartmentContractItem : ModItem {
		private static Texture2D Overlay = null;



		////////////////

		public static int Create( Player player, Vector2 position, ISet<string> itemUids ) {
			if( itemUids.Count == 0 ) {
//Main.NewText( "No ImpartmentContractItem created for "+player.name+" (no items)" );
				LogHelpers.Warn( "No ImpartmentContractItem created for " + player.name + " (no items)" );
				return -1;
			}

			int itemIdx = Item.NewItem( position, IntrinsicsMod.Instance.ItemType<ImpartmentContractItem>() );
			Item item = Main.item[itemIdx];
			if( item == null || item.IsAir ) {
//Main.NewText( "No ImpartmentContractItem created for "+player.name );
				LogHelpers.Warn( "No ImpartmentContractItem created for " + player.name );
				return -1;
			}

			var myitem = item.modItem as ImpartmentContractItem;
			if( myitem != null ) {
				myitem.IntrinsicItemUids = itemUids;
			} else {
//Main.NewText( "Invalid ImpartmentContractItem created for "+player.name );
				LogHelpers.Warn( "Invalid ImpartmentContractItem created for " + player.name );
				itemIdx = -1;
			}

			if( Main.netMode == 1 && itemIdx != -1 ) {
				NetMessage.SendData( MessageID.SyncItem, -1, Main.myPlayer, null, itemIdx, 1 );
			}

			return itemIdx;
		}



		////////////////

		private ISet<string> IntrinsicItemUids = new HashSet<string>();



		////////////////
		
		public override bool CloneNewInstances => true;



		////////////////

		internal void LoadContent() {
			ImpartmentContractItem.Overlay = ModLoader.GetTexture( this.Texture + "_Overlay" );
		}

		internal void UnloadContent() {
			ImpartmentContractItem.Overlay = null;
		}

		////

		public override ModItem Clone() {
			var clone = (ImpartmentContractItem)base.Clone();
			clone.IntrinsicItemUids = this.IntrinsicItemUids;
			return clone;
		}


		////////////////
		
		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey( "item_count" ) ) {
				return;
			}

			int items = tag.GetInt( "item_count" );

			this.IntrinsicItemUids.Clear();

			for( int i = 0; i < items; i++ ) {
				this.IntrinsicItemUids.Add( tag.GetString( "item_" + i ) );
			}
		}

		public override TagCompound Save() {
			int items = this.IntrinsicItemUids.Count;
			var tags = new TagCompound {
				{ "item_count", items }
			};

			int i = 0;
			foreach( string itemUid in this.IntrinsicItemUids ) {
				tags["item_" + i++] = itemUid;
			}

			return tags;
		}

		public override void NetRecieve( BinaryReader reader ) {
			this.IntrinsicItemUids.Clear();

			int items = reader.ReadInt32();

			for( int i = 0; i < items; i++ ) {
				this.IntrinsicItemUids.Add( reader.ReadString() );
			}
		}

		public override void NetSend( BinaryWriter writer ) {
			writer.Write( (int)this.IntrinsicItemUids.Count );

			foreach( string itemUid in this.IntrinsicItemUids ) {
				writer.Write( itemUid );
			}
		}


		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Impartment Contract" );
			this.Tooltip.SetDefault( "This contract will impart intrinsic effects when used.\n" +
				"The item(s) used to make this contract define the intrinsic(s) applied.\n" +
				"An intrinsic works exactly like equipping or using an item, but implicitly.\n" +
				"Intrinsic impartments are permanent."
			//"Sometimes more than one impartment may occur, good or bad."
			);
		}

		public override void SetDefaults() {
			this.item.width = 32;
			this.item.height = 32;
			this.item.value = Item.buyPrice( 1, 0, 0, 0 );
			this.item.rare = ItemAttributeHelpers.HighestVanillaRarity;
			this.item.consumable = true;
			this.item.useStyle = 4;
			this.item.useTime = 30;
			this.item.useAnimation = 30;
			this.item.UseSound = SoundID.Item4;
		}
	}
}
