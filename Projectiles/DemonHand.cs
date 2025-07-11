using Infernus.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class DemonHand : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.width = 48;
            Projectile.height = 32;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
        float RotationTimer;
        int rand_dist;
        float rand_speed;
        int timer;
        int rand_shoot_time;
        bool dashing = false;
        int dash_cooldown;
        private Vector2 destination;
        float inertia = 20;
        float speed = 16f;
        private Vector2 npcspeed;
        public override void OnSpawn(IEntitySource source)
        {
            rand_dist = Main.rand.Next(15, 50);
            rand_shoot_time = Main.rand.Next(45, 55);
            rand_speed = Main.rand.NextFloat(1.5f, 3f);
            dash_cooldown = rand_shoot_time + 16;
        }
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            //Projectile.spriteDirection = Projectile.direction;

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<DemonBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<DemonBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            float rad = (float)player.numMinions / 4 * MathHelper.TwoPi;

            RotationTimer += rand_speed;
            if (RotationTimer > 360)
            {
                RotationTimer = 0;
            }
            float continuousRotation = MathHelper.ToRadians(RotationTimer);
            rad += continuousRotation;
            if (rad > MathHelper.TwoPi)
            {
                rad -= MathHelper.TwoPi;
            }
            else if (rad < 0)
            {
                rad += MathHelper.TwoPi;
            }

            float distanceFromBody = player.width + Projectile.width + rand_dist;

            Vector2 offset = Vector2.One.RotatedBy(rad) * distanceFromBody;

            destination = player.Center + offset;

            Vector2 toDestination = destination - Projectile.Center;
            Vector2 toDestinationNormalized = toDestination.SafeNormalize(Vector2.Zero);
            Vector2 moveTo = toDestinationNormalized * speed;
            //mub = toDestinationNormalized;

            if (dashing == false)
            {
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + moveTo) / inertia;
            }
            else
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.SolarFlare, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.014f;
                Projectile.velocity.X = Projectile.velocity.X * 0.97f;
                Projectile.velocity.Y = Projectile.velocity.Y * 0.97f;
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
                    targetCenter = npc.Center;
                    float detween = Vector2.Distance(targetCenter, Projectile.Center);
                    distanceFromTarget = detween;
                    npcspeed = npc.velocity;
                    npcspeed.Normalize();
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
                            targetCenter = npc.Center;
                            float detween = Vector2.Distance(targetCenter, Projectile.Center);
                            distanceFromTarget = detween;
                            npcspeed = npc.velocity;
                            npcspeed.Normalize();
                            foundTarget = true;
                        }
                    }
                }
            }
            if (foundTarget)
            {
                Vector2 pos = targetCenter - Projectile.Center;
                var f = pos.ToRotation();
                Projectile.rotation = f;
                timer++;
                if (distanceFromTarget < 300f)
                {
                    dashing = true;
                    if (timer == rand_shoot_time)
                    {
                        for (int k = 0; k < 6; k++)
                        {
                            Vector2 speed_Dust = Main.rand.NextVector2Unit();
                            Dust wand = Dust.NewDustPerfect(Projectile.Center + speed_Dust * 6, DustID.SolarFlare, speed_Dust, 0, default, Scale: 1.4f);
                            wand.noGravity = true;
                        }
                        SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);
                        if (Main.myPlayer == Projectile.owner)
                        {
                            float dive_speed = 19f;
                            Vector2 VectorToCursor = targetCenter - Projectile.position;
                            float DistToCursor = VectorToCursor.Length();

                            DistToCursor = dive_speed / DistToCursor;
                            VectorToCursor *= DistToCursor;

                            Projectile.velocity = VectorToCursor;
                        }
                    }
                    if (timer >= dash_cooldown)
                    {
                        timer = 0;
                        dashing = false;
                    }
                }
                else
                {
                    if (timer >= rand_shoot_time)
                    {
                        Vector2 shootVel = targetCenter - Projectile.Center;
                        if (shootVel == Vector2.Zero)
                        {
                            shootVel = new Vector2(0f, 1f);
                        }
                        shootVel.Normalize();
                        shootVel *= 18;
                        Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, shootVel.X,shootVel.Y, ModContent.ProjectileType<DemonHand_Shot>(), (int)(Projectile.damage * 0.66f), 3f, Main.myPlayer, 0f, Projectile.owner);
                        timer = 0;
                        dashing = false;
                    }
                }
            }
            else
            {
                dashing = false;
                timer = 0;
                Vector2 pos = Main.MouseWorld - Projectile.Center;
                var f = pos.ToRotation();
                Projectile.rotation = f;
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