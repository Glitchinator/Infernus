using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Tesla : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 16;
            Projectile.width = 16;
            Projectile.hostile = false;
            Projectile.extraUpdates = 75;
            Projectile.timeLeft = 75;
            Projectile.penetrate = 2;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
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
                    int dust = Dust.NewDust(projectilePosition, 1, 1, DustID.Electric, 0f, 0f, 0, default, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.012f;
                    Main.dust[dust].velocity *= 0.2f;
                }
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            float speedMulti = Main.rand.NextFloat(0.22f);

            Vector2 newVelocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(24));


            newVelocity *= speedMulti;

            var smokeGore = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
            smokeGore.velocity = newVelocity;

            for (int k = 0; k < 4; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }

        }
    }
}