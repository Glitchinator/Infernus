using Infernus.Buffs;
using Infernus.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Rock_Flail : ModProjectile
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
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 24;
            Projectile.localNPCHitCooldown = 12;
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
        int rand_dist;

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
            if (magnitude > 60f)
            {
                magnitude = 60f;
            }
            if(magnitude < 30f)
            {
                magnitude = 30f;
            }

            if(player.channel && retracted == false && retracting == true)
            {
                if (lerp_mag >= (magnitude -= rand_dist) && retracted == false)
                {
                    lerp_mag -= 3f;
                }
                if(lerp_mag <= (magnitude += rand_dist) && retracted == false)
                {
                    lerp_mag += 3f;
                }
                double dist = lerp_mag;
                Projectile.timeLeft = 299;
                Projectile.position.X = player.position.X - (int)(Math.Cos(rad) * dist);
                Projectile.position.Y = player.position.Y - (int)(Math.Sin(rad) * dist);
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
                            Projectile.usesIDStaticNPCImmunity = false;
                            Projectile.usesLocalNPCImmunity = true;
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

                        VectorToCursor = VectorToCursor.RotatedByRandom(MathHelper.ToRadians(18));

                        Projectile.velocity = VectorToCursor;
                        Projectile.tileCollide = true;
                    }
                    if (retracting == false && dist_check >= 300f)
                    {
                        retracting = true;
                        retracted = true;
                    }
                }
                
            }

            Projectile.ai[1] += 1f;

            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Stone, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
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
            Projectile.Center = player.Center;

            rand_dist = Main.rand.Next(5, 25);
            Speed = Main.rand.Next(20, 28);
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
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone, 2.5f, -2.5f, 0, default, 1.2f);
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            retracting = true;
            retracted = true;
            Projectile.tileCollide = false;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 playerArmPosition = Main.GetPlayerArmPosition(Projectile);

            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("Infernus/Projectiles/Rock_Flail_Chain");

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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(151, 151, 151, 0) * (.70f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }
            return true;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.SilverBulletSparkle,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                Projectile.owner);
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Stone);
            }
        }
    }
}