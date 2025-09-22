using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Enchanted_Femur_Explosion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 180;
            Projectile.height = 180;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.alpha = 0;
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 60;
            Projectile.DamageType = DamageClass.Generic;

        }

        public override void AI()
        {
            Projectile.alpha += 9;
            //Projectile.scale += .01f;

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}