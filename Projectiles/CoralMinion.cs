using Infernus.Buffs;
using Microsoft.Build.Framework;
using Microsoft.Xna.Framework;
using System;
using System.Threading;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class CoralMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 0.5f;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 22;
        }
        float RotationTimer;
        int rand_dist;
        float rand_speed;
        int timer;
        int rand_shoot_time;
        private Vector2 destination;
        float inertia = 20;
        float speed = 16f;
        public override void OnSpawn(IEntitySource source)
        {
            rand_dist = Main.rand.Next(20, 80);
            rand_shoot_time = Main.rand.Next(30, 60);
            rand_speed = Main.rand.NextFloat(1.5f,3f);
        }
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            //Projectile.spriteDirection = Projectile.direction;

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<ShellBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<ShellBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            //double deg = (double)Projectile.ai[1];
            //double rad = deg * (Math.PI / 180);

            //double dist = rand_dist;

            
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
            Projectile.velocity = (Projectile.velocity * (inertia - 1) + moveTo) / inertia;


            //Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            //Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

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
                if (timer >= rand_shoot_time)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        Vector2 speed_Dust = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(Projectile.Center + speed_Dust * 6, DustID.Water, speed_Dust, 0, default, Scale: 1.2f);
                        wand.noGravity = true;
                    }
                    Vector2 shootVel = targetCenter - Projectile.Center;
                    if (shootVel == Vector2.Zero)
                    {
                        shootVel = new Vector2(0f, 1f);
                    }
                    shootVel.Normalize();
                    shootVel *= 13;
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<Coral_Minion_Shot>(), (int)(Projectile.damage * 0.66f), Projectile.knockBack, Main.myPlayer, 0f, Projectile.owner);
                    timer = 0;
                }
            }
            else
            {
                timer = 0;
                Vector2 pos = Main.MouseWorld - Projectile.Center;
                var f = pos.ToRotation();
                Projectile.rotation = f;
            }
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
    }
}