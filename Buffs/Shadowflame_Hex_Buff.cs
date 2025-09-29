using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Shadowflame_Hex_Buff : ModBuff
    {
        public override string Texture => "Infernus/Buffs/Whip_Debuffs/Whip_Debuff_Icon";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= 7;

            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Shadowflame, 0, Main.rand.Next(4, 11));
            }
        }
    }
}
