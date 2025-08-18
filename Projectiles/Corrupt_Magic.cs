using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Corrupt_Magic : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 32;
            Projectile.height = 30;
            Projectile.penetrate = 1;
            Projectile.netImportant = true;
            Projectile.tileCollide = true;
            DrawOffsetX = -5;
            DrawOriginOffsetY = -6;
        }
        public override void AI()
        {
            Projectile.rotation += 0.2f * (float)Projectile.direction;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 25f)
            {
                Projectile.ai[0] = 25f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.7f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 16; k++)
            {
                Vector2 speed = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.Demonite, speed * 2, 0, default, Scale: 2f);
                wand.noGravity = true;
            }
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, -5, ModContent.ProjectileType<CorrShot>(), (int)(Projectile.damage * 0.75f), 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 5, ModContent.ProjectileType<CorrShot>(), (int)(Projectile.damage * 0.75f), 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, -5, 0, ModContent.ProjectileType<CorrShot>(), (int)(Projectile.damage * 0.75f), 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 5, 0, ModContent.ProjectileType<CorrShot>(), (int)(Projectile.damage * 0.75f), 0, Projectile.owner);
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, -5, ModContent.ProjectileType<CorrShot>(), (int)(Projectile.damage * 0.75f), 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 5, ModContent.ProjectileType<CorrShot>(), (int)(Projectile.damage * 0.75f), 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, -5, 0, ModContent.ProjectileType<CorrShot>(), (int)(Projectile.damage * 0.75f), 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 5, 0, ModContent.ProjectileType<CorrShot>(), (int)(Projectile.damage * 0.75f), 0, Projectile.owner);
            for (int k = 0; k < 16; k++)
            {
                Vector2 speed = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.Demonite, speed * 2, 0, default, Scale: 2f);
                wand.noGravity = true;
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
                Main.EntitySpriteDraw(texture, drawPosg, null, new Color(94, 71, 115, 0) * (.75f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
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

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(94, 71, 115, 50), Projectile.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(94, 71, 115, 77), Projectile.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }

            return true;
        }
    }
}