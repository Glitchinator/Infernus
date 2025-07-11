using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class TGoblinM : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 48;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.extraUpdates = 1;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 120);
        }
        int Dive_Timer;
        bool Diving = false;
        int When_Dive;
        int Dive_Reset;
        bool Left = false;

        public override void OnSpawn(IEntitySource source)
        {
            Left = Main.rand.NextBool();
            When_Dive = Main.rand.Next(10, 21);
            Dive_Reset = Main.rand.Next(40, 44);
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 11)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }

            Player player = Main.player[Projectile.owner];
            Projectile.spriteDirection = Projectile.direction;

            Vector2 withplayer = player.Center;
            withplayer.Y -= 48f;
            float notamongusX = (10 + Projectile.minionPos * 40) * -player.direction;
            withplayer.X += notamongusX;
            Vector2 vectorToplayer = withplayer - Projectile.Center;
            float distanceToplayer = vectorToplayer.Length();
            if (Main.myPlayer == player.whoAmI && distanceToplayer > 2000f)
            {
                Projectile.position = withplayer;
                Projectile.velocity *= 0.1f;
            }

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<GoblinBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<GoblinBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            float distanceFromTarget = 250f;
            Vector2 targetCenter = Projectile.position;
            bool foundTarget = false;

            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);
                if (between < 2000f)
                {

                    if (Left == true)
                    {
                        targetCenter = new Vector2(npc.Center.X + 100, npc.Center.Y);
                    }
                    else
                    {
                        targetCenter = new Vector2(npc.Center.X - 100, npc.Center.Y);
                    }

                    float detween = Vector2.Distance(targetCenter, Projectile.Center);
                    distanceFromTarget = detween;
                    foundTarget = true;
                }
            }
            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        bool closeThroughWall = between < 100f;
                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {

                            if (Left == true)
                            {
                                targetCenter = new Vector2(npc.Center.X + 100, npc.Center.Y);
                            }
                            else
                            {
                                targetCenter = new Vector2(npc.Center.X - 100, npc.Center.Y);
                            }

                            float detween = Vector2.Distance(targetCenter, Projectile.Center);
                            distanceFromTarget = detween;
                            foundTarget = true;
                        }
                    }
                }
            }
            Projectile.friendly = foundTarget;


            float speed = 7f;
            float inertia = 14f;

            if (foundTarget)
            {

                if (distanceFromTarget > 20f && Diving == false)
                {
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                else
                {
                    Diving = true;
                }
                if (Diving == true)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.97f;
                    Projectile.velocity.Y = Projectile.velocity.Y * 0.97f;

                    Dive_Timer++;
                }
                if (Dive_Timer == When_Dive)
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        if (Left == true)
                        {
                            Projectile.velocity.Y = 0;
                            Projectile.velocity.X = -10;
                        }
                        else
                        {
                            Projectile.velocity.Y = 0;
                            Projectile.velocity.X = 10;
                        }
                        /*
                        float dive_speed = 18f;
                        Vector2 VectorToCursor = targetCenter - Projectile.position;
                        float DistToCursor = VectorToCursor.Length();

                        DistToCursor = dive_speed / DistToCursor;
                        VectorToCursor *= DistToCursor;

                        Projectile.velocity = VectorToCursor;
                        */
                    }
                }
                if (Dive_Timer > Dive_Reset)
                {
                    if (Left == false)
                    {
                        Left = true;
                    }
                    else
                    {
                        Left = false;
                    }
                    Diving = false;
                    Dive_Timer = 0;
                }
            }
            else
            {
                Dive_Timer = 0;
                Diving = false;
                if (distanceToplayer > 600f)
                {
                    speed = 12f;
                    inertia = 35f;
                }
                else
                {
                    speed = 7f;
                    inertia = 50f;
                }
                if (distanceToplayer > 20f)
                {
                    vectorToplayer.Normalize();
                    vectorToplayer *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToplayer) / inertia;
                }
                else if (Projectile.velocity == Vector2.Zero)
                {
                    Projectile.velocity.X = -0.01f;
                    Projectile.velocity.Y = -0.01f;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Asset<Texture2D> texture = ModContent.Request<Texture2D>("Infernus/Projectiles/TGoblinM_Afteraffect");
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw((Texture2D)texture, drawPos, null, new Color(242, 240, 235, 0) * (.60f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
    }
}