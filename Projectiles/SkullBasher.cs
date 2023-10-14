using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class SkullBasher : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skull Basher");
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PaladinsHammerFriendly);
            AIType = ProjectileID.PaladinsHammerFriendly;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.height = 38;
            Projectile.width = 38;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.netImportant = true;
        }
    }
}