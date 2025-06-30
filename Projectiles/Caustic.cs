using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Caustic : ModProjectile
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
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
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
        bool Spinning = false;
        int spin_timer;

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = Main.rand.Next(0, 3);
            When_Dive = Main.rand.Next(10, 21);
            Dive_Reset = Main.rand.Next(70, 81);
        }
        public override void AI()
        {

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
                player.ClearBuff(ModContent.BuffType<CasBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<CasBuff>()))
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


            float speed = 12f;
            float inertia = 14f;

            if (foundTarget)
            {
                if (Spinning == false)
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
                        Projectile.velocity.X = Projectile.velocity.X * 0.98f;
                        Projectile.velocity.Y = Projectile.velocity.Y * 0.98f;

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
                    if (Dive_Timer > When_Dive)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - 0.2f;
                    }
                }
                if (Dive_Timer > Dive_Reset)
                {
                    Spinning = true;
                    Diving = false;
                    Dive_Timer = 0;
                }
                if (Spinning == true)
                {
                    float speed_spin = 3.5f;
                    float inertia_spin = 28f;
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed_spin;
                    Projectile.velocity = (Projectile.velocity * (inertia_spin - 1) + direction) / inertia_spin;
                    spin_timer++;
                    Projectile.rotation += 0.2f * (float)Projectile.direction;
                }
                else
                {
                    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                }
                if (spin_timer == 45 || spin_timer == 90)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, -5, ModContent.ProjectileType<Caustic_Thorn>(), (int)(Projectile.damage * 0.4f), 2f, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 5, ModContent.ProjectileType<Caustic_Thorn>(), (int)(Projectile.damage * 0.4f), 2f, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, -5, 0, ModContent.ProjectileType<Caustic_Thorn>(), (int)(Projectile.damage * 0.4f), 2f, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 5, 0, ModContent.ProjectileType<Caustic_Thorn>(), (int)(Projectile.damage * 0.4f), 2f, Projectile.owner);
                }
                if (spin_timer == 90)
                {
                    Spinning = false;
                    spin_timer = 0;
                }
            }
            else
            {
                Dive_Timer = 0;
                Diving = false;
                spin_timer = 0;
                Spinning = false;
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 300);
            target.AddBuff(BuffID.Frostburn2, 300);
            target.AddBuff(BuffID.Poisoned, 300);
            target.AddBuff(BuffID.CursedInferno, 300);
            target.AddBuff(BuffID.Ichor, 300);
            target.AddBuff(BuffID.ShadowFlame, 300);
            target.AddBuff(BuffID.Venom, 300);
        }
    }
}