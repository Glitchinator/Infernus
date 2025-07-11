using Infernus.Buffs;
using Infernus.Items.Weapon.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Dunger : ModProjectile
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
            Projectile.width = 36;
            Projectile.height = 34;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 22;
        }
        bool teleport = false;
        int timer;
        float telex;
        float teley;
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = Main.rand.Next(0, 3);
        }
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = Projectile.velocity.X * 0.02f;

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
                player.ClearBuff(ModContent.BuffType<DungBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<DungBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            float distanceFromTarget = 100f;
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
                    telex = targetCenter.X += Main.rand.Next(0, npc.width);
                    teley = targetCenter.Y += Main.rand.Next(0, npc.height);
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
                            telex = targetCenter.X += Main.rand.Next(0, npc.width);
                            teley = targetCenter.Y += Main.rand.Next(0, npc.height);
                            foundTarget = true;
                        }
                    }
                }
            }
            Projectile.friendly = foundTarget;


            float speed = 10f;
            float inertia = 15f;

            if (foundTarget)
            {
                teleport = true;
                /*
                if (distanceFromTarget > 200f)
                {
                    teleport = true;
                    //Vector2 direction = targetCenter - Projectile.Center;
                    //direction.Normalize();
                    //direction *= speed;
                    //Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                */
                if (teleport == true)
                {
                    timer++;
                    Projectile.velocity.Y = Projectile.velocity.Y *= 0.95f;
                    Projectile.velocity.X = Projectile.velocity.X *= 0.95f;
                }
                if (timer == 50)
                {
                    for (int k = 0; k < 11; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 16, DustID.ShadowbeamStaff, speed2 * 2, Scale: 1f);
                        wand.noGravity = true;
                    }
                    Vector2 dest = new Vector2(telex, teley);
                    var destnorm = dest.SafeNormalize(Vector2.UnitY);
                    Dust.QuickDustLine(Projectile.Center + destnorm * Projectile.width, dest, dest.Length() / 20f, Color.Purple);
                    Projectile.Center = new Vector2(telex,teley);
                }
                if (timer == 73)
                {
                    //explode
                    SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Dunger_Exlos>(), (int)(Projectile.damage * 0.7f), 4f, Main.myPlayer, 0f, 0f);
                    for (int k = 0; k < 27; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Circular(2f, 4f);
                        Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed2 * 32, DustID.ShadowbeamStaff, speed2 * 3, Scale: 2f);
                        Sword.noGravity = true;
                    }
                }    
                if (timer == 120)
                {
                    Projectile.Center = withplayer;
                    for (int k = 0; k < 5; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 8, DustID.ShadowbeamStaff, speed2 * 2, Scale: 1f);
                        wand.noGravity = true;
                    }
                    teleport = false;
                    timer = 0;
                }
            }
            else
            {
                teleport = false;
                timer = 0;
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