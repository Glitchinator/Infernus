using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles.Raiko.Boss
{

    public class Meteor_Heat : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 9;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.friendly = false;
            Projectile.height = 13;
            Projectile.width = 13;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 400;
            Projectile.alpha = 255;
        }
        int timer;
        public override void AI()
        {
            Projectile.alpha = 255;
            Projectile.rotation += (float)Projectile.direction * 4;

            timer++;

            if(timer >= 50)
            {
                Projectile.velocity.X = Projectile.velocity.X * .98f;
                Projectile.velocity.Y = Projectile.velocity.Y * .98f;
            }
            if (timer <= 51)
            {
                Projectile.velocity.X = Projectile.velocity.X * 1.02f;
                Projectile.velocity.Y = Projectile.velocity.Y * 1.02f;
            }
            if (timer == 101)
            {
                timer = 0;
            }


            if (Main.rand.NextBool(4))
            {
                for (int k = 0; k < 2; k++)
                {
                    Dust Sword = Dust.NewDustPerfect(Projectile.Center, DustID.SolarFlare, Projectile.velocity, 0, default, Scale: 1f);
                    Sword.noGravity = true;
                }
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(230, 81, 0, 0) * (.9f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }
            return true;
        }
    }
}