using Infernus.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class SkullBasher : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = false;
        }
        bool hit = false;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int k = 0; k < 16; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 20, DustID.Blood, speed2 * 2, Scale: 1.7f);
                wand.noGravity = true;
            }
            Projectile.damage = (int)(Projectile.damage * 0.95f);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.aiStyle = ProjAIStyleID.Boomerang;
            hit = true;
            return false;
        }
        int timer;
        public override void AI()
        {
            timer++;
            if (timer == 9)
            {
                Projectile.tileCollide = true;
            }
            Projectile.rotation += 0.2f * (float)Projectile.direction;
            Projectile.ai[0] += 1f;
            if (hit == false)
            {
                Projectile.ai[0] += 1f;
                if (Projectile.ai[0] >= 23f)
                {
                    Projectile.ai[0] = 23f;
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.7f;
                }
                if (Projectile.velocity.Y > 22f)
                {
                    Projectile.velocity.Y = 22f;
                }
            }
            if (Main.rand.NextBool(3))
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Bone, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.014f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Bone);
            }
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(48, 41, 31, 0) * (.50f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
    }
}