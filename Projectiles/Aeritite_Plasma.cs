using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Aeritite_Plasma : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 9;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 16;
            Projectile.width = 16;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            Projectile.alpha = 255;
            Projectile.rotation += (float)Projectile.direction * 4;

            if (Main.rand.NextBool(4))
            {
                for (int k = 0; k < 2; k++)
                {
                    Dust Sword = Dust.NewDustPerfect(Projectile.Center, DustID.BlueCrystalShard, Projectile.velocity, 0, default, Scale: 1f);
                    Sword.noGravity = true;
                }
            }
            Projectile.scale += .05f;
            Projectile.damage += 1;

            if (Projectile.damage >= 75)
            {
                Projectile.damage = 75;
            }
            if (Projectile.scale >= 2f)
            {
                Projectile.scale = 2f;
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(47, 147, 194, 0) * (.74f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }
            return true;
        }
    }
}