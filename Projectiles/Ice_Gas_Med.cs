using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Ice_Gas_Med : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 140;
            Projectile.height = 140;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 120;

            DrawOffsetX = -30;
            DrawOriginOffsetY = -30;
        }

        public override void AI()
        {
            Projectile.rotation += 0.04f * (float)Projectile.direction;

            if (Projectile.timeLeft <= 30)
            {
                Projectile.alpha += 4;
                Projectile.hostile = false;
                return;
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 15;
            }

            if (Projectile.alpha <= 10)
            {
                Projectile.hostile = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return true;
            Main.instance.LoadProjectile(Projectile.type);

            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

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

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(76, 200, 217, 50), Projectile.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(76, 200, 217, 77), Projectile.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }

            return true;
        }
    }
}