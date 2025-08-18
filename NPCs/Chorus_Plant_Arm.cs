using Infernus.Items.Weapon.HardMode.Summon;
using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    public class Chorus_Plant_Arm: ModNPC
    {
        private Player player;
        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasParent => ParentIndex > -1;

        public int PositionIndex
        {
            get => (int)NPC.ai[1] - 1;
            set => NPC.ai[1] = value + 1;
        }

        public bool HasPosition => PositionIndex > -1;

        public ref float RotationTimer => ref NPC.ai[2];

        public static int BodyType()
        {
            return ModContent.NPCType<Boulderminiboss>();
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }
        int Timer;
        int saved_timer;
        bool save_time;
        int Ice_Shard_Area = 3;
        private Vector2 destination;
        float inertia = 3;
        float speed = 15f;
        bool dashing = false;

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 40;
            NPC.damage = 0;
            NPC.defense = 32;
            NPC.lifeMax = 6000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.0f;
            NPC.netAlways = true;
            NPC.aiStyle = -1;
            NPC.npcSlots = 0;
            NPC.boss = true;
        }
        public override void AI()
        {

            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            NPC.despawnEncouraged = false;

            if (Despawn())
            {
                return;
            }
            Vector2 dest = NPC.Center - destination;
            float gj = Magnitude(dest);
            if (gj > 80f)
            {
                if (dashing == true)
                {
                    if (save_time == false)
                    {
                        saved_timer = InfernusWorld.Boulder_Boss_Timer;
                        save_time = true;
                    }
                    InfernusWorld.Boulder_Boss_Timer = saved_timer;
                }
            }
            else
            {
                save_time = false;
            }

            Vector2 diddy = Main.npc[ParentIndex].Center - NPC.Center;
            var f = diddy.ToRotation();
            NPC.rotation = NPC.rotation.AngleTowards(f, .1f);


            if (Main.npc[ParentIndex].alpha < 3)
            {
                Timer = InfernusWorld.Boulder_Boss_Timer;
                if(Timer % 80 == 0)
                {
                    Shoot_Projectile_Random();
                }
            }
            /*
            Timer = InfernusWorld.Boulder_Boss_Timer;
            if (Timer == 90)
            {
                dashing = false;
                Shoot_Projectile_Random();
                //Timer = 0;
            }
            if (Timer == 135)
            {
                dashing = false;
                Shoot_Projectile_Random();
                //Timer = 0;
            }
            if (Timer == 180)
            {
                Shoot_Projectile_Random();
                //Timer = 0;
            }
            if (Timer == 225)
            {
                dashing = false;
                Shoot_Projectile_Random();
                //Timer = 0;
            }
            if (Timer == 270)
            {
                Shoot_Projectile_Random();
                //Timer = 0;
            }
            */
            /*
            if(Timer == 470)
            {
                dashing = true;
            }
            if (Timer >= 580)
            {
                spam_boulder_bolts();
                if (Timer % 50 == 0)
                {
                    Shoot_Projectile_Random();
                }
            }
            if (Timer == 900)
            {
                dashing = false;
                //InfernusWorld.Boulder_Boss_Timer = 0;
            }
            */
            Form_Ice();
        }
        public override void OnSpawn(IEntitySource source)
        {
            NPC.lifeMax = 6000;
            NPC.life = 6000;
        }
        private void Shoot_Projectile_Random()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 9f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 9f);
                }

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Boulder_Rain>(), 24, NPC.whoAmI);

                SoundEngine.PlaySound(SoundID.Item20, NPC.position);
            }
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = 0;
        }
        public override bool PreKill()
        {
            Main.npc[ParentIndex].alpha--;
            Despawn();
            return false;
        }
        private void spam_boulder_bolts()
        {
            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, (Main.npc[ParentIndex].Center - NPC.Center).SafeNormalize(Vector2.Zero) * 15f, ModContent.ProjectileType<Boulder_Bolt>(), 24, NPC.whoAmI);
        }
        private static float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        private bool Despawn()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && (!HasPosition || !HasParent || !Main.npc[ParentIndex].active || Main.npc[ParentIndex].type != BodyType()))
            {
                NPC.active = false;
                NPC.life = 0;
                NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
                for (int k = 0; k < 16; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Confetti_Pink, 4f, -2.5f, 0, default, 1f);
                }
                return true;
            }
            return false;
        }
        private void Form_Ice()
        {
            Player player = Main.player[NPC.target];

            float rad = (float)PositionIndex / 3 * MathHelper.TwoPi;

            RotationTimer += 0.8f;
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

            float distanceFromBody = Main.npc[ParentIndex].width + NPC.width + Ice_Shard_Area;

            Vector2 offset = Vector2.One.RotatedBy(rad) * distanceFromBody;

            destination = Main.npc[ParentIndex].Center + offset;

            Vector2 toDestination = destination - NPC.Center;
            Vector2 toDestinationNormalized = toDestination.SafeNormalize(Vector2.Zero);

            Vector2 moveTo = toDestinationNormalized * speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
        }
    }
}