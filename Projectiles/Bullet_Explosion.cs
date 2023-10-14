using Terraria;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Bullet_Explosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");

        }
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.alpha = 0;
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 22;
        }

        public override void AI()
        {
            Projectile.alpha += 3;
            Projectile.scale -= .066f;

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}