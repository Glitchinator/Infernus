using Infernus.Items.BossSummon;
using Infernus.Items.Weapon.HardMode.Melee;
using Infernus.Projectiles;
using Infernus.Projectiles.Raiko.Boss;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    [AutoloadBossHead]

    public class Boulderminiboss : ModNPC
    {
        private Player player;
        private float speed;

        public int ArmIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasArms => ArmIndex > -1;

        public int PositionIndex2
        {
            get => (int)NPC.ai[1] - 1;
            set => NPC.ai[1] = value + 1;
        }

        public bool HasPosition1 => PositionIndex2 > -1;
        public static int BodyType2()
        {
            return ModContent.NPCType<Chorus_Plant_Arm>();
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public static int Arms_Left = 3;

        public override void SetDefaults()
        {
            NPC.lifeMax = 24000;
            NPC.damage = 50;
            NPC.defense = 30;
            NPC.knockBackResist = 0.0f;
            NPC.width = 50;
            NPC.height = 60;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit10;
            NPC.DeathSound = SoundID.Roar;
            NPC.value = 30000;
            NPC.boss = true;
            AIType = NPCID.AngryBones;
            Music = MusicLoader.GetMusicSlot("Infernus/Music/Volcanic_Disaster");
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.npcSlots = 10;
        }
        int Move_X;
        int Move_Y;
        int Move_Location = 0;
        int Timer;
        bool is_dashing = false;
        bool spawned_second_Shield = false;
        bool timer_reset = false;
        bool Timer_Halt = false;
        bool Spin = false;
        bool Stay_Above = false;
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            if (Spin == false)
            {
                NPC.rotation = NPC.velocity.X * 0.02f;
            }
            else
            {
                NPC.rotation = NPC.rotation += 0.2f;
            }

            if (NPC.life <= (NPC.lifeMax / 2) && spawned_second_Shield == false)
            {
                NPC.alpha = Arms_Left;
                Spawn_IceShards();
                timer_reset = false;
                spawned_second_Shield = true;
            }

            if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.velocity.Y = NPC.velocity.Y + .3f;
                if (NPC.timeLeft > 20)
                {
                    NPC.timeLeft = 20;
                    return;
                }
            }
            if (is_dashing == true)
            {
                NPC.velocity.X = NPC.velocity.X * 0.98f;
                NPC.velocity.Y = NPC.velocity.Y * 0.98f;
            }
            Move(new Vector2(Move_X, Move_Y));
            if (Move_Location == 0)
            {
                Move_X = 0;
                Move_Y = -400;
            }
            if (Move_Location == 1)
            {
                Move_X = 300;
                Move_Y = -460;
            }
            if (Move_Location == 2)
            {
                Move_X = -300;
                Move_Y = -500;
            }
            if (Move_Location == 3)
            {
                Move_X = 0;
                Move_Y = -300;
            }
            if (Move_Location == 4)
            {
                Move_X = 300;
                Move_Y = 0;
            }
            if (Move_Location == 5)
            {
                Move_X = 0;
                Move_Y = 300;
            }
            if (Stay_Above == true)
            {
                Move_Location = 0;
            }
            if(Timer_Halt == false)
            {
                InfernusWorld.Boulder_Boss_Timer++;
            }
            Timer = InfernusWorld.Boulder_Boss_Timer;
            if (NPC.alpha == 0 && timer_reset == false)
            {
                InfernusWorld.Boulder_Boss_Timer = 0;
                timer_reset = true;
            }
            if (spawned_second_Shield == false && NPC.alpha > 0)
            {
                // first phase w shields
                if (Timer >= 0)
                {
                    //Main.NewText("First phase with shields", 229, 214, 127);
                }
                if (Timer == 90)
                {
                    //shields shoot
                }
                if (Timer == 180)
                {
                    //shields shoot
                }
                if (Timer == 270)
                {
                    //shields shoot
                }
                if (Timer == 350)
                {
                    is_dashing = true;
                }
                if (Timer == 400)
                {
                    PreDash();
                }
                if (Timer == 420)
                {
                    Dash();
                }
                if (Timer == 470)
                {
                    Super_Dash();
                }
                if (Timer == 510)
                {
                    is_dashing = false;
                }
                if (Timer == 560)
                {
                    InfernusWorld.Boulder_Boss_Timer = 0;
                }
            }
            if (spawned_second_Shield == false && NPC.alpha == 0)
            {
                // first phase w/ shields
                if (Timer >= 0)
                {
                    //Main.NewText("First phase without shields", 229, 214, 127);
                }
                if (Timer == 100)
                {
                    Rapid_Fire_Shotgun();
                }
                if (Timer == 120)
                {
                    is_dashing = true;
                    Dash();
                }
                if (Timer >= 200 && Timer <= 300)
                {
                    Heresy_Rapid();
                    Knockback();
                }
                if (Timer == 360)
                {
                    is_dashing = false;
                }
                if (Timer == 440)
                {
                    Move_Location = 3;
                    Timer_Halt = true;
                    InfernusWorld.Boulder_Boss_Timer += 1;
                }
                
                if (Timer == 540)
                {
                    PreDash();
                }
                if (Timer == 600)
                {
                    Dash();
                }
                if (Timer == 700)
                {
                    Timer_Halt = true;
                    Stay_Above = true;
                    Spin = true;
                    InfernusWorld.Boulder_Boss_Timer += 1;
                }
                if (Timer == 900)
                {
                    Spin = false;
                    Stay_Above = false;
                    InfernusWorld.Boulder_Boss_Timer = 0;
                }
            }
            if (spawned_second_Shield == true && NPC.alpha > 0)
            {
                // second phase w shields
                if (Timer >= 0)
                {
                    Main.NewText("Second phase with shields", 229, 214, 127);
                }
            }
            if (spawned_second_Shield == true && NPC.alpha == 0)
            {
                // second phase w/ shields
                if (Timer >= 0)
                {
                    Main.NewText("Second phase without shields", 229, 214, 127);
                }
            }
            /*
            if (Timer == 60)
            {
                Timer = 11000;
            }
            if (Timer == 100)
            {
                //Boulder_Rain();
                //SoundEngine.PlaySound(SoundID.Item74, NPC.position);
            }
            if (Timer == 150)
            {
                Homing_Shotts();
                SoundEngine.PlaySound(SoundID.Item60, NPC.position);
            }
            if (Timer == 200)
            {
                Homing_Shotts();
                SoundEngine.PlaySound(SoundID.Item60, NPC.position);
            }
            if (Timer == 250)
            {
                Homing_Shotts();
                SoundEngine.PlaySound(SoundID.Item60, NPC.position);
            }
            if (Timer == 300)
            {
                Homing_Shotts();
                SoundEngine.PlaySound(SoundID.Item60, NPC.position);
            }
            if (Timer == 400)
            {
                // dash starts
                Timer = 10000;
            }
            if (Timer == 500)
            {
                //Boulder_Rain();
                //SoundEngine.PlaySound(SoundID.Item74, NPC.position);
            }
            if (Timer == 600)
            {
                Timer = 12000;
            }
            if (Timer == 700)
            {
                //Boulder_Rain();
                //SoundEngine.PlaySound(SoundID.Item74, NPC.position);
            }
            if (Timer == 800)
            {
                Timer = 0;
            }
            
            #region first_phase_attacks
            if (Timer == 10000)
            {
                Rock_Rain_Dash();
                Dash();
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
            }
            if (Timer == 10020)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10040)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10060)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10080)
            {
                Dash();
                Rock_Rain_Dash();
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
            }
            if (Timer == 10100)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10120)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10140)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10160)
            {
                Rock_Rain_Dash();
                Dash();
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
            }
            if (Timer == 10180)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10200)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10220)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10240)
            {
                Dash();
                Rock_Rain_Dash();
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                is_dashing = true;
            }
            if (Timer == 10260)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10280)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10290)
            {
                Rock_Rain_Dash();
                Timer = 440;
                is_dashing = false;
            }
            if (Timer >= 11000)
            {
                if (InfernusSystem.Level_systemON == true)
                {
                    Heresy_Rapid();
                }
            }
            if (Timer == 11000)
            {
                Teleport_Up_Center();
            }
            if (Timer == 11015)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11030)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11045)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11060)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11075)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11090)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11105)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11120)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11135)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11150)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11165)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11180)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11195)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11210)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Rapid_Fire_Shotgun();
            }
            if (Timer == 11300)
            {
                Timer = 70;
            }
            if (Timer == 12000)
            {
                Teleport_Up_Center();
            }
            if (Timer == 12060)
            {
                SoundEngine.PlaySound(SoundID.Item70, NPC.position);
                Shotgun_360();
            }
            if (Timer == 12120)
            {
                SoundEngine.PlaySound(SoundID.Item70, NPC.position);
                Shotgun_360();
            }
            if (Timer == 12180)
            {
                SoundEngine.PlaySound(SoundID.Item70, NPC.position);
                Shotgun_360();
            }
            if (Timer == 12240)
            {
                SoundEngine.PlaySound(SoundID.Item70, NPC.position);
                Shotgun_360();
            }
            if (Timer == 12300)
            {
                SoundEngine.PlaySound(SoundID.Item70, NPC.position);
                Shotgun_360();
            }
            if (Timer == 12360)
            {
                SoundEngine.PlaySound(SoundID.Item70, NPC.position);
                Shotgun_360();
            }
            if (Timer == 12420)
            {
                SoundEngine.PlaySound(SoundID.Item70, NPC.position);
                Shotgun_360();
            }
            if (Timer == 12480)
            {
                SoundEngine.PlaySound(SoundID.Item70, NPC.position);
                Shotgun_360();
            }
            if (Timer == 12600)
            {
                Timer = 650;
            }
            #endregion
            */


            if (NPC.alpha == 0 && Arms_Left > 0)
            {
                NPC.dontTakeDamage = false;
                return;
            }
            NPC.dontTakeDamage = true;
        }
        private void PreDash()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 2.25f;
                NPC.velocity.Y *= 2.25f;
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - player.position.Y, NPC.Center.X - player.position.X);
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 9);
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 9);
                }
            }
        }
        private void Knockback()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 2.25f;
                NPC.velocity.Y *= 2.25f;
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - player.position.Y, NPC.Center.X - player.position.X);
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 2);
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 2);
                }
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            Arms_Left = 3;
            Spawn_IceShards();
            NPC.alpha = Arms_Left;
        }
        private void Spawn_IceShards()
        {
            for (int i = 0; i < Arms_Left; i++)
            {
                int NPC_ = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Chorus_Plant_Arm>(), NPC.whoAmI);
                NPC Ice = Main.npc[NPC_];

                if (Ice.ModNPC is Chorus_Plant_Arm minion)
                {
                    minion.ParentIndex = NPC.whoAmI;
                    minion.PositionIndex = i;

                }

                if (Main.netMode == NetmodeID.Server && NPC_ < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: NPC_);
                }
            }
        }
        private void Move(Vector2 offset)
        {
            if (Timer >= 10000 || is_dashing == true)
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.SolarFlare, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f, 0, default, 1.7f);
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Torch, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f, 0, default, 1.7f);
                return;
            }
            player = Main.player[NPC.target];
            //speed = 7f;
            Vector2 moveTo = player.Center + offset;
            Dust.NewDust(moveTo, 10, 10, DustID.SolarFlare, 0, 0);
            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
            if (magnitude <= 80f)
            {
                if (Move_Location >= 3 && Move_Location <= 5)
                {
                    Rapid_Fire_Shotgun();
                    Move_Location++;
                }
                else
                {
                    Move_Location = Main.rand.Next(3);
                    Timer_Halt = false;
                }
                if (Spin == true)
                {
                    Homing_Shotts();
                }
                speed = 11.5f;
            }
            if (Move_Location == Move_Location)
            {
                speed += 0.1f;
            }
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 36f;
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity = move;
        }
      
        private void Dash()
        {
            SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
            player = Main.player[NPC.target];
            var distancevec2 = player.position - NPC.position;
            float magnitude = Magnitude(distancevec2);
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 2.25f;
                NPC.velocity.Y *= 2.25f;
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - (player.position.Y + Main.rand.Next(-40, 40)), NPC.Center.X - (player.position.X + Main.rand.Next(-40, 40)));
                    if (magnitude >= 700f)
                    {
                        magnitude = 700;
                    }
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 12 * (magnitude / 200)) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 12 * (magnitude / 200)) * -1;
                    string i = magnitude.ToString();
                    Main.NewText(i, 229, 214, 127);
                }
            }
        }
        private void Super_Dash()
        {
            SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
            player = Main.player[NPC.target];
            var distancevec2 = player.position - NPC.position;
            float magnitude = Magnitude(distancevec2);
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 2.25f;
                NPC.velocity.Y *= 2.25f;
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - (player.position.Y + player.velocity.Y * 10), NPC.Center.X - (player.position.X + player.velocity.X * 10));
                    if (magnitude >= 800f)
                    {
                        magnitude = 800;
                    }
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 12 * (magnitude / 200)) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 12 * (magnitude / 200)) * -1;
                    string i = magnitude.ToString();
                    Main.NewText(i, 229, 214, 127);
                }
            }
            for (int i = 0; i < 30; i++)
            {
                var smoke = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                smoke.velocity *= 1.4f;
            }

            // Spawn a bunch of fire dusts.
            for (int j = 0; j < 20; j++)
            {
                var fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Stone, 0f, 0f, 100, default, 1.4f);
                fireDust.noGravity = true;
                fireDust.velocity *= 7f;
            }

            // Spawn a bunch of smoke gores.
            for (int k = 0; k < 2; k++)
            {
                float speedMulti = 0.8f;
                if (k == 1)
                {
                    speedMulti = 1.6f;
                }

                var smokeGore = Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                smokeGore.velocity *= speedMulti;
                smokeGore.velocity += Vector2.One;
                smokeGore = Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                smokeGore.velocity *= speedMulti;
                smokeGore.velocity.X -= 1.2f;
                smokeGore.velocity.Y += 1.2f;
                smokeGore = Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                smokeGore.velocity *= speedMulti;
                smokeGore.velocity.X += 1.2f;
                smokeGore.velocity.Y -= 1.2f;
                smokeGore = Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                smokeGore.velocity *= speedMulti;
                smokeGore.velocity -= Vector2.One;
            }
        }
        private void Teleport_Up_Center()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(0f, -400f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
                SoundEngine.PlaySound(SoundID.Item6 with
                {
                    Volume = 1f,
                    Pitch = -0.65f,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
            }
        }
        private void Shotgun_360()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Bottom;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 8f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 7f);
                }

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Boulder_Bolt>(), 14, NPC.whoAmI);
                float rotation = MathHelper.ToRadians(18);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation, -rotation, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Boulder_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation2 = MathHelper.ToRadians(36);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation2, -rotation2, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Boulder_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation3 = MathHelper.ToRadians(54);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation3, -rotation3, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Boulder_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation4 = MathHelper.ToRadians(72);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation4, -rotation4, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Boulder_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation5 = MathHelper.ToRadians(90);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation5, -rotation5, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Boulder_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation6 = MathHelper.ToRadians(108);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation6, -rotation6, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Boulder_Bolt>(), 14, NPC.whoAmI);
                }
            }
        }
        private void Heresy_Rapid()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Bottom;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 8f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 7f);
                }

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, velocity, ModContent.ProjectileType<Boulder_Bolt>(), 20, NPC.whoAmI);
            }
        }
        private void Rapid_Fire_Shotgun()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Bottom;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 8f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 7f);
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Boulder_Bolt>(), 21, NPC.whoAmI);
                float rotation = MathHelper.ToRadians(10);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation, -rotation, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Boulder_Bolt>(), 21, NPC.whoAmI);
                }
                float rotation2 = MathHelper.ToRadians(32);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation2, -rotation2, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Boulder_Bolt>(), 21, NPC.whoAmI);
                }
            }
        }
        private void Rock_Rain_Dash()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                {
                    Vector2 velocity = new(10, Main.rand.Next(-8,8));
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 3f / magnitude;
                    }
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top, velocity, ModContent.ProjectileType<Boulder_Rain>(), 20, NPC.whoAmI);
                }
                {
                    Vector2 velocity = new(-10, Main.rand.Next(-8, 8));
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 3f / magnitude;
                    }
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top, velocity, ModContent.ProjectileType<Boulder_Rain>(), 20, NPC.whoAmI);
                }
            }
        }
        private void Homing_Shotts()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 1; i++)
                {
                    player = Main.player[NPC.target];
                    Vector2 velocity = player.Center - NPC.Center;
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 5f / magnitude;
                    }
                    else
                    {
                        velocity = new Vector2(0f, 5f);
                    }
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(360));

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, newVelocity, ModContent.ProjectileType<Boulder_Rocket>(), 20, NPC.whoAmI);
                }
            }
        }
        private void Boulder_Rain()
        {
            int damage = 16;
            var proj = ModContent.ProjectileType<Boulder_Rain>();
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(100f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(200f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(300f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(400f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(500f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(600f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(700f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-100f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-200f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-300f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-400f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-500f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-600f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-700f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 6f })), proj, damage, NPC.whoAmI);
            }
        }
        private static float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 60; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 4f, -2.5f, 0, default, 2f);
                }
                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Boulder").Type);
                }
            }
        }
        public override void OnKill()
        {
            InfernusSystem.downedBoulderBoss = true;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Diddy stone")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Boulder_Bag>()));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Magic.Bouldermagicweapon>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Magic.BoulderTomb>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Melee.bould>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Ranged.Bog>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Summon.Whiprock>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Accessories.Wings>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Accessories.HiveHeart>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Materials.Broken_Heros_Staff>(), 2));

            npcLoot.Add(notExpertRule);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * 1.3f);
            NPC.lifeMax = (NPC.lifeMax = 28000 * (int)balance);

            if (Main.masterMode == true)
            {
                NPC.lifeMax = (NPC.lifeMax = 32000 * (int)balance);
                NPC.life = (NPC.lifeMax = 32000 * (int)balance);
                NPC.damage = ((NPC.damage / 2) * 3);
            }
            if (Main.getGoodWorld == true)
            {
                NPC.scale = 2f;
                NPC.lifeMax = (NPC.lifeMax = 36000 * (int)balance);
                NPC.life = (NPC.lifeMax = 36000 * (int)balance);
                NPC.damage = ((NPC.damage / 10) * 13);
            }
        }
    }
}