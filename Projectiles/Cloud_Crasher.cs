using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Cloud_Crasher : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crasher Knife");
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.height = 34;
            Projectile.width = 10;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(6))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.YellowTorch, Projectile.velocity.X, Projectile.velocity.Y);
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}