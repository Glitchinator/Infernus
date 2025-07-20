using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class EqualMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 0;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 40;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 46;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }

        int Dive_Timer;
        bool Diving = false;
        int When_Dive;
        int Dive_Reset;
        int combo;
        float combo_dist;

        public override void OnSpawn(IEntitySource source)
        {
            When_Dive = Main.rand.Next(14, 18);
            Dive_Reset = Main.rand.Next(24, 36);
            combo_dist = 160f; 
        }
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            Projectile.spriteDirection = Projectile.direction;

            Vector2 withplayer = player.Center;
            withplayer.Y -= 52f;
            float notamongusX = (5 + Projectile.minionPos * 5) * -player.direction;
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
                player.ClearBuff(ModContent.BuffType<EqualBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<EqualBuff>()))
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
            float inertia = 17f;

            if (foundTarget)
            {
                if (distanceFromTarget > combo_dist && Diving == false)
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
                    if (Main.myPlayer == Projectile.owner && combo < 3)
                    {
                        float dive_speed = 18f;
                        Vector2 VectorToCursor = targetCenter - Projectile.position;
                        float DistToCursor = VectorToCursor.Length();

                        DistToCursor = dive_speed / DistToCursor;
                        VectorToCursor *= DistToCursor;

                        Projectile.velocity = VectorToCursor;
                    }
                    if (Main.myPlayer == Projectile.owner && combo >= 3)
                    {
                        Projectile.velocity = Vector2.Zero;
                        Vector2 shootVel = targetCenter - Projectile.Center;
                        if (shootVel == Vector2.Zero)
                        {
                            shootVel = new Vector2(0f, 1f);
                        }
                        shootVel.Normalize();
                        shootVel *= 16;
                        Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<EqualMinion_Shot>(), (int)(Projectile.damage * 0.15f), 1f, Main.myPlayer, 0f, Projectile.owner);
                    }
                    combo += 1;
                    if (combo == 3)
                    {
                        combo_dist = 700f;
                    }
                    if (combo == 5)
                    {
                        combo_dist = 160f;
                        combo = 0;
                    }
                }
                if (Diving == true && combo >= 3)
                {
                    Vector2 pos = targetCenter - Projectile.Center;
                    var f = pos.ToRotation() + MathHelper.PiOver2;
                    Projectile.rotation = Projectile.rotation.AngleTowards(f, 0.3f);
                }
                else
                {
                    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                }
                if (Dive_Timer > Dive_Reset)
                {
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
    }
}