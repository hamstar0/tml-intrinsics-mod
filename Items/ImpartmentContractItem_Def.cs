using HamstarHelpers.Helpers.ItemHelpers;
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

		public static int Create( Player player, ISet<string> itemUids ) {
			int itemIdx = ItemHelpers.CreateItem( player.Center, IntrinsicsMod.Instance.ItemType<ImpartmentContractItem>(), 1, 24, 24 );
			var myitem = Main.item[itemIdx].modItem as ImpartmentContractItem;

			if( myitem != null ) {
				myitem.IntrinsicItemUids = itemUids;
			} else {
				itemIdx = -1;
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
			this.Tooltip.SetDefault( "This contract will impart intrinsic effects when agreed to.\n" +
				"Intrinsic impartments are permanent.\n" +
				"Sometimes more than one impartment may occur, good or bad."
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
