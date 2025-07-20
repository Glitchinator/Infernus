using Infernus.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
            Projectile.tileCollide = true;
            Projectile.ownerHitCheck = true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int k = 0; k < 16; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 20, DustID.Blood, speed2 * 2, Scale: 1.7f);
                wand.noGravity = true;
            }
            Projectile.damage = (int)(Projectile.damage * 0.75f);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            retracted = true;
            return false;
        }
        //int timer;
        //bool retracting = false;
        bool retracted = false;
        int Speed = 24;
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 18;
            height = 18;
            fallThrough = true;

            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation += 0.2f * (float)Projectile.direction;

            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Stone, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.014f;

            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 19f)
            {
                Projectile.ai[0] = 19f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.7f;
            }



            if (Main.myPlayer == Projectile.owner)
            {
                var inertia = 8f;
                Vector2 direction = player.Center - Projectile.Center;
                float dist_check = Magnitude(direction);

                if (player.dead || !player.active)
                {
                    return;
                }
                if (retracted == true)
                {
                    Projectile.tileCollide = false;
                    direction.Normalize();
                    direction *= Speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;

                }
                if (dist_check <= 40f)
                {
                    if (retracted == true)
                    {
                        Projectile.Kill();
                    }
                }
                if (retracted == false && dist_check >= 360f)
                {
                    retracted = true;
                    Speed = 34;
                    //retracted = true;
                }
            }
        }
        private static float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
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