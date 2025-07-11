using Infernus.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Aeritite_Minion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
        int Dive_Timer;
        bool Diving = false;
        int When_Dive;
        int Dive_Reset;
        bool exploded = false;

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = 0;
            When_Dive = Main.rand.Next(15, 24);
            Dive_Reset = Main.rand.Next(30, 41);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (exploded == false && Diving == true && Dive_Timer > When_Dive)
            {
                for (int k = 0; k < 10; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Unit();
                    Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 22, DustID.BlueCrystalShard, speed * 2, 0, default, Scale: 2.8f);
                    wand.noGravity = true;
                }
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Aeritite_Minion_Explosion>(), (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
                exploded = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (exploded == false && Diving == true && Dive_Timer > When_Dive)
            {
                for (int k = 0; k < 10; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Unit();
                    Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 22, DustID.BlueCrystalShard, speed * 2, 0, default, Scale: 2.8f);
                    wand.noGravity = true;
                }
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Aeritite_Minion_Explosion>(), (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
                exploded = true;
            }
            return true;
        }
        public override void AI()
        {
            /*
            if (++Projectile.frameCounter >= 11)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }
            */

            Player player = Main.player[Projectile.owner];
            //Projectile.spriteDirection = Projectile.direction;

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
                player.ClearBuff(ModContent.BuffType<AerBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<AerBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            float distanceFromTarget = 250f;
            Vector2 targetCenter = Projectile.position;
            Vector2 TRUEtargetCenter = Projectile.position;
            bool foundTarget = false;

            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);
                if (between < 2000f)
                {
                    TRUEtargetCenter = npc.Center;
                    targetCenter = new Vector2(npc.Center.X, npc.Top.Y - 80);
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
                            TRUEtargetCenter = npc.Center;
                            targetCenter = new Vector2(npc.Center.X, npc.Top.Y - 80);
                            float detween = Vector2.Distance(targetCenter, Projectile.Center);
                            distanceFromTarget = detween;
                            foundTarget = true;
                        }
                    }
                }
            }
            Projectile.friendly = foundTarget;


            float speed = 10f;
            float inertia = 15f;

            if (foundTarget)
            {
                if (distanceFromTarget > 30f && Diving == false)
                {
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                else
                {
                    if (Diving == false)
                    {
                        for (int k = 0; k < 6; k++)
                        {
                            Vector2 speed_Dust = Main.rand.NextVector2Unit();
                            Dust wand = Dust.NewDustPerfect(Projectile.Center + speed_Dust * 8, DustID.BlueCrystalShard, speed_Dust, 0, default, Scale: 1.3f);
                            wand.noGravity = true;
                        }
                        Projectile.frame = 1;
                    }
                    Diving = true;
                }
                if (Diving == true)
                {
                    int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.BlueCrystalShard, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.014f;
                    Projectile.velocity.X = Projectile.velocity.X * 0.97f;

                    if (Projectile.velocity.Y > -6)
                    {
                        Projectile.velocity.Y -= 0.1f;
                    }
                    Dive_Timer++;
                }
                if (Dive_Timer == When_Dive)
                {
                    Projectile.frame = 2;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        float dive_speed = 18f;
                        Vector2 VectorToCursor = TRUEtargetCenter - Projectile.position;
                        float DistToCursor = VectorToCursor.Length();

                        DistToCursor = dive_speed / DistToCursor;
                        VectorToCursor *= DistToCursor;

                        Projectile.velocity = VectorToCursor;
                    }
                }
                if (Dive_Timer > Dive_Reset)
                {
                    Projectile.frame = 0;
                    Diving = false;
                    Dive_Timer = 0;
                    exploded = false;
                }
            }
            else
            {
                Projectile.frame = 0;
                Dive_Timer = 0;
                Diving = false;
                exploded = false;
                if (distanceToplayer > 600f)
                {
                    speed = 15f;
                    inertia = 35f;
                }
                else
                {
                    speed = 6f;
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
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
    }
}