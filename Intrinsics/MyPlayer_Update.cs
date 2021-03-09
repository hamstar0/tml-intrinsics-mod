using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Intrinsics.NPCs;


namespace Intrinsics {
	partial class IntrinsicsPlayer : ModPlayer {
		public override void PreUpdate() {
			Player plr = this.player;
			//if( plr.whoAmI != Main.myPlayer ) { return; }
			//if( plr.dead ) { return; }

			if( plr.whoAmI == Main.myPlayer ) {
				WanderingGhostNPC.UpdateTradingState();
			}

			this.UpdateIntrinsicBuffs();
		}

		public override void PostUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			this.UpdateContractInteractionsIf();
		}

		public override void UpdateAutopause() {
			if( !Main.gamePaused ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			this.UpdateContractInteractionsIf();
		}


		public override void UpdateEquips( ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff ) {
			this.UpdateIntrinsicEquips();
		}


		////////////////

		private void UpdateContractInteractionsIf() {
			if( Main.mouseItem?.IsAir != false ) {
				return;
			}

//DebugHelpers.Print("uci", "mouse:"+Main.mouseItem.HoverName+", held:"+plr.HeldItem?.HoverName+", PrevSelectedItem:"+this.PrevSelectedItem?.HoverName);
			if( this.IsScribeMode ) {
				this.IsScribeMode = false;

				IntrinsicsPlayer.ScribeBlankContractIf( this.player, Main.mouseItem );
			}
		}
	}
}
