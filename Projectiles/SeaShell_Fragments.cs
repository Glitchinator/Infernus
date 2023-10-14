using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class SeaShell_Fragments : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SeaShell Fragment");
        }
        public override void SetDefaults()
        {
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.height = 8;
            Projectile.width = 6;
            Projectile.hostile = false;
            Projectile.timeLeft = 250;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            Projectile.velocity.X = 0;
            Projectile.velocity.Y = Projectile.velocity.Y + 0.45f;
        }
    }
}