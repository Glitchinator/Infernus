﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Infernus.Projectiles
{
    public class Homing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
            Projectile.netImportant = true;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Firework_Red, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.014f;

            float maxDetectRadius = 400f;
            var inertia = 12f;

            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            Vector2 direction = closestNPC.Center - Projectile.Center;
            direction.Normalize();
            direction *= 16;
            Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;

            Projectile.rotation = Projectile.velocity.ToRotation();

            Projectile.velocity.Y += Projectile.ai[0];
        }

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }
    }
}