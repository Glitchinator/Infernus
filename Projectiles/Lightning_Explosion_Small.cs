using Terraria;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Lightning_Explosion_Small : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.alpha = 0;
            Projectile.scale = 0.5f;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 60;
            Projectile.DamageType = DamageClass.Generic;

        }

        public override void AI()
        {
            Projectile.alpha += 5;
            Projectile.scale += .026f;

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}