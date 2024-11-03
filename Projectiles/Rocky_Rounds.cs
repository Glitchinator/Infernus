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
            Projectile.height = 2;
            Projectile.width = 2;
            Projectile.hostile = false;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
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
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), positionX, positionY, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Rocky_Rounds_Proj>(), (int)(Projectile.damage * .25f), 0, Projectile.owner);
            }
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity2.RotatedBy(MathHelper.Lerp(rotation2, -rotation2, i));
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), positionX, positionY, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Rocky_Rounds_Proj>(), (int)(Projectile.damage * .25f), 0, Projectile.owner);
            }
        }
    }
}