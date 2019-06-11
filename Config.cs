using HamstarHelpers.Components.Config;
using HamstarHelpers.Helpers.ItemHelpers;
using System;
using System.Collections.Generic;
using Terraria.ID;


namespace Intrinsics {
	public class IntrinsicsConfigData : ConfigurationDataBase {
		public readonly static string ConfigFileName = "Intrinsics Config.json";



		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugModeInfo = false;

		public int BlankContractRecipeContractTattersNeeded = 3;
		public string[] BlankContractAlternativeRecipeIngredients = new string[0];
		//public string BlankContractRecipeStation = "";	//TODO

		public float GhostNpcSpawnChance = 0.015f;

		public int TradeUIPositionX = 64;
		public int TradeUIPositionY = 256;

		public IDictionary<string, int> TradeItemContractTatters = new Dictionary<string, int>();



		////////////////

		public void SetDefaults() {
			Func<int, string> n = ( int itemId ) => ItemIdentityHelpers.GetProperUniqueId( itemId );
			
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


		////////////////

		public bool UpdateToLatestVersion() {
			var mymod = IntrinsicsMod.Instance;
			var newConfig = new IntrinsicsConfigData();
			newConfig.SetDefaults();

			var versSince = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( versSince >= mymod.Version ) {
				return false;
			}

			if( this.VersionSinceUpdate == "" ) {
				this.SetDefaults();
			}

			this.VersionSinceUpdate = mymod.Version.ToString();

			return true;
		}
	}
}
