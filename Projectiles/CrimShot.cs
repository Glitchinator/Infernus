using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class CrimShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 34;
            Projectile.width = 14;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(4))
            {
                Projectile.velocity.Y = -0.01f;
            }
            if (Main.rand.NextBool(4))
            {
                Projectile.velocity.Y = +0.01f;
            }
        }
    }
}