using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
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
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 3;
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
            float rotation = MathHelper.ToRadians(6);
            float rotation2 = MathHelper.ToRadians(14);
            float rotation3 = MathHelper.ToRadians(26);
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
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(243, 210, 250, 0) * (.40f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Plantera_Pink);
            }
        }
    }
}