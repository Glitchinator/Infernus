using Terraria;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Death_Ring_White : ModProjectile
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
            Projectile.alpha += 1;
            Projectile.scale += .115f;
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}