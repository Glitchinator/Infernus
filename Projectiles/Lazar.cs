using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Lazar : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ZAP");
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.height = 2;
            Projectile.width = 42;
            Projectile.hostile = false;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}