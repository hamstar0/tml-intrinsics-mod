using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;


namespace Intrinsics {
	partial class IntrinsicsPlayer : ModPlayer {
		private void UpdateIntrinsicBuffs() {
			foreach( Item item in this.IntrinsicBuffItem.Values ) {
				if( !this.IntrinsicToggle.GetOrDefault( item.type ) ) {
					continue;
				}

				int buffIdx = this.player.FindBuffIndex( item.buffType );
				if( buffIdx == -1 ) {
					this.player.AddBuff( item.buffType, 3 );
				} else if( buffIdx >= 0 && buffIdx < this.player.buffTime.Length ) {
					this.player.buffTime[buffIdx] = 3;
				}
			}
		}

		private void UpdateIntrinsicEquips() {
			bool _ = false;
			foreach( Item item in this.IntrinsicAccItem.Values ) {
				if( !this.IntrinsicToggle.GetOrDefault( item.type ) ) {
					continue;
				}
				this.player.VanillaUpdateEquip( item );
				this.player.VanillaUpdateAccessory( this.player.whoAmI, item, false, ref _, ref _, ref _ );
			}

			foreach( Item item in this.IntrinsicArmItem.Values ) {
				if( !this.IntrinsicToggle.GetOrDefault( item.type ) ) {
					continue;
				}
				this.player.VanillaUpdateEquip( item );
			}
			//VanillaUpdateInventory( this.inventory[j] );
		}
	}
}
