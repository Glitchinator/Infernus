using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class HolyRam : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 88;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 600;
            Projectile.CloneDefaults(ProjectileID.JavelinFriendly);
            AIType = ProjectileID.JavelinFriendly;
            Projectile.penetrate = 2;
            Projectile.netImportant = true;

        }
        public override void AI()
        {
            if (Main.rand.NextBool(15))
            {
                int a = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Homing>(), (int)(Projectile.damage * .8f), 0, Projectile.owner);
                Main.projectile[a].timeLeft = 100;
            }
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Clentaminator_Red, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
        }
    }
}