using Infernus.Projectiles;
using Infernus.Projectiles.Raiko.Boss;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    [AutoloadBossHead]
    public class Shark : ModNPC
    {
        private Player player;
        private float speed;
        private bool Is_Spinning = false;
        int Spawn_Area;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;

            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Electrified] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn2] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 70650;
            NPC.damage = 70;
            NPC.defense = 38;
            NPC.knockBackResist = 0.0f;
            NPC.width = 138;
            NPC.height = 62;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit56;
            NPC.DeathSound = SoundID.NPCDeath1;
            AnimationType = NPCID.DemonEye;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 36);
            NPC.boss = true;
            Music = MusicLoader.GetMusicSlot("Infernus/Music/Eye_of_the_Ocean");
        }
        int Timer;
        //bool waitingg = true;
        //bool moving = false;
        bool is_dashing = false;
        int Move_Location = 0;
        int Move_X = 0;
        int Move_Y = 0;
        bool in_position = false;
        bool check_position = false;
        bool should_track = false;
        bool looking_at_player = false;
        bool once = false;
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            if (looking_at_player == false && Is_Spinning == false)
            {
                var f = NPC.velocity.ToRotation();
                NPC.rotation = NPC.rotation.AngleTowards(f, 0.6f);
            }
            if (looking_at_player == true && Is_Spinning == false)
            {
                Vector2 pos = player.Center - NPC.Center;
                var f = pos.ToRotation();
                NPC.rotation = NPC.rotation.AngleTowards(f, 0.3f);
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
            string i = speed.ToString();
            Main.NewText(i, 229, 214, 127);
            if (is_dashing == true)
            {
                NPC.velocity.X = NPC.velocity.X * 0.988f;
                NPC.velocity.Y = NPC.velocity.Y * 0.988f;
            }
            if(should_track == true)
            {
                Tracking();
            }
            {
                if(Move_Location == 0)
                {
                    Move_X = 0;
                    Move_Y = 0;
                }
                if (Move_Location == 1)
                {
                    Move_X = 0;
                    Move_Y = 400;
                }
                if (Move_Location == 2)
                {
                    Move_X = 0;
                    Move_Y = 800;
                }
                if (Move_Location == 3)
                {
                    Move_X = 0;
                    Move_Y = -700;
                }
                if(Move_Location == 4)
                {
                    // create own shit
                }
                if (Move_Location == 5)
                {
                    Move_X = 200;
                    Move_Y = -400;
                }
                if (Move_Location == 6)
                {
                    Move_X = -200;
                    Move_Y = -400;
                }
                if (Move_Location == 7)
                {
                    Move_X = 000;
                    Move_Y = -600;
                }
                Move(new Vector2(Move_X, Move_Y));
                if (check_position == false)
                {
                    Timer++;
                }
                /*
                if (Timer == 120)
                {
                    Move_Location = 5;
                    check_position = true;
                }
                if (Timer == 180)
                {
                    Move_Location = 6;
                    check_position = true;
                }
                if (Timer == 240)
                {
                    Move_Location = 7;
                    check_position = true;
                }
                if (Timer == 370)
                {
                    Move_Location = 0;
                }
                if (Timer == 450)
                {

                }
                */
                if (Timer == 120)
                {
                    // dashes
                    Timer = 10000;
                }
                if (Timer == 240)
                {
                    // move to top
                    Timer = 7000;
                }
                if(Timer == 261)
                {
                    // finish above spin cyclones
                    Is_Spinning = false;
                    in_position = false;
                    check_position = false;
                    Move_Location = 0;
                }
                if (Timer == 330)
                {
                    SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                }
                if (Timer == 390)
                {
                    SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                }
                if(Timer == 450)
                {
                    // predictive dashes
                    Timer = 12000;
                }
                if(Timer == 600)
                {
                    // falling lighting booms moving to top
                    Timer = 8000;
                }
                if (Timer == 601)
                {
                    Is_Spinning = false;
                    in_position = false;
                    check_position = false;
                    Move_Location = 0;
                }
                if(Timer == 720)
                {
                    is_dashing = true;
                    if (player.direction == 1)
                    {
                        Lightning_Side_Left();
                    }
                    if (player.direction == -1)
                    {
                        Lightning_Side_Right();
                    }
                }
                if (Timer == 800)
                {
                    Dash();
                    if (player.direction == 1)
                    {
                        Lightning_Side_Left();
                    }
                    if (player.direction == -1)
                    {
                        Lightning_Side_Right();
                    }
                }
                if (Timer == 880)
                {
                    if (player.direction == 1)
                    {
                        Lightning_Side_Left();
                    }
                    if (player.direction == -1)
                    {
                        Lightning_Side_Right();
                    }
                }
                if (Timer == 960)
                {
                    Dash();
                    if (player.direction == 1)
                    {
                        Lightning_Side_Left();
                    }
                    if (player.direction == -1)
                    {
                        Lightning_Side_Right();
                    }
                }
                if(Timer == 1040)
                {
                    is_dashing = false;
                }
                if (Timer == 1100)
                {
                    Timer = 0;
                }

                if (Timer == 7001)
                {
                    if (in_position == false)
                    {
                        check_position = true;
                    }
                    Move_Location = 1;
                    in_position = true;
                }
                if (in_position = true && check_position == false && Timer == 7002)
                {
                    Timer = 17000;
                    is_dashing = true;
                    //waitingg = true;
                    //moving = false;
                }
                if (Timer == 8001)
                {
                    if (in_position == false)
                    {
                        check_position = true;
                    }
                    Move_Location = 2;
                    in_position = true;
                }
                if (in_position = true && check_position == false && Timer == 8002)
                {
                    Timer = 11000;
                    //waitingg = true;
                    //moving = false;
                }
                #region First_Phase_Attacks;
                if (Timer == 10000)
                {
                    is_dashing = true;
                    Dash();
                }
                if (Timer == 10010)
                {
                    Lightning_Trail();
                }
                if (Timer == 10030)
                {
                    Lightning_Trail();
                }
                if (Timer == 10050)
                {
                    Lightning_Trail();
                }
                if (Timer == 10060)
                {
                    Dash();
                }
                if (Timer == 10070)
                {
                    Lightning_Trail();
                }
                if (Timer == 10090)
                {
                    Lightning_Trail();
                }
                if (Timer == 10110)
                {
                    Lightning_Trail();
                }
                if (Timer == 10120)
                {
                    Dash();
                }
                if (Timer == 10130)
                {
                    Lightning_Trail();
                }
                if (Timer == 10150)
                {
                    Lightning_Trail();
                }
                if (Timer == 10170)
                {
                    Lightning_Trail();
                }
                if (Timer == 10180)
                {
                    Dash();
                }
                if (Timer == 10190)
                {
                    Lightning_Trail();
                }
                if (Timer == 10210)
                {
                    Lightning_Trail();
                }
                if (Timer == 10230)
                {
                    Lightning_Trail();
                }
                if (Timer == 10240)
                {
                    Timer = 121;
                    is_dashing = false;
                }
                if(Timer == 11000)
                {
                    Teleport_Up_Center_Lightning();
                }
                if (Timer == 11010)
                {
                    is_dashing = true;
                    Dash();
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                }
                if (Timer == 11079)
                {
                    Lightning_BOOOM();
                }
                if(Timer == 11080)
                {
                    is_dashing = false;
                    if (in_position == false)
                    {
                        check_position = true;
                    }
                    Move_Location = 2;
                    in_position = true;
                }
                if (Timer == 11081)
                {
                    Teleport_Up_Center_Lightning();
                    in_position = false;
                    check_position = false;
                }
                if (Timer == 11090)
                {
                    is_dashing = true;
                    Dash();
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                }
                if (Timer == 11159)
                {
                    Lightning_BOOOM();
                }
                if (Timer == 11160)
                {
                    is_dashing = false;
                    if (in_position == false)
                    {
                        check_position = true;
                    }
                    Move_Location = 2;
                    in_position = true;
                }
                if (Timer == 11161)
                {
                    Teleport_Up_Center_Lightning();
                    in_position = false;
                    check_position = false;
                }
                if (Timer == 11170)
                {
                    is_dashing = true;
                    Dash();
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                }
                if (Timer == 11239)
                {
                    Lightning_BOOOM();
                }
                if (Timer == 11240)
                {
                    is_dashing = false;
                    Move_Location = 0;
                    Timer = 601;
                }
                if (Timer == 12000)
                {
                    if (in_position == false)
                    {
                        check_position = true;
                    }
                    if (once == false)
                    {
                        Spawn_Area = Main.rand.Next(new int[] { 0, 1, 2, 3 });
                        once = true;
                    }
                    Move_Location = 4;
                    if (Spawn_Area == 0)
                    {
                        Move_X = 300;
                        Move_Y = 400;
                    }
                    if (Spawn_Area == 1)
                    {
                        Move_X = -300;
                        Move_Y = 400;
                    }
                    if(Spawn_Area == 2)
                    {
                        Move_X = 300;
                        Move_Y = -400;
                    }
                    if(Spawn_Area == 3)
                    {
                        Move_X = -300;
                        Move_Y = 400;
                    }
                    in_position = true;
                }
                if (Timer == 12001)
                {
                    is_dashing = true;
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                    should_track = true;
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                    in_position = false;
                    looking_at_player = true;
                    check_position = false;
                    once = false;
                }
                if (Timer == 12030)
                {
                    //Tracking();
                }
                if (Timer == 12060)
                {
                   // Tracking();
                }
                if (Timer == 12090)
                {
                    should_track = false;
                    looking_at_player = false;
                    Lightning_Slug();
                    Dash_Flash();
                }
                if (Timer == 12120)
                {
                    is_dashing = false;
                    if (in_position == false)
                    {
                        check_position = true;
                    }
                    if (once == false)
                    {
                        Spawn_Area = Main.rand.Next(new int[] { 0, 1, 2, 3 });
                        once = true;
                    }
                    Move_Location = 4;
                    if (Spawn_Area == 0)
                    {
                        Move_X = 300;
                        Move_Y = 400;
                    }
                    if (Spawn_Area == 1)
                    {
                        Move_X = -300;
                        Move_Y = 400;
                    }
                    if (Spawn_Area == 2)
                    {
                        Move_X = 300;
                        Move_Y = -400;
                    }
                    if (Spawn_Area == 3)
                    {
                        Move_X = -300;
                        Move_Y = 400;
                    }
                    in_position = true;
                }
                if (Timer == 12121)
                {
                    is_dashing = true;
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                    should_track = true;
                    looking_at_player = true;
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                    check_position = false;
                    in_position = false;
                    once = false;
                }
                if (Timer == 12150)
                {
                    //Tracking();
                }
                if (Timer == 12180)
                {
                    //Tracking();
                }
                if (Timer == 12220)
                {
                    looking_at_player = false;
                    should_track = false;
                    Lightning_Slug();
                    Dash_Flash();
                }
                if( Timer == 12250)
                {
                    is_dashing = false;
                    if (in_position == false)
                    {
                        check_position = true;
                    }
                    if (once == false)
                    {
                        Spawn_Area = Main.rand.Next(new int[] { 0, 1, 2, 3 });
                        once = true;
                    }
                    Move_Location = 4;
                    if (Spawn_Area == 0)
                    {
                        Move_X = 300;
                        Move_Y = 400;
                    }
                    if (Spawn_Area == 1)
                    {
                        Move_X = -300;
                        Move_Y = 400;
                    }
                    if (Spawn_Area == 2)
                    {
                        Move_X = 300;
                        Move_Y = -400;
                    }
                    if (Spawn_Area == 3)
                    {
                        Move_X = -300;
                        Move_Y = 400;
                    }
                    in_position = true;
                }
                if (Timer == 12251)
                {
                    is_dashing = true;
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                    should_track = true;
                    looking_at_player = true;
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                    check_position = false;
                    in_position = false;
                    once = false;
                }
                if (Timer == 12280)
                {
                    Tracking();
                }
                if (Timer == 12310)
                {
                    Tracking();
                }
                if (Timer == 12340)
                {
                    should_track = false;
                    looking_at_player = false;
                    Lightning_Slug();
                    Dash_Flash();
                }
                if (Timer == 12370)
                {
                    is_dashing = false;
                    Move_Location = 0;
                    Timer = 451;
                }
                if (Timer >= 17000)
                {
                    Teleport_Up_Center();
                    Is_Spinning = true;
                    NPC.rotation += 16f;
                }
                if (Timer == 17175)
                {
                    SoundEngine.PlaySound(SoundID.Item95, NPC.position);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(-10,10), ModContent.ProjectileType<Shark_Typhoon>(), 19, NPC.whoAmI);
                }
                if (Timer == 17300)
                {
                    SoundEngine.PlaySound(SoundID.Item95, NPC.position);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(10, 10), ModContent.ProjectileType<Shark_Typhoon>(), 19, NPC.whoAmI);
                }
                if (Timer == 17350)
                {
                    is_dashing = false;
                    Timer = 260;
                }
                #endregion
            }
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * 1.15f);
            NPC.lifeMax = (int)(NPC.lifeMax = 86650 * (int)balance);

            if (Main.masterMode == true)
            {
                NPC.lifeMax = (int)(NPC.lifeMax = 105000 * (int)balance);
                NPC.life = (int)(NPC.lifeMax = 105000 * (int)balance);
                NPC.damage = ((NPC.damage / 2) * 3);
            }
            if (Main.getGoodWorld == true)
            {
                NPC.scale = .8f;
                NPC.lifeMax = (int)(NPC.lifeMax = 136600 * (int)balance);
                NPC.life = (int)(NPC.lifeMax = 136600 * (int)balance);
                NPC.damage = ((NPC.damage / 10) * 13);
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        private void Teleport_Up_Center()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(0f, -400f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        private void Teleport_Up_Center_Lightning()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(0f, -700f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        private void Teleport_Up_Left()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(500f, -400f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        private void Teleport_Up_Right()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(-500f, -400f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        private void Teleport_Down_Left()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(500f, 700f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        private void Teleport_Down_Right()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(-500f, 700f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        private void Lightning_Slug()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Bottom;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 7f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 6f);
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Lightning_Bolt>(), 14, NPC.whoAmI);
                float rotation = MathHelper.ToRadians(12);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation, -rotation, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Lightning_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation2 = MathHelper.ToRadians(24);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation2, -rotation2, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Lightning_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation3 = MathHelper.ToRadians(36);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation3, -rotation3, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Lightning_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation4 = MathHelper.ToRadians(48);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation4, -rotation4, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Lightning_Bolt>(), 14, NPC.whoAmI);
                }
            }
        }
        private void Shower()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                {
                    Vector2 velocity = new(0f, -8f);
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 4.5f / magnitude;
                    }

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Ice_Bolt>(), 18, NPC.whoAmI);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Lightning_Tracking>(), 0, NPC.whoAmI);
                }
                {
                    Vector2 velocity = new(4f, 8f);
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 4.5f / magnitude;
                    }

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Ice_Bolt>(), 18, NPC.whoAmI);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Lightning_Tracking>(), 0, NPC.whoAmI);
                }
                {
                    Vector2 velocity = new(-4f, 8f);
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 4.5f / magnitude;
                    }

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Ice_Bolt>(), 18, NPC.whoAmI);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Lightning_Tracking>(), 0, NPC.whoAmI);
                }
                {
                    Vector2 velocity = new(-8f, 8f);
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 4.5f / magnitude;
                    }

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Ice_Bolt>(), 18, NPC.whoAmI);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Lightning_Tracking>(), 0, NPC.whoAmI);
                }
                {
                    Vector2 velocity = new(8f, 8f);
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 4.5f / magnitude;
                    }

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Ice_Bolt>(), 18, NPC.whoAmI);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Lightning_Tracking>(), 0, NPC.whoAmI);
                }
            }
        }
        private void Lightning_BOOOM()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0f, 0f), ModContent.ProjectileType<Lightning_Boom>(), 24, NPC.whoAmI);
            }
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Bottom;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 7f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 6f);
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Lightning_Bolt>(), 14, NPC.whoAmI);
                float rotation = MathHelper.ToRadians(12);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation, -rotation, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Lightning_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation2 = MathHelper.ToRadians(24);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation2, -rotation2, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Lightning_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation3 = MathHelper.ToRadians(36);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation3, -rotation3, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Lightning_Bolt>(), 14, NPC.whoAmI);
                }
                float rotation4 = MathHelper.ToRadians(48);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation4, -rotation4, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Lightning_Bolt>(), 14, NPC.whoAmI);
                }
            }
            SoundEngine.PlaySound(SoundID.NPCHit53, NPC.position);
            PunchCameraModifier modifier = new PunchCameraModifier(NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, FullName);
            Main.instance.CameraModifiers.Add(modifier);
        }
        private void Lightning_Side_Left()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                var proj = ModContent.ProjectileType<Lightning_Bolt>();
                var damage = 24;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 420f), new Vector2(Main.rand.Next(new[] { 3f, 5f, 7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 350f), new Vector2(Main.rand.Next(new[] { 3f, 5f, 7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 280f), new Vector2(Main.rand.Next(new[] {3f, 5f, 7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 140f), new Vector2(Main.rand.Next(new[] {3f, 5f, 7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 70f), new Vector2(Main.rand.Next(new[] {3f, 5f, 7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 0f), new Vector2(Main.rand.Next(new[] {3f, 5f, 7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, -140f), new Vector2(Main.rand.Next(new[] {3f, 5f, 7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, -210f), new Vector2(Main.rand.Next(new[] {3f, 5f, 7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, -350f), new Vector2(Main.rand.Next(new[] {3f, 5f, 7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, -420f), new Vector2(Main.rand.Next(new[] {3f, 5f, 7f }), 0f), proj, damage, NPC.whoAmI);
            }
            SoundEngine.PlaySound(SoundID.Item94, NPC.position);
        }
        private void Lightning_Side_Right()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                var proj = ModContent.ProjectileType<Lightning_Bolt>();
                var damage = 24;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 420f), new Vector2(Main.rand.Next(new[] { -3f, -5f, -7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 280f), new Vector2(Main.rand.Next(new[] { -3f, -5f, -7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 210f), new Vector2(Main.rand.Next(new[] { -3f, -5f, -7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 140f), new Vector2(Main.rand.Next(new[] { -3f, -5f, -7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 70f), new Vector2(Main.rand.Next(new[] { -3f, -5f, -7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 0f), new Vector2(Main.rand.Next(new[] { -3f, -5f, -7f }), 0f), proj,  damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, -140f), new Vector2(Main.rand.Next(new[] { -3f, -5f, -7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, -210f), new Vector2(Main.rand.Next(new[] { -3f, -5f, -7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, -280f), new Vector2(Main.rand.Next(new[] { -3f, -5f, -7f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, -420f), new Vector2(Main.rand.Next(new[] { -3f, -5f, -7f }), 0f), proj, damage, NPC.whoAmI);
            }
            SoundEngine.PlaySound(SoundID.Item94, NPC.position);
        }
        private void Move(Vector2 offset)
        {
            if (is_dashing == true)
            {
                return;
            }
            player = Main.player[NPC.target];
            float turnResistance = 36f;
            if (Move_Location != 0)
            {
                speed += 0.2f;
                turnResistance = 20f;
            }
            else
            {
                speed = 11.8f;
                turnResistance = 36f;
            }
            /*
            if (Timer >= 7000)
            {
                speed = 22f;
            }
            if (player.ZoneBeach == false)
            {
                speed = 22f;
            }
            else if (Main.expertMode == true)
            {
                speed = 11.8f;
            }
            */
            Vector2 moveTo = player.Center - offset;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
            if (magnitude <= 50f)
            {
                speed = 11.8f;
                check_position = false;
            }
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity = move;
        }
        private void Tracking()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = player.Center - NPC.Center;

                float rotation = (float)Math.Atan2(NPC.Center.Y - (player.position.Y + player.velocity.Y * 10), NPC.Center.X - (player.position.X + player.velocity.X * 10));
                velocity.X = (float)(Math.Cos(rotation) * 52) * -1;
                velocity.Y = (float)(Math.Sin(rotation) * 52) * -1;

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Lightning_Tracking>(), 0, NPC.whoAmI);
            }
        }
        private void Lightning_Trail()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                var entitySource = NPC.GetSource_FromAI();
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 6f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 5f);
                }

                Projectile.NewProjectile(entitySource, NPC.Center, velocity, ModContent.ProjectileType<Lightning_Ball>(), 20, NPC.whoAmI);
            }
        }
        private void Bubbles_Square()
        {
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 140 , (int)NPC.Center.Y - 140, NPCID.DetonatingBubble, NPC.whoAmI);
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 140, (int)NPC.Center.Y + 140, NPCID.DetonatingBubble, NPC.whoAmI);
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 140, (int)NPC.Center.Y - 140, NPCID.DetonatingBubble, NPC.whoAmI);
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 140, (int)NPC.Center.Y + 140, NPCID.DetonatingBubble, NPC.whoAmI);
        }
        private void Bubbles_Plus()
        {
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 0, (int)NPC.Center.Y - 140, NPCID.DetonatingBubble, NPC.whoAmI);
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 140, (int)NPC.Center.Y + 0, NPCID.DetonatingBubble, NPC.whoAmI);
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 140, (int)NPC.Center.Y - 0, NPCID.DetonatingBubble, NPC.whoAmI);
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 0, (int)NPC.Center.Y + 140, NPCID.DetonatingBubble, NPC.whoAmI);
        }
        public override void OnKill()
        {
            InfernusSystem.downedTigerShark = true;
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        private void Dash()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 1.8f;
                NPC.velocity.Y *= 1.8f;
                if (Main.expertMode == true)
                {
                    NPC.velocity.X *= 2.9f;
                    NPC.velocity.Y *= 2.9f;
                }
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - (player.position.Y + player.height), NPC.Center.X - (player.position.X + player.width));
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 18) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 18) * -1;
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
                var fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.WaterCandle, 0f, 0f, 100, default, 2f);
                fireDust.noGravity = true;
                fireDust.velocity *= 7f;
                fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.IcyMerman, 0f, 0f, 100, default, 1.5f);
                fireDust.velocity *= 3f;
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
            SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
        }
        private void Dash_Flash()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 6f;
                NPC.velocity.Y *= 6f;
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - (player.position.Y + player.velocity.Y * 10), NPC.Center.X - (player.position.X + player.velocity.X * 10));
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 52) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 52) * -1;
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
                var fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.WaterCandle, 0f, 0f, 100, default, 2f);
                fireDust.noGravity = true;
                fireDust.velocity *= 7f;
                fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.IcyMerman, 0f, 0f, 100, default, 1.5f);
                fireDust.velocity *= 3f;
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
            SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 36; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 4f, -2.5f, 0, default, 1f);
                }
                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Shark_Head").Type);
                }
                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Shark_Body").Type);
                }
                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Shark_Tail").Type);
                }
            }
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("Tiger Sharks are rare fish to see, but when you reel in the wrong one... well, a feral amalgamation dosen't like being pestered.")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            int chance = 13;

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.BossSummon.SharkBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Placeable.SharkTrophy>(), 10));

            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Placeable.SharkRelic>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Pets.RudeItem>()));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Magic.Lightning>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Melee.Electricice>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Summon.whiplight>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Ranged.Electricbow>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Ranged.Lightning_Daggers>(), 1, 175, 2580));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.JellyfishDivingGear, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.WaterWalkingBoots, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.SharkFin, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.FloatingTube, chance, 1, 1));

            npcLoot.Add(notExpertRule);
        }
    }
}