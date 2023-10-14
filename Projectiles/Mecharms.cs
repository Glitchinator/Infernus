using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Mecharms : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = false;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 102;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.height = 40;
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

        protected float shootCool = 6f;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<MechBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<MechBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            Projectile.Center = player.Center;
            Projectile.position.X = player.position.X - 45;
            Projectile.position.Y = player.position.Y - 16;

            float distanceFromTarget = 250f;
            Vector2 targetCenter = Projectile.position;
            bool foundTarget = false;

            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                {
                    targetCenter = npc.Center;
                    foundTarget = true;
                    Projectile.ai[0] = 0f;
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
                            foundTarget = true;
                            Projectile.ai[0] = 0f;
                        }
                    }
                }
            }
            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] += 1f;
                if (Main.rand.NextBool(3))
                {
                    Projectile.ai[1] += 1f;
                }
            }
            if (Projectile.ai[1] > shootCool)
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
                            shootVel *= 27;
                            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X + 40, Projectile.Center.Y - 30, shootVel.X, shootVel.Y, ModContent.ProjectileType<Mechhands>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X - 40, Projectile.Center.Y - 30, shootVel.X, shootVel.Y, ModContent.ProjectileType<Mechhands>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                            Projectile.ai[1] = 1f;
                        }
                    }
                }
            }
        }
    }
}