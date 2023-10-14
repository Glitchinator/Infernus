using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Aeritite_Plasma : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aeritite Plasma");
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 16;
            Projectile.width = 16;
            Projectile.hostile = false;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            Projectile.rotation += (float)Projectile.direction * 4;

            if (Main.rand.NextBool(4))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.BlueCrystalShard, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            Projectile.scale += .05f;
            Projectile.damage += 1;

            if (Projectile.damage >= 75)
            {
                Projectile.damage = 75;
            }
            if (Projectile.scale >= 2f)
            {
                Projectile.scale = 2f;
            }
        }
    }
}