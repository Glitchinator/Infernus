using Terraria;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Ice_Wall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Wall");
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 260;
        }
    }
}