using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Boulder_Rain2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boulder");
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 500;
        }
        public override void AI()
        {
            Projectile.rotation += (float)Projectile.direction * 7;

            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            if (Projectile.velocity.Y > 8.5f)
            {
                Projectile.velocity.Y = 8.5f;
            }
            if (Projectile.velocity.X > 8.5f)
            {
                Projectile.velocity.X = 8.5f;
            }
            if (Projectile.timeLeft == 250)
            {
                SoundEngine.PlaySound(SoundID.Item69, Projectile.position);

                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Right.X, Projectile.Right.Y / 2f, 0, 10, ModContent.ProjectileType<Boulder_Rain>(), 10, 0, 0);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.BrokenArmor, 800);
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(242, 240, 235, 0) * (.30f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Dust.NewDustPerfect((Projectile.Bottom + new Vector2(0, -45)) + Main.rand.NextVector2Unit((float)MathHelper.Pi / 4, (float)MathHelper.Pi / 2) * Main.rand.NextFloat(), DustID.Stone);

            Projectile.velocity.X = Projectile.velocity.X * .98f;
            Projectile.velocity.Y = Projectile.velocity.Y * .98f;
            return false;
        }
    }
}