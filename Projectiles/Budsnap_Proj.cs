using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Budsnap_Proj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 18;
            Projectile.width = 16;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Plantera_Pink, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Main.dust[dust].noGravity = true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            float positionX = Projectile.Center.X + (Projectile.velocity.X * -1f);
            float positionY = Projectile.Center.Y + (Projectile.velocity.Y * -1f);
            float rotation = MathHelper.ToRadians(12);
            float rotation2 = MathHelper.ToRadians(20);
            float rotation3 = MathHelper.ToRadians(33);
            Vector2 velocity = Projectile.velocity;
            Vector2 velocity2 = Projectile.velocity;
            Vector2 velocity3 = Projectile.velocity;
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation, -rotation, i));
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), positionX, positionY, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Budsnap_Thorn>(), (int)(Projectile.damage * .4f), 0, Projectile.owner);
            }
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity2.RotatedBy(MathHelper.Lerp(rotation2, -rotation2, i));
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), positionX, positionY, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Budsnap_Thorn>(), (int)(Projectile.damage * .4f), 0, Projectile.owner);
            }
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity3.RotatedBy(MathHelper.Lerp(rotation3, -rotation3, i));
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), positionX, positionY, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Budsnap_Thorn>(), (int)(Projectile.damage * .4f), 0, Projectile.owner);
            }
        }
    }
}