using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class CorrShot : ModProjectile
    {
        int timer;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = false;
            Projectile.height = 34;
            Projectile.width = 14;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 8;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(4))
            {
                for (int k = 0; k < 2; k++)
                {
                    Dust Sword = Dust.NewDustPerfect(Projectile.Center, DustID.Demonite, Projectile.velocity, 0, default, Scale: 1f);
                    Sword.noGravity = true;
                }
            }
            timer++;
            if (timer == 24)
            {
                Projectile.friendly = true;
                Projectile.netUpdate = true;
                if (Main.myPlayer == Projectile.owner)
                {
                    float speed = 18f;
                    Vector2 VectorToCursor = Main.MouseWorld - Projectile.position;
                    float DistToCursor = VectorToCursor.Length();

                    DistToCursor = speed / DistToCursor;
                    VectorToCursor *= DistToCursor;

                    Projectile.velocity = VectorToCursor;
                }
            }
            if (timer == 34)
            {
                Projectile.tileCollide = true;
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(103, 58, 184, 0) * (.75f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
    }
}