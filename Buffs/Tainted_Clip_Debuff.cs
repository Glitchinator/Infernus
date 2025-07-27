using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Tainted_Clip_Debuff : ModBuff
    {
        public override string Texture => "Infernus/Buffs/Whip_Debuffs/Whip_Debuff_Icon";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= 5;

            if (npc.buffTime[buffIndex] % 30 == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), npc.Center, Vector2.Zero, ModContent.ProjectileType<Tainted_Clip_exlos>(), 10, 0f, 0);
            }
        }
    }
}
