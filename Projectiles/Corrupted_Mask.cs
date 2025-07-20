using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Corrupted_Mask : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            //Main.projFrames[Projectile.type] = 3;
        }



        public sealed override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.timeLeft = 900;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.penetrate = -1;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override void AI()
        {
            if (EffectNPC.Haunted_NPC == null)
            {
                Projectile.Kill();
            }
            else
            {
                NPC npc = EffectNPC.Haunted_NPC;
                if (npc.active == true)
                {
                    Projectile.damage = EffectNPC.damage;
                    Vector2 targetCenter = npc.Center;

                    targetCenter = new Vector2(npc.Center.X, npc.Top.Y - 80);

                    float speed = 17f;
                    float inertia = 15f;

                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                else
                {
                    Projectile.Kill();
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Coralstone);
            }
            var damage = (int)(Projectile.damage * 0.15f);
            var proj = ModContent.ProjectileType<Flour_Homing>();
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(-7, 0), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(7, 0), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(0, 7), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(0, -7), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(-7, -7), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(7, -7), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(-7, 7), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(7, 7), proj, damage, 3f, Main.myPlayer);
            EffectNPC.Haunted_NPC = null;
        }
    }
}