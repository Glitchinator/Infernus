using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Terra2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 60;
            Projectile.height = 54;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 22;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        int Dive_Timer;
        bool Diving = false;
        int When_Dive;
        int Dive_Reset;
        int Shoot_Time;
        bool slower = false;

        public override void OnSpawn(IEntitySource source)
        {
            When_Dive = Main.rand.Next(10, 21);
            Dive_Reset = Main.rand.Next(38, 51);
            Shoot_Time = Main.rand.Next(60, 71);
        }
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            Projectile.spriteDirection = Projectile.direction;

            if (++Projectile.frameCounter >= 11)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }

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
                player.ClearBuff(ModContent.BuffType<TerraBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<TerraBuff>()))
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
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
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
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }
            Projectile.friendly = foundTarget;


            float speed = 20f;
            float inertia = 16f;

            if (foundTarget)
            {
                if (distanceFromTarget > 160f && Diving == false)
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
                    if (slower == false)
                    {
                        Projectile.velocity.X = Projectile.velocity.X * 0.97f;
                        Projectile.velocity.Y = Projectile.velocity.Y * 0.97f;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X * 0.92f;
                        Projectile.velocity.Y = Projectile.velocity.Y * 0.92f;
                    }

                    Dive_Timer++;
                }
                if (Dive_Timer == When_Dive)
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        float dive_speed = 18f;
                        Vector2 VectorToCursor = targetCenter - Projectile.position;
                        float DistToCursor = VectorToCursor.Length();

                        DistToCursor = dive_speed / DistToCursor;
                        VectorToCursor *= DistToCursor;

                        Projectile.velocity = VectorToCursor;
                    }
                }
                if (Dive_Timer == Dive_Reset)
                {
                    slower = true;
                }
                if (Dive_Timer > Shoot_Time)
                {
                    Projectile.velocity = Vector2.Zero;
                    Vector2 shootVel = targetCenter - Projectile.Center;
                    if (shootVel == Vector2.Zero)
                    {
                        shootVel = new Vector2(0f, 1f);
                    }
                    shootVel.Normalize();
                    shootVel *= 20;
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<Terra_Shot>(), (int)(Projectile.damage * 1.3f), 8f, Main.myPlayer, 0f, Projectile.owner);
                    Projectile.velocity = new Vector2(-shootVel.X, -shootVel.Y);
                    Diving = false;
                    Dive_Timer = 0;
                    slower = false;
                }
            }
            else
            {
                Dive_Timer = 0;
                Diving = false;
                slower = false;
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
    }
}