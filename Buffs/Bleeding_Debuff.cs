using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Bleeding_Debuff : ModBuff
    {
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
            npc.lifeRegen -= 6;

            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Blood, Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
            }
        }
    }
}
