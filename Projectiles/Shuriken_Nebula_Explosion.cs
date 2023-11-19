using Terraria;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Shuriken_Nebula_Explosion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 400;
            Projectile.height = 400;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.alpha = 0;
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 8;
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