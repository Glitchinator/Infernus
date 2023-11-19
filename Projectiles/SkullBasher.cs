using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class SkullBasher : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PaladinsHammerFriendly);
            AIType = ProjectileID.PaladinsHammerFriendly;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.height = 28;
            Projectile.width = 28;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.netImportant = true;
        }
    }
}