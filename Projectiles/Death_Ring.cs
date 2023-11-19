using Terraria;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Death_Ring : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.alpha = 0;
            Projectile.scale = 1f;
        }

        public override void AI()
        {
            Projectile.alpha += 7;
            Projectile.scale += .075f;

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}