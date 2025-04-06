using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class SandySlug : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.timeLeft = 20;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Sand, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Sandnado, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            Projectile.velocity.X = Projectile.velocity.X * .97f;
            Projectile.velocity.Y = Projectile.velocity.Y * .97f;
        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int k = 0; k < 1; k++)
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
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Sandstorm, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }

        }
    }
}