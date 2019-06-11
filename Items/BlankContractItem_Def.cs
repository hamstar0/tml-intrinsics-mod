using HamstarHelpers.Helpers.ItemHelpers;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace Intrinsics.Items {
	public partial class BlankContractItem : ModItem {
		private ISet<string> IntrinsicItemUids = new HashSet<string>();



		////////////////

		public int MyLastInventoryPosition { get; private set; }

		public override bool CloneNewInstances => false;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Blank Contract" );
			this.Tooltip.SetDefault( "Write your own destiny.\n"+
				"Drop a valid item onto this contract to bind it."
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


		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey( "item_count" ) ) {
				return;
			}

			int items = tag.GetInt( "item_count" );

			this.IntrinsicItemUids.Clear();

			for( int i=0; i<items; i++ ) {
				this.IntrinsicItemUids.Add( tag.GetString("item_"+i) );
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

		public override void UpdateInventory( Player player ) {
			this.MyLastInventoryPosition = -1;

			for( int i = 0; i < player.inventory.Length; i++ ) {
				if( player.inventory[i] == this.item ) {
					this.MyLastInventoryPosition = i;
					break;
				}
			}
		}
	}
}
