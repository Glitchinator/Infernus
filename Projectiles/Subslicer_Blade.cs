using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Subslicer_Blade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PaladinsHammerFriendly);
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.height = 34;
            Projectile.width = 34;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.netImportant = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                int d = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Grass, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                Main.dust[d].noGravity = true;
            }
            if(Main.rand.NextBool(8))
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Subslicer_Proj>(), 31, 3f, Projectile.owner);
            }
        }
    }
}