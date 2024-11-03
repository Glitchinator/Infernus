using Infernus.Config;
using Infernus.Items.BossSummon;
using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    [AutoloadBossHead]
    public class Calypsical : ModNPC
    {
        private Player player;
        private float speed;
        private int damage;
        public static readonly Color Calypsical_Voice = new(232, 200, 125);
        public static readonly Color Dialogue_Skip = new(118, 207, 89);
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;

            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Ichor] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BetsysCurse] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 413200;
            NPC.damage = 95;
            NPC.defense = 90;
            NPC.knockBackResist = 0.0f;
            NPC.width = 115;
            NPC.height = 170;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.NPCDeath59 with
            {
                Volume = 1.5f,
                Pitch = -0.55f,
                SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
            };
            NPC.value = Item.buyPrice(1, 50, 0, 0);
            NPC.boss = true;
            AIType = NPCID.AngryBones;
            Music = MusicLoader.GetMusicSlot("Infernus/Music/Divine_Fight");
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.npcSlots = 8;
        }

        int First_Phase_Timer;
        int Second_Phase_Timer;
        int Dead_Phase_Timer;
        public static bool death_aninatiom = false;
        bool second_phase = false;

        public override void AI()
        {
            NPC.netUpdate = true;
            damage = NPC.lifeMax - NPC.life;
            NPC.despawnEncouraged = false;
            NPC.rotation = NPC.velocity.X * 0.04f;

            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest(true);

            if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.velocity.Y = NPC.velocity.Y - .3f;
                if (NPC.timeLeft > 20)
                {
                    NPC.timeLeft = 20;
                    return;
                }
            }
            if (NPC.life <= (NPC.lifeMax / 2))
            {
                second_phase = true;
            }
            if (second_phase == true)
            {
                Music = MusicLoader.GetMusicSlot("Infernus/Music/Divine_Fight");
                First_Phase_Timer = 9000;
                if (First_Phase_Timer == 9000)
                {
                    Second_Phase_Timer++;
                }
                NPC.defense = 80 + (int)(damage * .000444f);
            }
            if (death_aninatiom == true)
            {
                First_Phase_Timer = 15000;
                Second_Phase_Timer = 15000;
                if (First_Phase_Timer == 15000)
                {
                    Dead_Phase_Timer++;
                }
                NPC.DoesntDespawnToInactivity();
                NPC.DiscourageDespawn(60);
            }

            {
                Move(new Vector2((Main.rand.Next(0)), -400f));
                First_Phase_Timer++;

                if (NPC.life <= 2000)
                {
                    death_aninatiom = true;
                    NPC.dontTakeDamage = true;
                    NPC.velocity.Y = 0;
                    NPC.velocity.X = 0;
                    NPC.damage = 0;
                }
                if(ModContent.GetInstance<InfernusConfig>().Skip_Dialogue == false)
                {
                    if (First_Phase_Timer <= 60)
                    {
                        // Spawn animation
                        First_Phase_Timer = 20000;
                    }
                }
                if (First_Phase_Timer == 72)
                {
                    Music = MusicLoader.GetMusicSlot("Infernus/Music/Divine_Fight");
                }
                if (First_Phase_Timer == 80)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 120)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 160)
                {
                    Heat_Seeking_Bombs();
                }
                if (First_Phase_Timer == 220)
                {
                    Homing_Rockets();
                }
                if (First_Phase_Timer == 300)
                {
                    Teleport_Down_Center();
                    Down_Sweep();
                }
                if (First_Phase_Timer == 380)
                {
                    Heat_Seeking_Bombs();
                }
                if (First_Phase_Timer == 340)
                {
                    // Upwards dashes + Bullet Hell begins
                    First_Phase_Timer = 12001;
                }
                if (First_Phase_Timer == 420)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 500)
                {
                    Heat_Seeking_Bombs();
                }
                if (First_Phase_Timer == 560)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 626)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Close_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Close_Right();
                    }
                    if (player.velocity.X == 0)
                    {
                        Teleport_Close_Left();
                    }
                }
                if (First_Phase_Timer == 630)
                {
                    // Dash begins
                    First_Phase_Timer = 10001;
                }
                if (First_Phase_Timer == 720)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 740)
                {
                    Homing_Rockets();
                }
                if (First_Phase_Timer == 750)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 800)
                {
                    // Teleport + Bullet Salvo begins
                    First_Phase_Timer = 13001;
                }
                if (First_Phase_Timer == 896)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Close_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Close_Right();
                    }
                    if (player.velocity.X == 0)
                    {
                        Teleport_Close_Left();
                    }
                }
                if (First_Phase_Timer == 900)
                {
                    // Dash 2 begins
                    First_Phase_Timer = 11001;
                }
                if (First_Phase_Timer == 960)
                {
                    Heat_Seeking_Bombs();
                }
                if (First_Phase_Timer == 1040)
                {
                    Heat_Seeking_Bombs();
                }
                if (First_Phase_Timer == 1180)
                {
                    // Above Player Sideways Dashes +  Bullet Hell Begins
                    First_Phase_Timer = 14001;
                }
                if (First_Phase_Timer == 1290)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 1350)
                {
                    Heat_Seeking_Bombs();
                }
                if (First_Phase_Timer == 1410)
                {
                    Homing_Rockets();
                }
                if (First_Phase_Timer == 1450)
                {
                    Teleport_Down_Center();
                    Down_Sweep();
                }
                if (First_Phase_Timer == 1550)
                {
                    First_Phase_Timer = 70;
                }
                if ((526 < First_Phase_Timer && First_Phase_Timer < 626) || (796 < First_Phase_Timer && First_Phase_Timer < 896) || (700 < First_Phase_Timer && First_Phase_Timer < 800) || (14141 < First_Phase_Timer && First_Phase_Timer < 14241) || (0 < Second_Phase_Timer && Second_Phase_Timer < 86) || (276 < Second_Phase_Timer && Second_Phase_Timer < 376) || (396 < Second_Phase_Timer && Second_Phase_Timer < 496) || (180 < Second_Phase_Timer && Second_Phase_Timer < 280) || (14141 < Second_Phase_Timer && Second_Phase_Timer < 14241))
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        if (player.velocity.X > 0)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                Vector2 position = player.Center + new Vector2(550f, 0f);
                                Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                                Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.SolarFlare, speed * 3, Scale: 2f);
                                Sword.noGravity = true;
                            }
                        }
                        if (player.velocity.X < 0)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                Vector2 position = player.Center + new Vector2(-550f, 0f);
                                Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                                Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.SolarFlare, speed * 3, Scale: 2f);
                                Sword.noGravity = true;
                            }
                        }
                        if (player.velocity.X == 0)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                Vector2 position = player.Center + new Vector2(550f, 0f);
                                Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                                Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.SolarFlare, speed * 3, Scale: 2f);
                                Sword.noGravity = true;
                            }
                        }
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        if (player.velocity.X > 0)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                Vector2 position = player.Center + new Vector2(550f, 0f);
                                Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                                Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.SolarFlare, speed * 3, Scale: 2f);
                                Sword.noGravity = true;
                            }
                        }
                        if (player.velocity.X < 0)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                Vector2 position = player.Center + new Vector2(-550f, 0f);
                                Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                                Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.SolarFlare, speed * 3, Scale: 2f);
                                Sword.noGravity = true;
                            }
                        }
                        if (player.velocity.X == 0)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                Vector2 position = player.Center + new Vector2(550f, 0f);
                                Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                                Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.SolarFlare, speed * 3, Scale: 2f);
                                Sword.noGravity = true;
                            }
                        }
                    }
                }

                #region Spawn_Animation

                if (First_Phase_Timer >= 20000)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                }
                if (First_Phase_Timer == 20000)
                {
                    Vector2 position = player.Center + new Vector2(0f, -220f);
                    NPC.Center = position;
                    NPC.dontTakeDamage = true;
                    Music = MusicID.OtherworldlySpace;
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("You can disable dialogue in the configs.", Dialogue_Skip);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("You can disable dialogue in the configs.", Dialogue_Skip);
                    }
                }
                if (First_Phase_Timer == 20050)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("A traveler? Hmm... Indeed. One with bloodsoaken hands.", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("A traveler? Hmm... Indeed. One with bloodsoaken hands.", Calypsical_Voice);
                    }
                }
                if (First_Phase_Timer == 20320)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("I have slept long enough, the kingdom of heaven is destroyed.", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("I have slept long enough, the kingdom of heaven is destroyed.", Calypsical_Voice);
                    }
                }
                if (First_Phase_Timer == 20700)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("For you, Child of Man. You have traded blood for gain.", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("For you, Child of Man. You have traded blood for gain.", Calypsical_Voice);
                    }
                }
                if (First_Phase_Timer == 20800)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("The blood of a thousand lives stains your hands.", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("The blood of a thousand lives stains your hands.", Calypsical_Voice);
                    }
                }
                if (First_Phase_Timer == 21300)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("And I can't allow you to continue without punishment.", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("And I can't allow you to continue without punishment.", Calypsical_Voice);
                    }
                }
                if (First_Phase_Timer == 21500)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("So. Come forth, weapon. And...", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("So. Come forth, weapon. And...", Calypsical_Voice);
                    }
                }
                if (First_Phase_Timer == 21650)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("DIE", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("DIE", Calypsical_Voice);
                    }

                    for (int k = 0; k < 40; k++)
                    {
                        Vector2 speed = Main.rand.NextVector2Circular(6.5f, 7f);
                        Dust Sword = Dust.NewDustPerfect(NPC.Center + speed * 32, DustID.SandstormInABottle, speed * 5, Scale: 3f);
                        Sword.noGravity = true;
                    }
                    for (int k = 0; k < 40; k++)
                    {
                        Vector2 speed = Main.rand.NextVector2Circular(7f, 6.5f);
                        Dust Sword = Dust.NewDustPerfect(NPC.Center + speed * 32, DustID.SandstormInABottle, speed * 5, Scale: 3f);
                        Sword.noGravity = true;
                    }
                }
                if (First_Phase_Timer == 21660)
                {
                    First_Phase_Timer = 70;
                    NPC.dontTakeDamage = false;
                }
                #endregion



                #region Stage_1_Boss_Unique_Attacks
                if (First_Phase_Timer == 10021)
                {
                    Dash();
                }
                if (First_Phase_Timer == 10061)
                {
                    Dash();
                }
                if (First_Phase_Timer == 10101)
                {
                    Dash();
                }
                if (First_Phase_Timer == 10141)
                {
                    Dash();
                }
                if (First_Phase_Timer == 10181)
                {
                    Dash();
                }
                if (First_Phase_Timer == 10221)
                {
                    //Dash ends 1
                    First_Phase_Timer = 660;
                }
                if (First_Phase_Timer == 11021)
                {
                    Dash();
                }
                if (First_Phase_Timer == 11061)
                {
                    Dash();
                }
                if (First_Phase_Timer == 11101)
                {
                    Dash();
                }
                if (First_Phase_Timer == 11141)
                {
                    Dash();
                }
                if (First_Phase_Timer == 11181)
                {
                    Dash();
                }
                if (First_Phase_Timer == 11221)
                {
                    //Dash ends 2
                    First_Phase_Timer = 920;
                }
                if (First_Phase_Timer == 12001)
                {
                    Teleport_Down_Left();
                }
                if (First_Phase_Timer == 12011)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12021)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12031)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12041)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12051)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12061)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12071)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12081)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12091)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12101)
                {
                    Teleport_Down_Right();
                }
                if (First_Phase_Timer == 12111)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12121)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12131)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12141)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12151)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12161)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12171)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12181)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12191)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12201)
                {
                    Teleport_Down_Center();
                    Dash();
                }
                if (First_Phase_Timer == 12211)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12221)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12231)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12241)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12251)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12261)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12271)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12281)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12291)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 12301)
                {
                    First_Phase_Timer = 360;
                }
                if (First_Phase_Timer == 13001)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Close_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Close_Right();
                    }
                    if (player.velocity.X == 0)
                    {
                        Teleport_Close_Left();
                    }
                }
                if (First_Phase_Timer == 13011)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 13021)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 13031)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 13041)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 13051)
                {
                    Shotgunblast();
                }
                if (First_Phase_Timer == 13051)
                {
                    First_Phase_Timer = 830;
                }
                if (First_Phase_Timer == 14001)
                {
                    Teleport_Up_Left();
                }
                if (First_Phase_Timer == 14011)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14021)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14031)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14041)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14051)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14061)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14071)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14081)
                {
                    Teleport_Up_Right();
                }
                if (First_Phase_Timer == 14091)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14101)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14111)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14121)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14131)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14141)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14151)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14161)
                {
                    Teleport_Up_Left();
                }
                if (First_Phase_Timer == 14171)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14181)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14191)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14201)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14211)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14221)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14231)
                {
                    Homing_Basic_Shot();
                }
                if (First_Phase_Timer == 14241)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Close_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Close_Right();
                    }
                    if (player.velocity.X == 0)
                    {
                        Teleport_Close_Left();
                    }
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                }
                if (First_Phase_Timer == 14251)
                {
                    Basic_Shot();
                }
                if (First_Phase_Timer == 14261)
                {
                    Basic_Shot();
                }
                if (First_Phase_Timer == 14271)
                {
                    Basic_Shot();
                }
                if (First_Phase_Timer == 14281)
                {
                    Basic_Shot();
                }
                if (First_Phase_Timer == 14291)
                {
                    Basic_Shot();
                }
                if (First_Phase_Timer == 14301)
                {
                    Basic_Shot();
                }
                if (First_Phase_Timer == 14311)
                {
                    Basic_Shot();
                }
                if (First_Phase_Timer == 14321)
                {
                    First_Phase_Timer = 1200;
                }
                #endregion


                //Second Phase
                if (Second_Phase_Timer == 1)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("Keep them coming!", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("Keep them coming!", Calypsical_Voice);
                    }
                }

                if (Second_Phase_Timer == 40)
                {
                    Dash_Strong();
                    Punch_Extra();
                }
                if (Second_Phase_Timer == 70)
                {
                    Heat_Seeking_Bombs();
                }
                if (Second_Phase_Timer == 86)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Close_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Close_Right();
                    }
                    if (player.velocity.X == 0)
                    {
                        Teleport_Close_Left();
                    }
                }
                if (Second_Phase_Timer == 90)
                {
                    Second_Phase_Timer = 15001;
                    // dash 1 begins
                }
                if (Second_Phase_Timer == 130)
                {
                    Dash_Strong();
                    Punch_Extra();
                }
                if (Second_Phase_Timer == 145)
                {
                    Shotgunblast();
                }
                if (Second_Phase_Timer == 170)
                {
                    Dash_Strong();
                    Punch_Extra();
                }
                if (Second_Phase_Timer == 200)
                {
                    Homing_Rockets();
                }
                if (Second_Phase_Timer == 230)
                {
                    // Upwards dashes + Bullet Hell begins
                    Second_Phase_Timer = 12001;
                }
                if (Second_Phase_Timer == 250)
                {
                    Homing_Rockets();
                }
                if (Second_Phase_Timer == 270)
                {
                    Heat_Seeking_Bombs();
                }
                if (Second_Phase_Timer == 280)
                {
                    //teleport
                    Second_Phase_Timer = 13001;
                }
                if (Second_Phase_Timer == 300)
                {
                    Shotgunblast();
                }
                if (Second_Phase_Timer == 320)
                {
                    Dash_Strong();
                    Punch_Extra();
                }
                if (Second_Phase_Timer == 360)
                {
                    Dash_Strong();
                    Punch_Extra();
                }
                if (Second_Phase_Timer == 376)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Close_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Close_Right();
                    }
                    if (player.velocity.X == 0)
                    {
                        Teleport_Close_Left();
                    }
                }
                if (Second_Phase_Timer == 380)
                {
                    // Dash 2 begins
                    Second_Phase_Timer = 16001;
                }
                if (Second_Phase_Timer == 400)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Down_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Down_Right();
                    }
                    else
                    {
                        Teleport_Down_Center();
                    }
                    Down_Sweep();
                }
                if (Second_Phase_Timer == 420)
                {
                    // spear dives
                    Second_Phase_Timer = 11001;
                }
                if (Second_Phase_Timer == 450)
                {
                    Heat_Seeking_Bombs();
                }
                if (Second_Phase_Timer == 480)
                {
                    Heat_Seeking_Bombs();
                }
                if (Second_Phase_Timer == 496)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Close_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Close_Right();
                    }
                    if (player.velocity.X == 0)
                    {
                        Teleport_Close_Left();
                    }
                }
                if (Second_Phase_Timer == 500)
                {
                    //Dash begins 3
                    Second_Phase_Timer = 10001;
                }
                if (Second_Phase_Timer == 530)
                {
                    Heat_Seeking_Bombs();
                }
                if (Second_Phase_Timer == 570)
                {
                    Shotgunblast();
                }
                if (Second_Phase_Timer == 600)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Down_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Down_Right();
                    }
                    else
                    {
                        Teleport_Down_Center();
                    }
                    Down_Sweep();
                }
                if (Second_Phase_Timer == 640)
                {
                    // Sideways dashes + Bullet Hell
                    Second_Phase_Timer = 14001;
                }
                if (Second_Phase_Timer == 700)
                {
                    Shotgunblast();
                }
                if (Second_Phase_Timer == 750)
                {
                    Dash_Strong();
                    Punch_Extra();
                }
                if (Second_Phase_Timer == 790)
                {
                    Dash_Strong();
                    Punch_Extra();
                }
                if (Second_Phase_Timer == 840)
                {
                    Homing_Rockets();
                }
                if (Second_Phase_Timer == 900)
                {
                    Second_Phase_Timer = 20;
                }

                #region Stage_2_Boss_Unique_Attacks
                if (Second_Phase_Timer == 10021)
                {
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 10081)
                {
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 10141)
                {
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 10171)
                {
                    //Dash ends 3
                    Second_Phase_Timer = 510;
                }
                if (Second_Phase_Timer == 15021)
                {
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 15081)
                {
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 15141)
                {
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 15171)
                {
                    //Dash ends 1
                    Second_Phase_Timer = 110;
                }
                if (Second_Phase_Timer == 16021)
                {
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 16081)
                {
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 16141)
                {
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 16171)
                {
                    //Dash ends 2
                    Second_Phase_Timer = 390;
                }
                if (Second_Phase_Timer == 11001)
                {
                    Teleport_Up_Center();
                    NPC.velocity.Y = 0;
                    NPC.velocity.X = 0;
                }
                if (Second_Phase_Timer == 11012)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 11022)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11032)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11042)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11052)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11062)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11072)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11082)
                {
                    Teleport_Up_Center();
                    NPC.velocity.Y = 0;
                    NPC.velocity.X = 0;
                }
                if (Second_Phase_Timer == 11093)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 11103)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11113)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11123)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11133)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11143)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11153)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11163)
                {
                    Teleport_Up_Center();
                    NPC.velocity.Y = 0;
                    NPC.velocity.X = 0;
                }
                if (Second_Phase_Timer == 11174)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash_Strong();
                }
                if (Second_Phase_Timer == 11184)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11194)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11204)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11214)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11224)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11234)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 11244)
                {
                    Second_Phase_Timer = 430;
                }

                if (Second_Phase_Timer == 13001)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Close_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Close_Right();
                    }
                    if (player.velocity.X == 0)
                    {
                        Teleport_Close_Left();
                    }
                }
                if (Second_Phase_Timer == 13011)
                {
                    Shotgunblast();
                }
                if (Second_Phase_Timer == 13021)
                {
                    Shotgunblast();
                }
                if (Second_Phase_Timer == 13031)
                {
                    Shotgunblast();
                }
                if (Second_Phase_Timer == 13041)
                {
                    Shotgunblast();
                }
                if (Second_Phase_Timer == 13051)
                {
                    Shotgunblast();
                }
                if (Second_Phase_Timer == 13051)
                {
                    Second_Phase_Timer = 290;
                }




                if (Second_Phase_Timer == 12001)
                {
                    Teleport_Down_Left();
                }
                if (Second_Phase_Timer == 12011)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12021)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12031)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12041)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12051)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12061)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12071)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12081)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12091)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12101)
                {
                    Teleport_Down_Right();
                }
                if (Second_Phase_Timer == 12111)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12121)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12131)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12141)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12151)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12161)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12171)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12181)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12191)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12201)
                {
                    Teleport_Down_Center();
                    Dash();
                }
                if (Second_Phase_Timer == 12211)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12221)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12231)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12241)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12251)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12261)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12271)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12281)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12291)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 12321)
                {
                    Second_Phase_Timer = 240;
                }





                if (Second_Phase_Timer == 14001)
                {
                    Teleport_Up_Left();
                }
                if (Second_Phase_Timer == 14011)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14021)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14031)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14041)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14051)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14061)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14071)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14081)
                {
                    Teleport_Up_Right();
                }
                if (Second_Phase_Timer == 14091)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14101)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14111)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14121)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14131)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14141)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14151)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14161)
                {
                    Teleport_Up_Left();
                }
                if (Second_Phase_Timer == 14171)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14181)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14191)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14201)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14211)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14221)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14231)
                {
                    Homing_Basic_Shot();
                }
                if (Second_Phase_Timer == 14241)
                {
                    if (player.velocity.X > 0)
                    {
                        Teleport_Close_Left();
                    }
                    if (player.velocity.X < 0)
                    {
                        Teleport_Close_Right();
                    }
                    if (player.velocity.X == 0)
                    {
                        Teleport_Close_Left();
                    }
                }
                if (Second_Phase_Timer == 14243)
                {
                    Dash();
                }
                if (Second_Phase_Timer == 14251)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 14261)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 14271)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 14281)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 14291)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 14301)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 14311)
                {
                    Basic_Shot();
                }
                if (Second_Phase_Timer == 14321)
                {
                    Second_Phase_Timer = 650;
                }
                #endregion

                #region Death_Animation
                if (Dead_Phase_Timer >= 1)
                {
                    // death animation
                    Music = MusicLoader.GetMusicSlot("Infernus/Music/Divinity");

                    if (Main.rand.NextBool(12))
                    {
                        Rectangle rectangle = new((int)NPC.position.X, (int)(NPC.position.Y + ((NPC.height - NPC.width) / 2)), NPC.width, NPC.width);
                        for (int i = 1; i <= 9; i++)
                        {
                            int dust = Dust.NewDust(NPC.position, rectangle.Width, rectangle.Height, DustID.Smoke, 0, 0, 100, Color.White, 3f);
                            Main.dust[dust].noGravity = true;
                        }
                    }
                    if (Main.rand.NextBool(15))
                    {
                        Rectangle rectangle = new((int)NPC.position.X, (int)(NPC.position.Y + ((NPC.height - NPC.width) / 2)), NPC.width, NPC.width);
                        for (int i = 1; i <= 9; i++)
                        {
                            int dust = Dust.NewDust(NPC.position, rectangle.Width, rectangle.Height, DustID.SolarFlare, 0, 0, 100, Color.White, 3f);
                            Main.dust[dust].noGravity = true;
                        }
                        SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
                    }
                }
                if (Dead_Phase_Timer >= 200)
                {
                    if (Main.rand.NextBool(12))
                    {
                        Death_Sparks();
                    }
                }
                if (Dead_Phase_Timer == 600)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("And so concludes the life of King Calypsical.", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("And so concludes the life of King Calypsical.", Calypsical_Voice);
                    }
                }
                if (Dead_Phase_Timer >= 700)
                {
                    if (Main.rand.NextBool(12))
                    {
                        Death_Rings();
                    }
                }
                if (Dead_Phase_Timer == 800)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("I have failed you, my children. To bring peace, and order back to this realm.", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("I have failed you, my children. To bring peace, and order back to this realm.", Calypsical_Voice);
                    }
                }
                if (Dead_Phase_Timer == 1000)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("But for those like you who kill. Judgement is on the other side. And they will not be so merciful.", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("But for those like you who kill. Judgement is on the other side. And they will not be so merciful.", Calypsical_Voice);
                    }
                }
                if (Dead_Phase_Timer >= 1100)
                {
                    if (Main.rand.NextBool(12))
                    {
                        Death_Rings_Big();
                    }
                }
                if (Dead_Phase_Timer == 1200)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("While in death, I look back on my legacy. And I regret nothing I have done for this world.", Calypsical_Voice);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("While in death, I look back on my legacy. And I regret nothing I have done for this world.", Calypsical_Voice);
                    }
                }
                if (Dead_Phase_Timer == 1300)
                {
                    SoundEngine.PlaySound(SoundID.Item162 with
                    {
                        Volume = 1f,
                        Pitch = -0.95f,
                        SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                    });
                }
                if (Dead_Phase_Timer == 1398)
                {
                    NPC.life = 2000;
                }
                if (Dead_Phase_Timer >= 1399)
                {
                    NPC.dontTakeDamage = false;
                }
                if (Dead_Phase_Timer == 1400)
                {
                    SoundEngine.PlaySound(SoundID.Item163 with
                    {
                        Volume = 1.2f,
                        Pitch = 0.55f,
                        SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                    });
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Death_Ring_White>(), 0, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<A_Death>(), 9999, 0, Main.myPlayer, 0f, 0f);
                }
                #endregion
            }
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * 1.08f);
            NPC.lifeMax = (int)(NPC.lifeMax = 523200 * (int)balance);

            if (Main.masterMode == true)
            {
                NPC.lifeMax = (NPC.lifeMax = 573800 * (int)balance);
                NPC.life = (NPC.lifeMax = 573800 * (int)balance);
                NPC.damage = ((NPC.damage / 2) * 3);
            }
            if (Main.getGoodWorld == true)
            {
                NPC.scale = .8f;
                NPC.lifeMax = (NPC.lifeMax = 623800 * (int)balance);
                NPC.life = (NPC.lifeMax = 623800 * (int)balance);
                NPC.damage = ((NPC.damage / 10) * 13);
            }
        }
        public override bool CheckDead()
        {
            NPC.life = 2000;
            if(death_aninatiom == true)
            {
                return true;
            }
            return false;
        }
        private void Move(Vector2 offset)
        {
            if (Second_Phase_Timer >= 10000)
            {
                return;
            }
            else if (First_Phase_Timer >= 10000)
            {
                return;
            }
            player = Main.player[NPC.target];
            speed = 14f;
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 40f;
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
            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.InfernoFork, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f);

            SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);

            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 3.2f;
                NPC.velocity.Y *= 3.2f;
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - (player.position.Y + player.height), NPC.Center.X - (player.position.X + player.width));
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 17) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 17) * -1;
                }
            }
        }
        private void Dash_Strong()
        {
            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.InfernoFork, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f);
            }
            SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);

            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 3.3f;
                NPC.velocity.Y *= 3.3f;
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - (player.position.Y + player.height), NPC.Center.X - (player.position.X + player.width));
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 18) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 18) * -1;
                }
            }
        }
        private void Death_Rings()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 0f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 0f);
                }

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Death_Ring>(), 0, NPC.whoAmI);
            }
        }
        private void Death_Rings_Big()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 0f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 0f);
                }

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Death_Ring_Big>(), 0, NPC.whoAmI);
            }
        }
        private void Death_Sparks()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 4f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 5f);
                }
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(360));
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, newVelocity, ModContent.ProjectileType<Death_Sparks>(), 0, NPC.whoAmI);
            }
        }
        private void Teleport_Up_Center()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(0f, -500f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Death_Ring_Big>(), 0, 0, Main.myPlayer, 0f, 0f);
                SoundEngine.PlaySound(SoundID.Item94 with
                {
                    Volume = 1f,
                    Pitch = -0.65f,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
            }
        }
        private void Teleport_Up_Left()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(500f, -400f);
                NPC.Center = position;
                NPC.velocity.X = -20;
                NPC.velocity.Y = 0;
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Death_Ring_Big>(), 0, 0, Main.myPlayer, 0f, 0f);
                SoundEngine.PlaySound(SoundID.Item94 with
                {
                    Volume = 1f,
                    Pitch = -0.65f,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
            }
        }
        private void Teleport_Up_Right()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(-500f, -400f);
                NPC.Center = position;
                NPC.velocity.X = 20;
                NPC.velocity.Y = 0;
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Death_Ring_Big>(), 0, 0, Main.myPlayer, 0f, 0f);
                SoundEngine.PlaySound(SoundID.Item94 with
                {
                    Volume = 1f,
                    Pitch = -0.65f,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
            }
        }
        private void Teleport_Down_Center()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(0f, 450f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Death_Ring_Big>(), 0, 0, Main.myPlayer, 0f, 0f);
                SoundEngine.PlaySound(SoundID.Item94 with
                {
                    Volume = 1f,
                    Pitch = -0.65f,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
            }
        }
        private void Teleport_Down_Left()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(500f, 700f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = -20;
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Death_Ring_Big>(), 0, 0, Main.myPlayer, 0f, 0f);
                SoundEngine.PlaySound(SoundID.Item94 with
                {
                    Volume = 1f,
                    Pitch = -0.65f,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
            }
        }
        private void Teleport_Down_Right()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(-500f, 700f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = -20;
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Death_Ring_Big>(), 0, 0, Main.myPlayer, 0f, 0f);
                SoundEngine.PlaySound(SoundID.Item94 with
                {
                    Volume = 1f,
                    Pitch = -0.65f,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
            }
        }
        private void Teleport_Close_Left()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(550f, 0f);
                NPC.Center = position;
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Death_Ring_Big>(), 0, 0, Main.myPlayer, 0f, 0f);
                SoundEngine.PlaySound(SoundID.Item94 with
                {
                    Volume = 1f,
                    Pitch = -0.65f,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
            }
        }
        private void Teleport_Close_Right()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(-550f, 0f);
                NPC.Center = position;
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Death_Ring_Big>(), 0, 0, Main.myPlayer, 0f, 0f);
                SoundEngine.PlaySound(SoundID.Item94 with
                {
                    Volume = 1f,
                    Pitch = -0.65f,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
            }
        }
        private void Homing_Rockets()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 3; i++)
                {
                    player = Main.player[NPC.target];
                    Vector2 velocity = player.Center - NPC.Bottom;
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 13f / magnitude;
                    }
                    else
                    {
                        velocity = new Vector2(0f, 7f);
                    }
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(95));

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, newVelocity, ModContent.ProjectileType<Holy_Rockets>(), 35, NPC.whoAmI);
                }
            }
        }
        private void Down_Sweep()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(100f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(200f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(300f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(400f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(500f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(600f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(700f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-100f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-200f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-300f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-400f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-500f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-600f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-700f, 800f), new Vector2(0f, Main.rand.Next(new[] { -5f, -7f, -10f })), ModContent.ProjectileType<Holy_Shot>(), 33, NPC.whoAmI);
            }
        }
        private void Heat_Seeking_Bombs()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 12f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 12f);
                }

                float rotation = MathHelper.ToRadians(55);

                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<Holy_Homing_Bomb>(), 29, NPC.whoAmI);
                }
            }

        }
        private void Shotgunblast()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 velocity = player.Center - NPC.Center;
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 9f / magnitude;
                    }
                    else
                    {
                        velocity = new Vector2(0f, 5f);
                    }
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(35));
                    newVelocity *= 1f - Main.rand.NextFloat(0.5f);

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, newVelocity, ModContent.ProjectileType<Holy_Bullet>(), 32, NPC.whoAmI);
                }
            }
        }
        private void Punch_Extra()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 9f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 5f);
                }

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Holy_Fist>(), 41, NPC.whoAmI);
            }
        }
        private void Basic_Shot()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 10.6f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 5f);
                }

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Holy_Shot>(), 31, NPC.whoAmI);
            }
        }
        private void Homing_Basic_Shot()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
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

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Holy_Energy>(), 29, NPC.whoAmI);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (second_phase == true)
            {
                Texture2D texture = TextureAssets.Npc[NPC.type].Value;

                Rectangle frame;


                frame = texture.Frame();

                Vector2 frameOrigin = frame.Size() / 2f;
                Vector2 offset = new(NPC.width / 2 - frameOrigin.X, NPC.height - frame.Height);
                Vector2 drawPos = NPC.position - Main.screenPosition + frameOrigin + offset;

                float time = Main.GlobalTimeWrappedHourly;
                float timer = NPC.activeTime / 240f + time * 0.04f;

                time %= 4f;
                time /= 2f;

                if (time >= 1f)
                {
                    time = 2f - time;
                }

                time = time * 0.5f + 0.5f;

                for (float i = 0f; i < 1f; i += 0.25f)
                {
                    float radians = (i + timer) * MathHelper.TwoPi;

                    spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(232, 200, 125, 50), NPC.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
                }

                for (float i = 0f; i < 1f; i += 0.34f)
                {
                    float radians = (i + timer) * MathHelper.TwoPi;

                    spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(232, 200, 125, 77), NPC.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
                }
            }
            return true;
        }
        private static float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText("You hear the Infernheads rejoice", InfernusPlayer.GainXP_Resource);
                }
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    Main.NewText("You hear the Infernheads rejoice", InfernusPlayer.GainXP_Resource);
                }
            }
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The Angelic Guardian " + name;
            potionType = ItemID.SuperHealingPotion;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("A king of heaven. With his kingdom torn down he has the rage of gods. But only uses his power for order.")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Mechbag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Placeable.MechTrophy>(), 10));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Tools.MechPole>(), 10));

            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Placeable.MechRelic>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Pets.MechItem>()));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Magic.Cyclone>(), 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Melee.HolyRam>(), 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Ranged.miniholy>(), 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Summon.Mecharmr>(), 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Summon.MechWhip>(), 1));

            npcLoot.Add(notExpertRule);
        }
        public override void OnKill()
        {
            death_aninatiom = false;
            InfernusSystem.downedCalypsical = true;
        }
    }
}