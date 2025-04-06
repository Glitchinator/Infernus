using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Lazar : ModProjectile
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
        int timer;
        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projectilePosition = Projectile.position;
                    projectilePosition -= Projectile.velocity * (i * 2f);
                    Projectile.alpha = 255;
                    int dust = Dust.NewDust(projectilePosition, 3, 3, DustID.LifeDrain, 0f, 0f, 0, default, 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.023f;
                    Main.dust[dust].velocity *= 0.2f;
                }
            }
            timer++;
            if (timer == 20)
            {
                Projectile.tileCollide = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.91f);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 34; k++)
            {
                Vector2 speed = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.LifeDrain, speed * 2, Scale: 2.5f);
                wand.noGravity = true;
            }
            Projectile.Kill();
            return false;
        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int k = 0; k < 4; k++)
            {
                float speedMulti = Main.rand.NextFloat(0.22f);

                Vector2 newVelocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(24));


                newVelocity *= speedMulti;

                var smokeGore = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                smokeGore.velocity = newVelocity;
                // smokeGore.velocity += Vector2.One;

            }
            for (int k = 0; k < 8; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.LifeDrain, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 0,default, 2f);
            }

        }
    }
}