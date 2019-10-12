using HamstarHelpers.Classes.UI.ModConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace Intrinsics {
	class MyFloatInputElement : FloatInputElement { }





	public class IntrinsicsConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;


		////

		public bool DebugModeInfo = false;

		public bool DebugModeCheat = false;


		[Range( -2048, 2048 )]
		[DefaultValue( -232 )]
		public int ControlsPositionX = -232;

		[Range( -1024, 1024 )]
		[DefaultValue( 120 )]
		public int ControlsPositionY = 120;


		[Range(0, 999)]
		[DefaultValue( 3 )]
		public int BlankContractRecipeContractTattersNeeded = 3;

		public Dictionary<ItemDefinition, int> BlankContractAltRecipeIngredients = new Dictionary<ItemDefinition, int>();

		//public string BlankContractRecipeStation = "";	//TODO


		[Range( 0, 100f )]
		[DefaultValue( 0.015f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float GhostNpcSpawnChance = 0.015f;


		[Range( -2048, 2048 )]
		[DefaultValue( 64 )]
		public int TradeUIPositionX = 64;

		[Range( -1024, 1024 )]
		[DefaultValue( 256 )]
		public int TradeUIPositionY = 256;


		public Dictionary<ItemDefinition, int> TradeItemContractTatters = new Dictionary<ItemDefinition, int>();


		[DefaultValue( true )]
		public bool ToggleableIntrinsics = true;



		////////////////

		public IntrinsicsConfig() {
			this.TradeItemContractTatters = new Dictionary<ItemDefinition, int>();

			Func<int, ItemDefinition> n = ( int itemId ) => new ItemDefinition( itemId );    //TODO GetProperUniqueId

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

		////

		public override ModConfig Clone() {
			var clone = (IntrinsicsConfig)base.Clone();

			clone.BlankContractAltRecipeIngredients = this.BlankContractAltRecipeIngredients?.ToDictionary( kv => kv.Key, kv => kv.Value );
			clone.TradeItemContractTatters = this.TradeItemContractTatters?.ToDictionary( kv => kv.Key, kv => kv.Value );

			return clone;
		}
	}
}
