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

    public class Raiko_Bow_Arrow : ModProjectile
    {
        int time = 35;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.height = 10;
            Projectile.width = 10;
            Projectile.hostile = false;
            Projectile.timeLeft = 250;
            Projectile.netImportant = true;
            Projectile.penetrate = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 14;
        }
        public override void AI()
        {
            time--;
            if (time <= 0)
            {
                Projectile.velocity.X = Projectile.velocity.X * 1.06f;
                Projectile.velocity.Y = Projectile.velocity.Y * 1.06f;
            }
            else
            {
                Projectile.velocity.X = Projectile.velocity.X * 0.96f;
                Projectile.velocity.Y = Projectile.velocity.Y * 0.96f;
            }


            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 120);

            for (int k = 0; k < 6; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork, 2.5f, -2.5f, 0, default, 1.2f);
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(242, 240, 235, 0) * (.60f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int k = 0; k < 8; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.3f, 0.3f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.InfernoFork, speed * 3, Scale: 1f);
                Sword.noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 8; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 0.5f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.InfernoFork, speed * 3, Scale: 1f);
                Sword.noGravity = true;
            }
        }
    }
}