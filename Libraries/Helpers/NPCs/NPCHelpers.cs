using System;
using Terraria;
using Terraria.ID;


namespace Intrinsics.Libraries.Helpers.NPCs {
	public class NPCHelpers {
		public static void Remove( NPC npc ) {
			npc.active = false;

			if( Main.netMode == 2 ) {
				npc.netSkip = -1;
				npc.life = 0;
				NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0 );
			}
		}
	}
}
