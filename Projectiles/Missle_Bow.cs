using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Missle_Bow : ModProjectile
    {
        int time = 0;
        bool homing = true;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = false;
            Projectile.height = 8;
            Projectile.width = 8;
            Projectile.hostile = false;
            Projectile.timeLeft = 250;
            Projectile.netImportant = true;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 1;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(4))
            {
                for (int k = 0; k < 2; k++)
                {
                    Dust Sword = Dust.NewDustPerfect(Projectile.Center, DustID.Smoke, Projectile.velocity, 0, default, Scale: 1f);
                    Sword.noGravity = true;
                }
            }
            time++;
            if (time == 38)
            {
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                for (int k = 0; k < 6; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(0.3f, 0.3f);
                    Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.Smoke, speed * 3, Scale: 1.2f);
                    Sword.noGravity = true;
                }
                for (int k = 0; k < 5; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(0.3f, 0.3f);
                    Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.InfernoFork, speed * 3, Scale: 1f);
                    Sword.noGravity = true;
                }
            }
            if (time >= 38)
            {
                Projectile.friendly = true;
                Projectile.netUpdate = true;
                float maxDetectRadiusenemy = 90f;
                var inertiaenemy = 12f;

                NPC closestNPC = FindClosestNPC(maxDetectRadiusenemy);
                if (closestNPC == null)
                {
                    if (homing == true)
                    {
                        if (Main.myPlayer == Projectile.owner)
                        {
                            float speed = 18f;
                            Vector2 VectorToCursor = Main.MouseWorld - Projectile.position;
                            float DistToCursor = VectorToCursor.Length();

                            VectorToCursor *= DistToCursor;

                            //Projectile.velocity = VectorToCursor;
                            float maxDetectRadius = 400f;
                            var inertia = 12f;

                            VectorToCursor.Normalize();
                            VectorToCursor *= 16;
                            Projectile.velocity = (Projectile.velocity * (inertia - 1) + VectorToCursor) / inertia;


                            // string i = DistToCursor.ToString();
                            //Main.NewText(i, 229, 214, 127);
                            if (DistToCursor <= 60f)
                            {
                                homing = false;
                            }

                        }
                    }
                }
                else
                {
                    Vector2 direction = closestNPC.Center - Projectile.Center;
                    direction.Normalize();
                    direction *= 16;
                    Projectile.velocity = (Projectile.velocity * (inertiaenemy - 1) + direction) / inertiaenemy;
                }
            }
            if (time >= 30)
            {
                Projectile.velocity.X = Projectile.velocity.X * 1.03f;
                Projectile.velocity.Y = Projectile.velocity.Y * 1.03f;
            }
            else
            {
                Projectile.velocity.X = Projectile.velocity.X * 0.95f;
                Projectile.velocity.Y = Projectile.velocity.Y * 0.95f;
            }


            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 300);

            for (int k = 0; k < 6; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork, 2.5f, -2.5f, 0, default, 1.2f);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Asset<Texture2D> texture = ModContent.Request<Texture2D>("Infernus/Projectiles/Molten_Chaingun");
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new(Projectile.width * 0.5f + 1, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw((Texture2D)texture, drawPos, null, new Color(242, 240, 235, 0) * (.60f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
        public override void OnSpawn(IEntitySource source)
        {
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