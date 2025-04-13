using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs.Whip_Debuffs
{
    public class Equite_Tag_Debuff : ModBuff
    {
        public override string Texture => "Infernus/Buffs/Whip_Debuffs/Whip_Debuff_Icon";
        public static readonly int TagDamage = 3;
        public static readonly float TagKnockBack = 2.5f;
        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsATagBuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (Main.rand.NextBool(3))
            {
                for (int k = 0; k < 3; k++)
                {
                    Vector2 speed2 = Main.rand.NextVector2Unit();
                    Dust wand = Dust.NewDustPerfect(npc.Center + speed2 * 16, DustID.SandstormInABottle, speed2 * 2, Scale: 1.2f);
                    wand.noGravity = true;
                }
            }
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= 4;
        }
    }
}
