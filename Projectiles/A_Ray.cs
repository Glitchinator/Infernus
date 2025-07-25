﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class A_Ray : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 1;
            Projectile.width = 1;
            Projectile.hostile = false;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 100;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projectilePosition = Projectile.position;
                    projectilePosition -= Projectile.velocity * (i * 0.25f);
                    Projectile.alpha = 255;
                    int dust = Dust.NewDust(projectilePosition, 1, 1, DustID.SolarFlare, 0f, 0f, 0, default, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.033f;
                    Main.dust[dust].velocity *= 0.2f;
                }
            }
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] >= 20f)
            {
                Projectile.tileCollide = true;
                Projectile.ai[1] = 20f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int k = 0; k < 4; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 16, DustID.SolarFlare, speed2 * 2, Scale: 1f);
                wand.noGravity = true;
            }
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<A_Ray_exlos>(), (int)(Projectile.damage * 1.1f), 4f, Projectile.owner);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 4; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 16, DustID.SolarFlare, speed2 * 2, Scale: 1f);
                wand.noGravity = true;
            }
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<A_Ray_exlos>(), (int)(Projectile.damage * 1.1f), 4f, Projectile.owner);
            return true;
        }
    }
}