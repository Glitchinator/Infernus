using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class BatMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
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
            Projectile.width = 26;
            Projectile.height = 18;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
        int timer;
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
                player.ClearBuff(ModContent.BuffType<BatBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<BatBuff>()))
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
                            targetCenter = new Vector2(npc.Center.X,npc.Top.Y - 80);
                            float detween = Vector2.Distance(targetCenter, Projectile.Center);
                            distanceFromTarget = detween;
                            foundTarget = true;
                        }
                    }
                }
            }
            Projectile.friendly = foundTarget;


            float speed = 17f;
            float inertia = 15f;

            if (foundTarget)
            {
                if (distanceFromTarget > 80f)
                {
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                else
                {
                    timer++;
                    if (timer == 30)
                    {
                        Vector2 shootVel = targetCenter - Projectile.Center;
                        if (shootVel == Vector2.Zero)
                        {
                            shootVel = new Vector2(0f, 1f);
                        }
                        shootVel.Normalize();
                        shootVel *= 9;
                        Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<Bat_Minion_Spit>(), (int)(Projectile.damage * 0.66f), Projectile.knockBack, Main.myPlayer, 0f, Projectile.owner);
                        timer = 0;
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
            target.AddBuff(BuffID.Poisoned, 300);
        }
    }
}