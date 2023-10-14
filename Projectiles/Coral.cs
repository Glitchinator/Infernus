using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Coral : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coral Knife");
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BoneDagger);
            AIType = ProjectileID.BoneDagger;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.netImportant = true;
        }
    }
}