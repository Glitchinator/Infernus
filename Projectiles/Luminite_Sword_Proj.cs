using Infernus.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Luminite_Sword_Proj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.width = 78;
            Projectile.height = 78;
            Projectile.timeLeft = 800;
            Projectile.tileCollide = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.extraUpdates = 1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int k = 0; k < 16; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 20, DustID.Electric, speed2 * 2, Scale: 1.7f);
                wand.noGravity = true;
            }
            Projectile.damage = (int)(Projectile.damage * 0.75f);
            hit.HitDirection = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            retracted = true;
            return false;
        }
        //int timer;
        //bool retracting = false;
        bool retracted = false;
        int Speed = 34;
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 10;
            height = 10;
            fallThrough = true;

            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.rotation += (float)Projectile.direction * 0.4f;

            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.011f;

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
                if (retracted == false && dist_check >= 660f)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 16, DustID.Electric, speed2 * 2, Scale: 1f);
                        wand.noGravity = true;
                        Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center + speed2 * 16, speed2 * 8, ModContent.ProjectileType<elumfire>(), (int)(Projectile.damage * 0.75f), 2f, Projectile.owner);
                    }
                    retracted = true;
                    Speed = 60;
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
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);

            SpriteBatch spriteBatch = Main.spriteBatch;
            Asset<Texture2D> texture = ModContent.Request<Texture2D>("Infernus/Projectiles/Lightning_Dagger_Giant");

            SpriteEffects spriteEffects = SpriteEffects.None;

            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPosg = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw((Texture2D)texture, drawPosg, null, new Color(242, 240, 235, 0) * (.55f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            Rectangle frame;


            frame = texture.Frame();

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new(Projectile.width / 2 - frameOrigin.X, Projectile.height - frame.Height);
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

                spriteBatch.Draw((Texture2D)texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(242, 240, 235, 50), Projectile.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw((Texture2D)texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(1, 87, 155, 77), Projectile.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }

            return true;
        }
    }
}