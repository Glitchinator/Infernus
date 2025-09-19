using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Crystal_Arrow : ModProjectile
    {
        bool hitq = false;
        private Vector2 vel;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.height = 6;
            Projectile.width = 6;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
            Projectile.penetrate = -1;

            DrawOffsetX = -8;
            DrawOriginOffsetY = -4;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if(hitq == true)
            {
                Projectile.ai[1] += 1f;
            }
            if (Projectile.ai[1] < 30f && hitq == true)
            {
                Projectile.velocity.X = Projectile.velocity.X * 0.92f;
                Projectile.velocity.Y = Projectile.velocity.Y * 0.92f;
            }
            else
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.04f;
                if (Projectile.velocity.Y > 16f)
                {
                    Projectile.velocity.Y = 16f;
                }
            }
            if (Projectile.ai[1] == 31f)
            {
                Projectile.timeLeft = 60;
                Projectile.friendly = true;
                Projectile.velocity = vel;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hitq == false)
            {
                Projectile.friendly = false;
                vel = -Projectile.velocity;
                hitq = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.PinkCrystalShard);
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(180, 100, 189, 0) * (.65f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
    }
}