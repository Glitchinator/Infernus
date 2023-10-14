using IL.Terraria.GameContent.Biomes;
using Infernus.Projectiles;
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
    public class Shark : ModNPC
    {
        private Player player;
        private float speed;
        private bool Is_Spinning = false;
        int Spawn_Area;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Serphious");
            Main.npcFrameCount[NPC.type] = 2;

            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[] {

                    BuffID.Electrified,
                    BuffID.Frostburn2,
                    BuffID.Frostburn,


                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 80650;
            NPC.damage = 70;
            NPC.defense = 38;
            NPC.knockBackResist = 0.0f;
            NPC.width = 138;
            NPC.height = 62;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AnimationType = NPCID.DemonEye;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 36);
            NPC.boss = true;
            Music = MusicID.Boss2;
        }
        int Timer;
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            if(Is_Spinning == false)
            {
                NPC.rotation = NPC.velocity.ToRotation();
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
            {
                Move(new Vector2((Main.rand.Next(0)), 0f));
                Timer++;
                if (Timer == 20)
                {
                    Timer = 10000;
                }
                if (Timer == 60)
                {
                    if (player.velocity.X <= 0)
                    {
                        Lightning_Side_Left();
                    }
                    else
                    {
                        Lightning_Side_Right();
                    }
                }
                if (Timer == 120)
                {
                    if (player.velocity.X <= 0)
                    {
                        Lightning_Side_Left();
                    }
                    else
                    {
                        Lightning_Side_Right();
                    }
                }
                if (Timer == 180)
                {
                    if (player.velocity.X <= 0)
                    {
                        Lightning_Side_Left();
                    }
                    else
                    {
                        Lightning_Side_Right();
                    }
                }
                if (Timer == 200)
                {
                    Lightning_Orb();
                }
                if (Timer == 240)
                {
                    Timer = 17000;
                }
                if(Timer == 261)
                {
                    Is_Spinning = false;
                }
                if(Timer == 320)
                {
                    Lightning_Orb();
                }
                if(Timer == 400)
                {
                    Timer = 12000;
                }
                if(Timer == 550)
                {
                    Timer = 11000;
                }
                if(Timer == 630)
                {
                    if (player.velocity.X <= 0)
                    {
                        Lightning_Side_Left();
                    }
                    else
                    {
                        Lightning_Side_Right();
                    }
                }
                if (Timer == 660)
                {
                    Timer = 0;
                }
                #region First_Phase_Attacks;
                if (Timer == 10000)
                {
                    Dash();
                }
                if (Timer == 10010)
                {
                    Lightning_Trail();
                }
                if (Timer == 10020)
                {
                    Lightning_Trail();
                }
                if (Timer == 10030)
                {
                    Lightning_Trail();
                }
                if (Timer == 10040)
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
                if (Timer == 10080)
                {
                    Lightning_Trail();
                }
                if (Timer == 10090)
                {
                    Lightning_Trail();
                }
                if (Timer == 10100)
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
                if (Timer == 10140)
                {
                    Lightning_Trail();
                }
                if (Timer == 10150)
                {
                    Lightning_Trail();
                }
                if (Timer == 10160)
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
                if (Timer == 10200)
                {
                    Lightning_Trail();
                }
                if (Timer == 10210)
                {
                    Lightning_Trail();
                }
                if (Timer == 10220)
                {
                    Lightning_Trail();
                }
                if (Timer == 10230)
                {
                    Lightning_Trail();
                }
                if (Timer == 10240)
                {
                    Timer = 40;
                }
                if(Timer == 11000)
                {
                    Teleport_Up_Center_Lightning();
                }
                if (Timer == 11010)
                {
                    Dash();
                    Lightning_Up();
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                }
                if (Timer == 11080)
                {
                    Teleport_Up_Center_Lightning();
                }
                if (Timer == 11090)
                {
                    Dash();
                    Lightning_Up();
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                }
                if (Timer == 11160)
                {
                    Teleport_Up_Center_Lightning();
                }
                if (Timer == 11170)
                {
                    Dash();
                    Lightning_Up();
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                }
                if(Timer == 11240)
                {
                    Timer = 600;
                }
                if(Timer >= 12000)
                {
                    if (Main.rand.NextBool(9))
                    {
                        Lightning_Trail();
                    }
                }
                if (Timer == 12000)
                {
                    Spawn_Area = Main.rand.Next(new int[] { 0, 1, 2, 3 });
                    if (Spawn_Area == 0)
                    {
                        Teleport_Down_Left();
                    }
                    if (Spawn_Area == 1)
                    {
                        Teleport_Up_Right();
                    }
                    if (Spawn_Area == 2)
                    {
                        Teleport_Down_Right();
                    }
                    if (Spawn_Area == 3)
                    {
                        Teleport_Up_Left();
                    }
                    Tracking();
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                }
                if (Timer == 12030)
                {
                    Tracking();
                }
                if (Timer == 12060)
                {
                    Tracking();
                }
                if (Timer == 12090)
                {
                    Lightning_Slug();
                    Dash_Flash();
                }
                if (Timer == 12120)
                {
                    Spawn_Area = Main.rand.Next(new int[] { 0, 1, 2, 3 });
                    if (Spawn_Area == 0)
                    {
                        Teleport_Down_Left();
                    }
                    if (Spawn_Area == 1)
                    {
                        Teleport_Up_Right();
                    }
                    if (Spawn_Area == 2)
                    {
                        Teleport_Down_Right();
                    }
                    if (Spawn_Area == 3)
                    {
                        Teleport_Up_Left();
                    }
                    Tracking();
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
                }
                if (Timer == 12150)
                {
                    Tracking();
                }
                if (Timer == 12180)
                {
                    Tracking();
                }
                if (Timer == 12220)
                {
                    Lightning_Slug();
                    Dash_Flash();
                }
                if (Timer == 12250)
                {
                    Spawn_Area = Main.rand.Next(new int[] { 0, 1, 2, 3 });
                    if (Spawn_Area == 0)
                    {
                        Teleport_Down_Left();
                    }
                    if (Spawn_Area == 1)
                    {
                        Teleport_Up_Right();
                    }
                    if (Spawn_Area == 2)
                    {
                        Teleport_Down_Right();
                    }
                    if (Spawn_Area == 3)
                    {
                        Teleport_Up_Left();
                    }
                    Tracking();
                    if (InfernusSystem.Level_systemON == true)
                    {
                        Lightning_Slug();
                    }
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
                    Lightning_Slug();
                    Dash_Flash();
                }
                if (Timer == 12370)
                {
                    Timer = 450;
                }
                if (Timer >= 17000)
                {
                    Teleport_Up_Center();
                    Is_Spinning = true;
                    NPC.rotation += 16f;
                }
                if (Timer == 17050)
                {
                    Bubbles_Plus();
                }
                if (Timer == 17100)
                {
                    Bubbles_Square();
                }
                if (Timer == 17150)
                {
                    Bubbles_Plus();
                }
                if (Timer == 17200)
                {
                    Bubbles_Square();
                }
                if (Timer == 17250)
                {
                    Bubbles_Plus();
                }
                if (Timer == 17300)
                {
                    Bubbles_Square();
                }
                if (Timer == 17350)
                {
                    Bubbles_Plus();
                }
                if (Timer == 17350)
                {
                    Bubbles_Square();
                }
                if (Timer == 17450)
                {
                    Timer = 260;
                }
                #endregion
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.damage = (int)(NPC.damage * 1.15f);
            NPC.lifeMax = (int)(NPC.lifeMax = 110360 + numPlayers);
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
                for (int i = 0; i < 7; i++)
                {
                    Vector2 velocity = player.Center - NPC.Center;
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 10f / magnitude;
                    }
                    else
                    {
                        velocity = new Vector2(0f, 7f);
                    }
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(54));
                    newVelocity *= 1f - Main.rand.NextFloat(0.4f);

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, newVelocity, ModContent.ProjectileType<Lightning_Bolt>(), 24, NPC.whoAmI);
                }
            }
        }
        private void Lightning_Up()
        {
            var proj = ModContent.ProjectileType<Lightning_Bolt>();
            var damage = 23;
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(0f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(50f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(100f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(150f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(200f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(250f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(300f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(350f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(400f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(450f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(500f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(550f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(600f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(650f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(700f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-50f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-100f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-150f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-200f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-250f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-300f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-350f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-400f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-450f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-500f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-550f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-600f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-650f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-700f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), proj, damage, NPC.whoAmI);
            }
        }
        private void Lightning_Side_Left()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                var proj = ModContent.ProjectileType<Lightning_Bolt>();
                var damage = 24;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 420f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 350f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 280f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 210f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 140f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 70f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, 0f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, -70f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, -140f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, -210f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, -280f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, -350f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) - 1200f, -420f), new Vector2(Main.rand.Next(new[] { 5f, 7f, 10f }), 0f), proj, damage, NPC.whoAmI);
            }
        }
        private void Lightning_Side_Right()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                var proj = ModContent.ProjectileType<Lightning_Bolt>();
                var damage = 24;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 420f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 350f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 280f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 210f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 140f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 70f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, 0f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj,  damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, -70f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, -140f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, -210f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, -280f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, -350f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.rand.Next(new[] { 75f, 150f, 200f }) + 1200f, -420f), new Vector2(Main.rand.Next(new[] { -5f, -7f, -10f }), 0f), proj, damage, NPC.whoAmI);
            }
        }
        private void Move(Vector2 offset)
        {
            if (Timer >= 10000)
            {
                return;
            }
            player = Main.player[NPC.target];
            speed = 10.6f;
            if(Main.expertMode == true)
            {
                speed = 11.8f;
            }
            Vector2 moveTo = player.Center - offset;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
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
        private void Lightning_Orb()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                var entitySource = NPC.GetSource_FromAI();
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 8f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 5f);
                }

                Projectile.NewProjectile(entitySource, NPC.Center, velocity, ProjectileID.CultistBossLightningOrb, 19, NPC.whoAmI);
            }
        }
        private void Tracking()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                float spawnarea = player.velocity.X * player.velocity.X;
                if (magnitude > 0)
                {
                    velocity *= 8f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 5f);
                }

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
                    velocity *= 8f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 5f);
                }

                Projectile.NewProjectile(entitySource, NPC.Center, velocity, ModContent.ProjectileType<Lightning_Ball>(), 21, NPC.whoAmI);
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
                NPC.velocity.X *= 2.9f;
                NPC.velocity.Y *= 2.9f;
                if (Main.expertMode == true)
                {
                    NPC.velocity.X *= 3.6f;
                    NPC.velocity.Y *= 3.6f;
                }
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - (player.position.Y + player.height), NPC.Center.X - (player.position.X + player.width));
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 18) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 18) * -1;
                }
            }
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
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 36; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 4f * hitDirection, -2.5f, 0, default, 1f);
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

            npcLoot.Add(notExpertRule);
        }
    }
}