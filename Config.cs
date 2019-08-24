using HamstarHelpers.Helpers.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace Intrinsics {
	public class IntrinsicsConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;


		////

		public bool DebugModeInfo = false;

		public bool DebugModeCheat = false;


		[DefaultValue( -232 )]
		public int ControlsPositionX = -232;

		[DefaultValue( 120 )]
		public int ControlsPositionY = 120;


		[DefaultValue( 3 )]
		public int BlankContractRecipeContractTattersNeeded = 3;

		public Dictionary<string, int> BlankContractAltRecipeIngredients = new Dictionary<string, int>();

		//public string BlankContractRecipeStation = "";	//TODO


		[DefaultValue( 0.015f )]
		public float GhostNpcSpawnChance = 0.015f;


		[DefaultValue( 64 )]
		public int TradeUIPositionX = 64;

		[DefaultValue( 256 )]
		public int TradeUIPositionY = 256;


		public Dictionary<string, int> TradeItemContractTatters = new Dictionary<string, int>();


		[DefaultValue( true )]
		public bool ToggleableIntrinsics = true;



		////////////////

		public override ModConfig Clone() {
			var clone = (IntrinsicsConfig)base.Clone();

			clone.BlankContractAltRecipeIngredients = this.BlankContractAltRecipeIngredients?.ToDictionary( kv => kv.Key, kv => kv.Value );
			clone.TradeItemContractTatters = this.TradeItemContractTatters?.ToDictionary( kv => kv.Key, kv => kv.Value );

			return clone;
		}

		[OnDeserialized]
		internal void OnDeserializedMethod( StreamingContext context ) {
			if( this.TradeItemContractTatters != null ) {
				return;
			}
			this.TradeItemContractTatters = new Dictionary<string, int>();

			Func<int, string> n = ( int itemId ) => ItemID.GetUniqueKey( itemId );    //TODO GetProperUniqueId

			this.TradeItemContractTatters[ n(ItemID.GPS) ] = 1;
			 this.TradeItemContractTatters[ n(ItemID.REK) ] = 1;
			 this.TradeItemContractTatters[ n(ItemID.GoblinTech) ] = 1;
			 this.TradeItemContractTatters[ n(ItemID.ArmorBracing ) ] = 1;
			 this.TradeItemContractTatters[ n(ItemID.MedicatedBandage ) ] = 1;
			 this.TradeItemContractTatters[ n(ItemID.CountercurseMantra ) ] = 1;
			 this.TradeItemContractTatters[ n(ItemID.ThePlan ) ] = 1;
			this.TradeItemContractTatters[ n(ItemID.AngelStatue) ] = 1;

			 this.TradeItemContractTatters[ n(ItemID.LavaCharm) ] = 2;
			 this.TradeItemContractTatters[ n(ItemID.WaterWalkingBoots) ] = 2;
			 this.TradeItemContractTatters[ n(ItemID.JellyfishDivingGear) ] = 2;
			 this.TradeItemContractTatters[ n(ItemID.SandstorminaBalloon) ] = 2;
			 this.TradeItemContractTatters[ n(ItemID.ArchitectGizmoPack) ] = 2;
			 this.TradeItemContractTatters[ n(ItemID.TigerClimbingGear) ] = 2;
			 this.TradeItemContractTatters[ n(ItemID.MechanicalGlove) ] = 2;
			 this.TradeItemContractTatters[ n(ItemID.FishFinder) ] = 2;
			 this.TradeItemContractTatters[ n(ItemID.GoldRing) ] = 2;
			 this.TradeItemContractTatters[ n(ItemID.LuckyCoin) ] = 2;
			 this.TradeItemContractTatters[ n(ItemID.DiscountCard) ] = 2;
			this.TradeItemContractTatters[ n(ItemID.HandWarmer) ] = 2;
			this.TradeItemContractTatters[ n(ItemID.PocketMirror) ] = 2;
			this.TradeItemContractTatters[ n(ItemID.CorruptionKey) ] = 2;
			this.TradeItemContractTatters[ n(ItemID.CrimsonKey) ] = 2;
			this.TradeItemContractTatters[ n(ItemID.FrozenKey) ] = 2;
			this.TradeItemContractTatters[ n(ItemID.HallowedKey) ] = 2;
			this.TradeItemContractTatters[ n(ItemID.JungleKey) ] = 2;

			 this.TradeItemContractTatters[n( ItemID.LightningBoots )] = 3;
			this.TradeItemContractTatters[ n(ItemID.ArcticDivingGear) ] = 3;
			this.TradeItemContractTatters[n( ItemID.MasterNinjaGear )] = 3;
			this.TradeItemContractTatters[n( ItemID.TerraBlade )] = 3;
			this.TradeItemContractTatters[ n(ItemID.FireGauntlet) ] = 3;
			this.TradeItemContractTatters[ n(ItemID.FrozenTurtleShell) ] = 3;
			this.TradeItemContractTatters[ n(ItemID.Uzi) ] = 3;
			this.TradeItemContractTatters[ n(ItemID.CoinRing) ] = 3;
			
			 this.TradeItemContractTatters[ n(ItemID.AnkhCharm ) ] = 4;
			this.TradeItemContractTatters[ n(ItemID.LavaWaders) ] = 4;
			this.TradeItemContractTatters[ n(ItemID.BundleofBalloons) ] = 4;
			this.TradeItemContractTatters[ n(ItemID.FrostsparkBoots) ] = 4;
			this.TradeItemContractTatters[ n(ItemID.RodofDiscord) ] = 4;
			
			 this.TradeItemContractTatters[ n(ItemID.PDA) ] = 5;
			this.TradeItemContractTatters[n( ItemID.AnkhShield )] = 5;
			this.TradeItemContractTatters[ n(ItemID.AmberMosquito) ] = 5;
			this.TradeItemContractTatters[ n(ItemID.CoinGun) ] = 5;
			this.TradeItemContractTatters[ n(ItemID.AnglerTackleBag) ] = 5;

			this.TradeItemContractTatters[ n(ItemID.CellPhone) ] = 6;

			this.TradeItemContractTatters[ n(ItemID.GreedyRing) ] = 7;
		}
	}
}
