﻿using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Meteor_Flail : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.scale = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.timeLeft = 1000;
            Projectile.tileCollide = false;
            //Projectile.aiStyle = 2;
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 3;
        }
        bool retracting = true;
        bool retracted = false;
        int Speed = 24;
        float lerp_mag = 0f;
        bool lerped = false;
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
            double deg = (double)Projectile.ai[1];
            double rad = deg * (Math.PI / Speed);
            //double dist = 64;

            Vector2 distance = player.Center - Main.MouseWorld;

            float magnitude = Magnitude(distance);
            if (magnitude > 400f)
            {
                magnitude = 400f;
            }

            if(player.channel)
            {
                if (lerp_mag >= magnitude && lerped == false)
                {
                    lerp_mag = magnitude;
                    lerped = true;
                }
                if(lerp_mag <= magnitude && lerped == false)
                {
                    lerp_mag += 3f;
                }
                if (lerped == true)
                {
                    lerp_mag = magnitude;
                }
                double dist = lerp_mag;
                Projectile.timeLeft = 299;
                Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
                /* TODO- Make projectile hitbox bigger while swinging around player without fucking everything else up
                Projectile.width = 72;
                Projectile.height = 72;
                DrawOffsetX = 18;
                DrawOriginOffsetY = 18;
                */
            }
            else
            {
                /*
                Projectile.width = 36;
                Projectile.height = 36;
                DrawOffsetX = 0;
                DrawOriginOffsetY = 0;
                */
                if (Main.myPlayer == Projectile.owner)
                {
                    var inertia = 8f;
                    Vector2 direction = player.Center - Projectile.Center;
                    float dist_check = Magnitude(direction);

                    if (player.dead || !player.active)
                    {
                        return;
                    }
                    if(retracting == true)
                    {
                        direction.Normalize();
                        direction *= Speed;
                        Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;

                        Projectile.rotation = Projectile.velocity.ToRotation();

                        //Projectile.velocity.Y += Projectile.ai[0];
                    }
                    if (dist_check <= 40f)
                    {
                        if (retracted == false)
                        {
                            retracting = false;
                            Projectile.damage = (int)(Projectile.damage * 0.5f);
                        }
                        if(retracted == true)
                        {
                            Projectile.Kill();
                        }
                        float speed = (float)Speed;
                        Vector2 VectorToCursor = Main.MouseWorld - Projectile.Center;
                        float DistToCursor = VectorToCursor.Length();

                        DistToCursor = speed / DistToCursor;
                        VectorToCursor *= DistToCursor;

                        Projectile.velocity = VectorToCursor;
                        Projectile.tileCollide = true;
                    }
                    if (retracting == false && dist_check >= 400f)
                    {
                        retracting = true;
                        retracted = true;
                    }
                }
                
            }

            Projectile.ai[1] += 1f;

            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.InfernoFork, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.014f;
        }
        private static float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            Projectile.position = player.position;
            /*
            Projectile.width = 36;
            Projectile.height = 36;
            */
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item70, Projectile.position);
            for (int k = 0; k < 20; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
                retracting = true;
                retracted = true;
            }
            Projectile.tileCollide = false;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 playerArmPosition = Main.GetPlayerArmPosition(Projectile);

            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("Infernus/Projectiles/Meteor_Flail_Chain");

            Rectangle? chainSourceRectangle = null;
            float chainHeightAdjustment = 0f;

            Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
            Vector2 chainDrawPosition = Projectile.Center;
            Vector2 vectorFromProjectileToPlayerArms = playerArmPosition.MoveTowards(chainDrawPosition, 4f) - chainDrawPosition;
            Vector2 unitVectorFromProjectileToPlayerArms = vectorFromProjectileToPlayerArms.SafeNormalize(Vector2.Zero);
            float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height()) + chainHeightAdjustment;
            if (chainSegmentLength == 0)
            {
                chainSegmentLength = 10;
            }
            float chainRotation = unitVectorFromProjectileToPlayerArms.ToRotation() + MathHelper.PiOver2;
            int chainCount = 0;
            float chainLengthRemainingToDraw = vectorFromProjectileToPlayerArms.Length() + chainSegmentLength / 2f;

            while (chainLengthRemainingToDraw > 0f)
            {
                var chainTextureToDraw = chainTexture;
                Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, Color.White, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);
                chainDrawPosition += unitVectorFromProjectileToPlayerArms * chainSegmentLength;
                chainCount++;
                chainLengthRemainingToDraw -= chainSegmentLength;
            }

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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(242, 154, 22, 0) * (.70f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.Item70, Projectile.position);
            for (int k = 0; k < 20; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
            }
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.OnFire, 300);
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.OnFire, 180, quiet: false);
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.SolarFlare);
            }
        }
    }
}