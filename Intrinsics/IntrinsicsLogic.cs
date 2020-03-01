using HamstarHelpers.Helpers.Items.Attributes;
using System;
using Terraria;


namespace Intrinsics {
	public enum IntrinsicType {
		Armor = 1,
		Accessory = 2,
		Buff = 3
	}




	public class IntrinsicsLogic {
		public static bool ItemHasIntrinsics( Item item ) {
			return item.buffType != 0 || item.accessory || ItemAttributeHelpers.IsArmor( item );
		}


		public static IntrinsicType GetItemIntrinsicType( Item item ) {
			if( item.accessory ) {
				return IntrinsicType.Accessory;
			}
			if( ItemAttributeHelpers.IsArmor(item) ) {
				return IntrinsicType.Armor;
			}
			if( item.buffType != 0 ) {
				return IntrinsicType.Buff;
			}
			return 0;
		}
	}
}
