using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Boulder_Storm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 8;
        }
        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity.X = 20;
            Projectile.velocity.Y = 0;
        }
        public override void AI()
        {
            Projectile.rotation += (float)Projectile.direction * 2;

            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Obsidian, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }

            if (Projectile.timeLeft % 15 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item125 with
                {
                    Volume = 1f,
                    Pitch = 0.2f,
                    MaxInstances = 3,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(0, 10), ModContent.ProjectileType<Boulder_Bolt>(), Projectile.damage, 0, Projectile.owner);
            }
            if (Projectile.timeLeft == 175)
            {
                Projectile.velocity.X = -20;
            }
            if (Projectile.timeLeft == 125)
            {
                Projectile.velocity.X = 20;
            }
            if (Projectile.timeLeft == 75)
            {
                Projectile.velocity.X = -20;
            }
            if (Projectile.timeLeft == 25)
            {
                Projectile.velocity.X = 20;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.BrokenArmor, 300);
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(40, 43, 67, 0) * (.30f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
    }
}