using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    class Swingy2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 14;
        }

        public override void SetDefaults()
        {
            Projectile.width = 260;
            Projectile.height = 260;
            Projectile.aiStyle = 20;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.netImportant = true;
        }



        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[Projectile.owner] = 9;
            target.immune[Projectile.owner] = 9;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(155, 155, 155, 155) * (1f - Projectile.alpha / 255f);
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.velocity *= 1f;
            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 14)
                {
                    Projectile.frame = 0;
                }
            }
            if (player.direction > 0)
            {
                DrawOffsetX = +0;
                DrawOriginOffsetX = -100;
                DrawOriginOffsetY = -0;
            }
            else if (player.direction < 0)
            {
                DrawOffsetX = -0;
                DrawOriginOffsetX = +100;
                DrawOriginOffsetY = -0;
            }

            Projectile.direction = (Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : -1));
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            if (Projectile.spriteDirection == -50)
                Projectile.rotation += MathHelper.Pi;
            {
                Lighting.AddLight(Projectile.position, 0.25f, 0f, 0.5f);
            }
            Projectile.soundDelay--;
            if (Projectile.soundDelay <= 16)
            {
                SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
                Projectile.soundDelay = 26;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            origin.X = (float)((Projectile.spriteDirection == -50) ? (sourceRectangle.Width - 40) : 40);

            Color drawColor = Projectile.GetAlpha(Color.SlateBlue);
            Main.spriteBatch.Draw(texture,
            Projectile.Center - Main.screenPosition + new Vector2(6f, Projectile.gfxOffY),
            sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            return false;
        }
    }
}