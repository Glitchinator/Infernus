using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Lightning_Boom : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.alpha = 0;
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 7; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6), ModContent.ProjectileType<Lightning_Bolt>(), Projectile.damage, 0, Projectile.owner);
            }
        }

        public override void AI()
        {
            Projectile.alpha += 1;
            Projectile.scale += .115f;

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.damage = 0;
        }
    }
}