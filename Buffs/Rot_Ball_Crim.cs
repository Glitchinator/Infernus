using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Rot_Ball_Crim : ModBuff
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
            npc.lifeRegen -= 7;

            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Blood, Main.rand.Next(-2, 4), Main.rand.Next(-11, -4));
            }
            if (npc.HasBuff(ModContent.BuffType<Rot_Ball_Demo>()))
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<A_M>(), 15, 0, 0);
                for (int k = 0; k < 20; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Unit();
                    Dust wand = Dust.NewDustPerfect(npc.Center + speed * 32, DustID.Blood, speed * 2, 0, default, Scale: 2.8f);
                    wand.noGravity = true;
                }
                npc.DelBuff(buffIndex);
                int buff_id = npc.FindBuffIndex(ModContent.BuffType<Rot_Ball_Demo>());
                npc.DelBuff(buff_id);
                buffIndex--;
            }
        }
    }
}
