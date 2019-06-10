using HamstarHelpers.Components.Errors;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.WorldHelpers;
using Intrinsics.Libraries.Helpers.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Intrinsics.NPCs {
	[AutoloadHead]
	partial class GhostWanderingNPC : ModNPC {
		public const string ChatText = "...";

		////

		public static int MyType { get; private set; }



		////////////////

		public static bool CanTrade() {
			var mymod = IntrinsicsMod.Instance;

			int npcWho = Main.LocalPlayer.talkNPC;
			if( npcWho == -1 ) {
				return false;
			}

			NPC npc = Main.npc[ npcWho ];
			if( npc == null || !npc.active || npc.type != GhostWanderingNPC.MyType ) {
				return false;
			}

			return true;
		}



		////////////////

		public override string Texture => "Intrinsics/NPCs/GhostWanderingNPC";



		////////////////

		public override bool Autoload( ref string name ) {
			name = "???";
			return this.mod.Properties.Autoload;
		}

		public override void SetStaticDefaults() {
			int npcType = this.npc.type;
			GhostWanderingNPC.MyType = npcType;

			this.DisplayName.SetDefault( "Ghost" );

			Main.npcFrameCount[npcType] = 26;
			NPCID.Sets.AttackFrameCount[npcType] = 5;
			NPCID.Sets.DangerDetectRange[npcType] = 700;
			NPCID.Sets.AttackType[npcType] = 1;
			NPCID.Sets.AttackTime[npcType] = 30;
			NPCID.Sets.AttackAverageChance[npcType] = 30;
			NPCID.Sets.HatOffsetY[npcType] = 4;
		}

		public override void SetDefaults() {
			int npcType = this.npc.type;

			this.npc.townNPC = true;
			this.npc.friendly = true;
			this.npc.width = 18;
			this.npc.height = 40;
			this.npc.aiStyle = 7;
			this.npc.damage = 1;
			this.npc.defense = 15;
			this.npc.lifeMax = 250;
			this.npc.HitSound = SoundID.NPCHit1;
			this.npc.DeathSound = SoundID.NPCDeath1;
			this.npc.knockBackResist = 0.5f;
			this.npc.dontTakeDamage = true;
			this.npc.dontTakeDamageFromHostiles = true;

			this.animationType = NPCID.Guide;
		}

		public override void TownNPCAttackStrength( ref int damage, ref float knockback ) {
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown( ref int cooldown, ref int randExtraCooldown ) {
			cooldown = 30;
			randExtraCooldown = 30;
		}

		public override void TownNPCAttackProj( ref int projType, ref int attackDelay ) {
			projType = 0;//1;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed( ref float multiplier, ref float gravityCorrection, ref float randomOffset ) {
			multiplier = 12f;
			randomOffset = 2f;
		}

		public override string TownNPCName() {
			return "???";
			//return "The Lunatic";
		}

		public override bool UsesPartyHat() {
			return false;   // :(
		}

		public override void SetChatButtons( ref string button, ref string button2 ) {
			button = "Give rare item";
		}

		////////////////

		public override string GetChat() {
			return GhostWanderingNPC.ChatText;
		}


		////////////////

		public override float SpawnChance( NPCSpawnInfo spawnInfo ) {
			if( WorldHelpers.IsRockLayer(spawnInfo.player.position) ) {
				var mymod = (IntrinsicsMod)this.mod;
				return mymod.Config.GhostNpcSpawnChance;
			}
			return 0f;
		}

		public override bool CheckActive() {
			float dist;
			int playerIdx = this.npc.FindClosestPlayer( out dist );

			if( dist >= 1300 ) {
				NPCHelpers.Remove( npc );
				return false;
			}

			return base.CheckActive();
		}


		////////////////

		public override Color? GetAlpha( Color drawColor ) {
			var color = Color.Lerp(new Color( 128, 255, 255 ), drawColor, 0.5f) * 0.35f;
			return color;
		}


		////////////////

		public override void OnChatButtonClicked( bool firstButton, ref bool shop ) {
			if( firstButton ) {
				var mymod = (IntrinsicsMod)this.mod;
				mymod.IsTrading = true;
			}
		}
	}
}