using Infernus.Buffs;
using Infernus.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Boldsum : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.width = 36;
            Projectile.height = 24;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 22;
        }
        int Dive_Timer;
        bool Diving = false;
        int When_Dive;
        int Dive_Reset;

        public override void OnSpawn(IEntitySource source)
        {
            When_Dive = Main.rand.Next(10, 21);
            Dive_Reset = Main.rand.Next(30, 41);
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }

            Player player = Main.player[Projectile.owner];
            Projectile.spriteDirection = -Projectile.direction;
            Projectile.rotation = Projectile.velocity.X * 0.03f;

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
                player.ClearBuff(ModContent.BuffType<boldBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<boldBuff>()))
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


            float speed = 7f;
            float inertia = 14f;

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
                    // deal much more damage while dashing
                    Projectile.velocity.X = Projectile.velocity.X * 0.97f;
                    Projectile.velocity.Y = Projectile.velocity.Y * 0.97f;

                    Dive_Timer++;
                }
                if (Dive_Timer == When_Dive)
                {
                    int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Cloud, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.014f;
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
                if (Dive_Timer > Dive_Reset)
                {
                    //Projectile.damage = reset_damage;
                    Diving = false;
                    Dive_Timer = 0;
                }
            }
            else
            {
                //Projectile.damage = reset_damage;
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