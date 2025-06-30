using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Magic_Star : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.alpha = 145;
            Projectile.penetrate = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 36;
        }
        public override void AI()
        {
            if (Projectile.timeLeft <= 274)
            {
                Projectile.tileCollide = true;
            }
            Projectile.rotation += (float)Projectile.direction * 2;
            Projectile.velocity.X = Projectile.velocity.X * 1.02f;
            Projectile.velocity.Y = Projectile.velocity.Y * 1.02f;

            if (Main.rand.NextBool(2))
            {
                for (int k = 0; k < 2; k++)
                {
                    Dust Sword = Dust.NewDustPerfect(Projectile.Center, DustID.YellowStarDust, Projectile.velocity, 0, default, Scale: 1f);
                    Sword.noGravity = true;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {

            for (int k = 0; k < 13; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 1f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.YellowStarDust, speed * 3, Scale: 2f);
                Sword.noGravity = true;
            }
            for (int k = 0; k < 10; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(1f, .5f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.StarRoyale, speed * 3, Scale: 1.5f);
                Sword.noGravity = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);

            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            SpriteEffects spriteEffects = SpriteEffects.None;

            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPosg = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(texture, drawPosg, null, new Color(26, 35, 126, 50) * (1f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            Rectangle frame;


            frame = texture.Frame();

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new(Projectile.width / 2 + 5 - frameOrigin.X, Projectile.height - frame.Height + 6);
            Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Projectile.timeLeft / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
            {
                time = 2f - time;
            }

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(26, 35, 126, 50), Projectile.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(26, 35, 126, 77), Projectile.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }

            return true;
        }
    }
}