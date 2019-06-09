using HamstarHelpers.Helpers.ItemHelpers;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace Intrinsics.Items {
	public partial class ImpartmentContractItem : ModItem {
		public static int Create( Player player, ISet<string> itemUids ) {
			int itemIdx = ItemHelpers.CreateItem( player.Center, IntrinsicsMod.Instance.ItemType<ImpartmentContractItem>(), 1, 24, 24 );
			var myitem = Main.item[ itemIdx ].modItem as ImpartmentContractItem;

			if( myitem != null ) {
				myitem.IntrinsicItemUids = itemUids;
			} else {
				itemIdx = -1;
			}

			return itemIdx;
		}



		////////////////

		private ISet<string> IntrinsicItemUids;



		////////////////

		public override bool CloneNewInstances => true;



		////////////////

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
			this.Tooltip.SetDefault( "This contract will impart intrinsic effects when agreed to.\n"+
				"Intrinsic impartments are permanent.\n"+
				"Sometimes more than one impartment may occur, good or bad."
			);
		}

		public override void SetDefaults() {
			this.item.width = 24;
			this.item.height = 24;
			this.item.value = 10000;
			this.item.rare = 2;
			this.item.consumable = true;
			this.item.useStyle = 4;
			this.item.useTime = 30;
			this.item.useAnimation = 30;
			this.item.UseSound = SoundID.Item4;
		}
	}
}
