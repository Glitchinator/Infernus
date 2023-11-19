using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Prismatic_Javelin : ModProjectile
    {
        private Color randcolor;
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 34;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 600;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 22f)
            {
                Projectile.ai[0] = 22f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.3f;
            }
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f, 0 , newColor: randcolor);
        }
        public override void OnKill(int timeLeft)
        {
            float rotation = MathHelper.ToRadians(12);
            float rotation2 = MathHelper.ToRadians(115);
            float rotation3 = MathHelper.ToRadians(135);
            Vector2 velocity = Projectile.velocity * .21f;
            Vector2 velocity2 = Projectile.velocity * .15f;
            Vector2 velocity3 = Projectile.velocity * .13f;
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation, -rotation, i));
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, perturbedSpeed, ModContent.ProjectileType<Prismatic_Javelin_Proj>(), Projectile.damage, 2f, Projectile.owner);
            }
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity2.RotatedBy(MathHelper.Lerp(rotation2, -rotation2, i));
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, perturbedSpeed, ModContent.ProjectileType<Prismatic_Javelin_Proj>(), Projectile.damage, 2f, Projectile.owner);
            }
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity3.RotatedBy(MathHelper.Lerp(rotation3, -rotation3, i));
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, perturbedSpeed, ModContent.ProjectileType<Prismatic_Javelin_Proj>(), Projectile.damage, 2f, Projectile.owner);
            }
        }
        public override void OnSpawn(IEntitySource data)
        {
            randcolor = Main.hslToRgb(Main.rand.NextFloat(), .9f, .5f);
        }
    }
}