using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
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
            Projectile.localNPCHitCooldown = 20;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            Projectile.spriteDirection = Projectile.direction;

            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }

            Vector2 withplayer = player.Center;
            withplayer.Y -= 52f;
            float notamongusX = (25 + Projectile.minionPos * 5) * -player.direction;
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

            float speed = 18f;
            float inertia = 15f;

            if (foundTarget)
            {
                if (distanceFromTarget > 40f)
                {
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                if (distanceFromTarget < 10f)
                {
                    if (Main.rand.NextBool(17))
                    {
                        for (int k = 0; k < 38; k++)
                        {
                            Vector2 speed_Dust = Main.rand.NextVector2Circular(1f, 1f);
                            Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed_Dust * 32, DustID.Confetti_Pink, speed_Dust * 3, Scale: 2f);
                            Sword.noGravity = true;
                        }
                        Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, -5, ModContent.ProjectileType<Terra_2_Shot>(), 32, 0, Projectile.owner);
                        Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 5, ModContent.ProjectileType<Terra_2_Shot>(), 32, 0, Projectile.owner);
                        Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, -5, 0, ModContent.ProjectileType<Terra_2_Shot>(), 32, 0, Projectile.owner);
                        Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 5, 0, ModContent.ProjectileType<Terra_2_Shot>(), 32, 0, Projectile.owner);
                    }
                }
                if (distanceFromTarget > 150f)
                {
                    if (Projectile.ai[1] > 0f)
                    {
                        Projectile.ai[1] += 1f;
                        if (Main.rand.NextBool(3))
                        {
                            Projectile.ai[1] += 1f;
                        }
                    }
                    if (Projectile.ai[1] > 60)
                    {
                        Projectile.ai[1] = 0f;
                        Projectile.netUpdate = true;
                    }
                    if (Projectile.ai[0] == 0f)
                    {
                        if (foundTarget)
                        {
                            if (Projectile.ai[1] == 0f)
                            {
                                Projectile.ai[1] = 1f;
                                if (Main.myPlayer == Projectile.owner)
                                {
                                    Vector2 shootVel = targetCenter - Projectile.Center;
                                    if (shootVel == Vector2.Zero)
                                    {
                                        shootVel = new Vector2(0f, 1f);
                                    }
                                    shootVel.Normalize();
                                    shootVel *= 16;
                                    for (int k = 0; k < 12; k++)
                                    {
                                        Vector2 speed_Dust = Main.rand.NextVector2Circular(.3f, .3f);
                                        Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed_Dust * 32, DustID.Confetti_Pink, speed_Dust * 3, Scale: 2f);
                                        Sword.noGravity = true;
                                    }
                                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X + 40, Projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<Terra_2_Shot>(), 48, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                                    Projectile.ai[1] = 1f;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
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
        }
    }
}