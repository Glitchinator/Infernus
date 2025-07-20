using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Rocky_Rounds : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            //Projectile.height = 2;
            //Projectile.width = 2;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 7;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Stone);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            float positionX = Projectile.Center.X + (Projectile.velocity.X * -1f);
            float positionY = Projectile.Center.Y + (Projectile.velocity.Y * -1f);
            float rotation = MathHelper.ToRadians(1);
            float rotation2 = MathHelper.ToRadians(9);
            Vector2 velocity = Projectile.velocity;
            Vector2 velocity2 = Projectile.velocity;

            for (int i = 0; i < 1; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation, -rotation, i));
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), positionX, positionY, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Rocky_Rounds_Proj>(), (int)(Projectile.damage * .4f), 4f, Projectile.owner);
            }
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity2.RotatedBy(MathHelper.Lerp(rotation2, -rotation2, i));
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), positionX, positionY, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Rocky_Rounds_Proj>(), (int)(Projectile.damage * .4f), 4f, Projectile.owner);
            }
        }
    }
}