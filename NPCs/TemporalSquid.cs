using Infernus.Items.BossSummon;
using Infernus.Items.Weapon.Melee;
using Infernus.Projectiles;
using Infernus.Projectiles.Temporal_Glow_Squid.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class TemporalSquid : ModNPC
    {
        private Player player;
        private float speed;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;

            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 2450;
            NPC.damage = 18;
            NPC.defense = 15;
            NPC.knockBackResist = 0.0f;
            NPC.width = 54;
            NPC.height = 180;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit8;
            NPC.DeathSound = SoundID.Roar;
            NPC.value = 30000;
            NPC.boss = true;
            AIType = NPCID.AngryBones;
            Music = MusicLoader.GetMusicSlot("Infernus/Music/Inked_Vortex");
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.npcSlots = 6;
            NPC.alpha = 255;
        }
        int Timer;
        int Timer_Tornado;
        int Timer_SecondPhase;
        int frameSpeed = 16;
        bool secondphase = false;
        bool spawn_anim = false;

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            NPC.rotation = NPC.velocity.X * 0.01f;
            NPC.netUpdate = true;

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
            if (NPC.alpha <= 0)
            {
                spawn_anim = true;
                NPC.alpha = 0;
            }
            if (spawn_anim == false)
            {
                NPC.alpha -= 7;
                return;
            }
            if (Main.expertMode == true || player.ZoneBeach == false)
            {
                Move(new Vector2((Main.rand.Next(0)), 0f));
            }
            else
            {
                Move(new Vector2((Main.rand.Next(0)), -230f));
            }
            if (NPC.life <= (NPC.lifeMax / 2))
            {
                secondphase = true;
            }
            Timer++;
            Timer_Tornado++;
            if (secondphase == true)
            {
                SoundEngine.PlaySound(SoundID.Zombie93, NPC.position);
                frameSpeed = 4;
                Timer = 10000;
                if (Timer == 10000)
                {
                    Timer_SecondPhase++;
                }
            }
            #region Tornado_Summon_Timer
            {
                if (InfernusSystem.Level_systemON == true)
                {
                    // every 5 sec
                    if (Timer_Tornado >= 300)
                    {
                        Pre_Typhoon();
                    }
                    if (Timer_Tornado == 300)
                    {
                        SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                    }
                    if (Timer_Tornado == 360)
                    {
                        SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                    }
                    if (Timer_Tornado == 420)
                    {
                        SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                    }
                    if (Timer_Tornado == 480)
                    {
                        SoundEngine.PlaySound(SoundID.Zombie103, NPC.position);
                        Typhoon();
                        Timer_Tornado = 0;
                    }
                }
                else if (Main.expertMode == true)
                {
                    // every 6 sec
                    if (Timer_Tornado >= 360)
                    {
                        Pre_Typhoon();
                    }
                    if (Timer_Tornado == 360)
                    {
                        SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                    }
                    if (Timer_Tornado == 420)
                    {
                        SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                    }
                    if (Timer_Tornado == 480)
                    {
                        SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                    }
                    if (Timer_Tornado == 540)
                    {
                        SoundEngine.PlaySound(SoundID.Zombie103, NPC.position);
                        Typhoon();
                        Timer_Tornado = 0;
                    }
                }
                else
                {
                    // every 7 sec
                    if (Timer_Tornado >= 420)
                    {
                        Pre_Typhoon();
                    }
                    if (Timer_Tornado == 480)
                    {
                        SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                    }
                    if (Timer_Tornado == 540)
                    {
                        SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                    }
                    if (Timer_Tornado == 600)
                    {
                        SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                    }
                    if (Timer_Tornado == 660)
                    {
                        SoundEngine.PlaySound(SoundID.Zombie103, NPC.position);
                        Typhoon();
                        Timer_Tornado = 0;
                    }
                }
            }
            #endregion
            #region First_Phase
            if (Timer == 60)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if (Timer == 120)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer == 180)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if (Timer == 240)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer == 300)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer == 360)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if (Timer == 420)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if (Timer == 480)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if (Timer == 540)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer == 600)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer == 660)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if (Timer == 720)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer == 780)
            {
                Timer = 0;
            }
            #endregion
            #region Second_Phase
            if (Timer_SecondPhase == 60)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if (Timer_SecondPhase == 100)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer_SecondPhase == 180)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer_SecondPhase == 210)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if(210 < Timer_SecondPhase && Timer_SecondPhase < 270)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    if (player.velocity.X > 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 position = player.Center + new Vector2(300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
                            Sword.noGravity = true;
                        }
                    }
                    if (player.velocity.X < 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 position = player.Center + new Vector2(-300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
                            Sword.noGravity = true;
                        }
                    }
                    if (player.velocity.X == 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 position = player.Center + new Vector2(300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
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
                            Vector2 position = player.Center + new Vector2(300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
                            Sword.noGravity = true;
                        }
                    }
                    if (player.velocity.X < 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 position = player.Center + new Vector2(-300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
                            Sword.noGravity = true;
                        }
                    }
                    if (player.velocity.X == 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 position = player.Center + new Vector2(300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
                            Sword.noGravity = true;
                        }
                    }
                }
            }
            if (Timer_SecondPhase == 270)
            {
                // ink jets // Temporal Tower
                Timer_SecondPhase = 10000;
            }
            if (Timer_SecondPhase == 330)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if (Timer_SecondPhase == 390)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if (Timer_SecondPhase == 440)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer_SecondPhase == 490)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer_SecondPhase == 550)
            {
                SoundEngine.PlaySound(SoundID.Item171, NPC.position);
                InkBolt();
            }
            if (550 < Timer_SecondPhase && Timer_SecondPhase < 610)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    if (player.velocity.X > 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 position = player.Center + new Vector2(300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
                            Sword.noGravity = true;
                        }
                    }
                    if (player.velocity.X < 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 position = player.Center + new Vector2(-300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
                            Sword.noGravity = true;
                        }
                    }
                    if (player.velocity.X == 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 position = player.Center + new Vector2(-300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
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
                            Vector2 position = player.Center + new Vector2(300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
                            Sword.noGravity = true;
                        }
                    }
                    if (player.velocity.X < 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 position = player.Center + new Vector2(-300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
                            Sword.noGravity = true;
                        }
                    }
                    if (player.velocity.X == 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 position = player.Center + new Vector2(-300f, 150f);
                            Vector2 speed = Main.rand.NextVector2Circular(1.2f, 2f);
                            Dust Sword = Dust.NewDustPerfect(position + speed * 32, DustID.Electric, speed * 3, Scale: 1f);
                            Sword.noGravity = true;
                        }
                    }
                }
            }
            if (Timer_SecondPhase == 610)
            {
                // ink jets // temporal Tower
                Timer_SecondPhase = 11000;
            }
            if (Timer_SecondPhase == 670)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer_SecondPhase == 730)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.position);
                InkRain();
            }
            if (Timer_SecondPhase == 730)
            {
                Timer_SecondPhase = 40;
            }
            #endregion
            #region Temporal_Tower
            if (Timer_SecondPhase == 10001)
            {
                if (player.velocity.X > 0)
                {
                    Teleport_Left();
                }
                if (player.velocity.X < 0)
                {
                    Teleport_Right();
                }
                if (player.velocity.X == 0)
                {
                    Teleport_Left();
                }
            }
            if(Timer_SecondPhase == 10121)
            {
                Timer_SecondPhase = 300;
            }

            if(Timer_SecondPhase >= 10001)
            {
                InkJets();
                NPC.rotation = MathHelper.ToRadians(180);
            }




            if (Timer_SecondPhase == 11001)
            {
                if (player.velocity.X > 0)
                {
                    Teleport_Left();
                }
                if (player.velocity.X < 0)
                {
                    Teleport_Right();
                }
                if (player.velocity.X == 0)
                {
                    Teleport_Right();
                }
            }
            if (Timer_SecondPhase == 11121)
            {
                Timer_SecondPhase = 650;
            }
            #endregion
        }
        public override void OnSpawn(IEntitySource source)
        {
            NPC.position.Y += 90;
            NPC.velocity.Y -= 4;
            base.OnSpawn(source);
        }
        private void Teleport_Left()
        {
            if (NPC.HasValidTarget)
            {
                Vector2 position = player.Center + new Vector2(300f, 150f);
                NPC.Center = position;
                NPC.velocity.X = -6;
                NPC.velocity.Y = 0;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, NPC.whoAmI);
                }
            }
        }
        private void Teleport_Right()
        {
            if (NPC.HasValidTarget)
            {
                Vector2 position = player.Center + new Vector2(-300f, 150f);
                NPC.Center = position;
                NPC.velocity.X = 6;
                NPC.velocity.Y = 0;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, NPC.whoAmI);
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 2;

            if (secondphase == true)
            {
                startFrame = 3;
                finalFrame = 5;
            }
            else
            {
                startFrame = 0;
                finalFrame = 2;
            }
            NPC.frameCounter += 0.8f;
            NPC.frameCounter += NPC.velocity.Length() / 7f;
            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }
        private void Move(Vector2 offset)
        {
            if(Timer_SecondPhase >= 10000)
            {
                return;
            }
            player = Main.player[NPC.target];
            speed = 4.3f;
            if (player.ZoneBeach == false)
            {
                speed = 7f;
            }
            Vector2 moveTo = player.Center + offset;

            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 32f;
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity = move;
        }
        private void InkRain()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 7; i++)
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
                        velocity = new Vector2(0f, 6f);
                    }
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(35));

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top, newVelocity, ModContent.ProjectileType<InkBomb>(), 10, NPC.whoAmI);
                }
            }
        }
        private void Typhoon()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {

                float spawnarea = player.velocity.X * 70;
                Vector2 position = player.Top + new Vector2(spawnarea + Main.rand.Next(-100, 100), Main.rand.Next(50, 100));

                Projectile.NewProjectile(NPC.GetSource_FromAI(), position, -Vector2.UnitY, ModContent.ProjectileType<InkTyphoon>(), 10, 0f, Main.myPlayer);
            }
        }

        private void Pre_Typhoon()
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                if (NPC.HasValidTarget)
                {
                    player = Main.player[NPC.target];
                    float spawnarea = player.velocity.X * 70;
                    Vector2 position = player.Top + new Vector2(spawnarea + Main.rand.Next(-100, 100), Main.rand.Next(50, 100));

                    if (Main.rand.NextBool(1))
                    {
                        Dust.NewDust(position, (int)spawnarea, 1, DustID.Wraith, 1f, -2.5f, 0, default, 1f);
                    }
                }
            }
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (NPC.HasValidTarget)
                {
                    player = Main.player[NPC.target];
                    float spawnarea = player.velocity.X * 70;
                    Vector2 position = player.Top + new Vector2(spawnarea + Main.rand.Next(-100, 100), Main.rand.Next(50, 100));

                    if (Main.rand.NextBool(1))
                    {
                        Dust.NewDust(position, (int)spawnarea, 1, DustID.Wraith, 1f, -2.5f, 0, default, 1f);
                    }
                }
            }
        }
        private void InkJets()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = new(0f, -6.5f);
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 3f / magnitude;
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top, velocity, ModContent.ProjectileType<Ink_Jet>(), 10, NPC.whoAmI);
            }
        }
        private void InkBolt()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Bottom;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 15f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 4.9f);
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, velocity, ModContent.ProjectileType<InkBolt>(), 10, NPC.whoAmI);
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
                for (int k = 0; k < 36; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 4f, -2.5f, 0, default, 1f);
                }
                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Squid_Eye").Type);
                }
                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Squid_Head").Type);
                }
                for (int i = 0; i < 4; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Squid_Tenticles").Type);
                }
            }
        }
        public override void OnKill()
        {
            InfernusSystem.downedSquid = true;
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
                new FlavorTextBestiaryInfoElement("Don't get too indulged by it's fasinating looks, these squids might be small but pack a punch.")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Squid_Bag>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Consumable.Potion>(), 1, 7, 9));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Placeable.Squid_Trophy>(), 10));

            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Placeable.Squid_Relic>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Pets.Squid_PetItem>()));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Magic.Radiant_Staff>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Ink_Sprinkler>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Ranged.Squid_Gun>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Summon.Squid_Fishbowl>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Ranged.Squid_FlameThrower>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Accesories.Squid_Accessory>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Accesories.Ink_Cartridge>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Accesories.Squid_Scroll>(), 2));

            npcLoot.Add(notExpertRule);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * 1.1f);
            NPC.lifeMax = (int)(NPC.lifeMax = 3350 * (int)balance);

            if (Main.masterMode == true)
            {
                NPC.lifeMax = (int)(NPC.lifeMax = 4400 * (int)balance);
                NPC.life = (int)(NPC.lifeMax = 4400 * (int)balance);
                NPC.damage = ((NPC.damage / 2) * 3);
            }
            if (Main.getGoodWorld == true)
            {
                NPC.scale = .6f;
                NPC.lifeMax = (int)(NPC.lifeMax = 5650 * (int)balance);
                NPC.life = (int)(NPC.lifeMax = 5650 * (int)balance);
                NPC.damage = ((NPC.damage / 10) * 13);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (secondphase)
                return true;
            Asset<Texture2D> texture = ModContent.Request<Texture2D>("Infernus/NPCs/Temp_Squid_Glow");

            Rectangle frame;


            frame = texture.Frame();

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new(NPC.width / 2 - frameOrigin.X, NPC.height - frame.Height);
            Vector2 drawPos = NPC.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = NPC.activeTime / 240f + time * 0.1f;

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

                spriteBatch.Draw((Texture2D)texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(215, 245, 245, 50), NPC.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw((Texture2D)texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(215, 245, 245, 77), NPC.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }
            return true;
        }
    }
}