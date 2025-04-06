using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Vivid_Glory_Round : ModProjectile
    {
        public override void SetDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.height = 34;
            Projectile.width = 10;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;


            if (Main.rand.NextBool(3))
            {
                Projectile.velocity.Y += .8f;
                Projectile.velocity.X -= .8f;
            }
            if (Main.rand.NextBool(3))
            {
                Projectile.velocity.Y -= .8f;
                Projectile.velocity.X += .8f;
            }

            float maxDetectRadius = 400f;
            var inertia = 12f;

            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            Vector2 direction = closestNPC.Center - Projectile.Center;
            direction.Normalize();
            direction *= 16;
            Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;

            Projectile.velocity.Y += Projectile.ai[0];
        }
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(242, 240, 235, 0) * (.30f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 20; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, .5f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.Vortex, speed * 3, Scale: 1f);
                Sword.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item42 with
            {
                Volume = .8f,
                MaxInstances = 1,
                SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
            }, Projectile.position);
        }
    }
}