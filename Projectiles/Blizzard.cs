using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Blizzard : ModProjectile
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
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 19;
        }
        bool close = false;
        int timer;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int k = 0; k < 14; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.HallowedPlants, 2.5f, -2.5f, 0, default, 1.2f);
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

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
                player.ClearBuff(ModContent.BuffType<IceBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<IceBuff>()))
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


            float speed = 14f;
            float inertia = 10f;

            if (foundTarget)
            {
                if (distanceFromTarget > 80f && close == false)
                {
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                else
                {
                    close = true;
                }
                if(close == true)
                {
                    timer++;
                    Projectile.velocity.Y = Projectile.velocity.Y *= 0.95f;
                    Projectile.velocity.X = Projectile.velocity.X *= 0.95f;
                }
                if (timer == 25)
                {
                    for (int k = 0; k < 11; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 16, DustID.HallowedPlants, speed2 * 2, Scale: 1f);
                        wand.noGravity = true;
                    }
                    for (int k = 0; k < 1; k++)
                    {
                        int x = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-10, 11), Main.rand.Next(-10, 11), ModContent.ProjectileType<Ice_Slash>(), (int)(Projectile.damage * 0.66f), 2f, Projectile.owner);
                        Main.projectile[x].DamageType = DamageClass.Summon;
                    }
                    Vector2 shootVel = targetCenter - Projectile.Center;
                    if (shootVel == Vector2.Zero)
                    {
                        shootVel = new Vector2(0f, 1f);
                    }
                    shootVel.Normalize();
                    shootVel *= 8;
                    int y = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<Ice_Slash>(), (int)(Projectile.damage * 0.66f), 2f, Main.myPlayer, 0f, Projectile.owner);
                    Main.projectile[y].DamageType = DamageClass.Summon;
                    timer = 0;
                    close = false;
                }
            }
            else
            {
                timer = 0;
                close = false;
                if (distanceToplayer > 600f)
                {
                    speed = 15f;
                    inertia = 35f;
                }
                else
                {
                    speed = 10f;
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
            Projectile.rotation += (float)Projectile.direction * 7;
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