﻿using Infernus.Projectiles;
using Infernus.Projectiles.Temporal_Glow_Squid.Boss;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Conditions = Terraria.GameContent.ItemDropRules.Conditions;

namespace Infernus.NPCs
{
    [AutoloadBossHead]
    public class Ruderibus : ModNPC
    {
        private Player player;
        private float speed = 15f;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 8130;
            NPC.damage = 34;
            NPC.defense = 16;
            NPC.knockBackResist = 0.0f;
            NPC.width = 104;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit42;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.value = 45000;
            NPC.boss = true;
            Music = MusicLoader.GetMusicSlot("Infernus/Music/Frigid_Predator");
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.npcSlots = 10;
        }

        int Timer;
        bool is_dashing = false;
        bool is_below = false;
        bool Check_below = false;
        bool Spinnn = false;
        bool Chasing = false;



        int Move_X = 0;
        int Move_Y = 0;
        int Move_Location = 0;
        bool should_move = true;
        public override void AI()
        {
            if (Check_below == false)
            {
                InfernusWorld.Ruderibus_Timer++;
            }
            Timer = InfernusWorld.Ruderibus_Timer;
            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest(true);

            if (Spinnn == false)
            {
                NPC.rotation = NPC.velocity.X * 0.01f;
            }
            else
            {
                NPC.rotation = NPC.rotation += 0.2f;
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
                NPC.velocity.X = NPC.velocity.X * 0.988f;
                NPC.velocity.Y = NPC.velocity.Y * 0.988f;
            }
            if (is_dashing == false)
            {
                Move(new Vector2(Move_X, Move_Y));
            }
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
                Move_Y = 0;
            }
            if (is_below == true)
            {
                Move_X = 0;
                Move_Y = 560;
            }
            /*
            if (Timer == 60)
            {
                is_above = true;
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Cone_Blast();
            }
            */
            if (Timer == 340)
            {
                is_dashing = true;
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
            }
            if (Timer == 420)
            {
                is_dashing = true;
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
            }
            if (Timer == 500)
            {
                is_dashing = true;
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
            }
            if (Timer == 580)
            {
                is_dashing = true;
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
            }
            if (Timer == 660)
            {
                is_dashing = false;
                //InfernusWorld.Ruderibus_Switch = false;
                speed = 8f;
            }

            if(Timer == 960)
            {
                if(is_below == false)
                {
                    Check_below = true;
                }
                is_below = true;
                //Timer = 801;
            }

            if(Timer == 1020)
            {
                is_dashing = true;
                Strong_Dash();
            }
            if(Timer == 1140)
            {
                is_dashing = false;
                is_below = false;
                speed = 8f;
            }


            if(Timer == 1900)
            {
                if (is_below == false)
                {
                    Check_below = true;
                }
                is_below = true;
                // hands start speeding up
            }
            if(Timer == 1960)
            {
                is_dashing = true;
                Strong_Dash();
            }
            if(Timer == 1970)
            {
                Ice_Wall();
            }
            if (Timer == 1990)
            {
                Ice_Wall();
            }
            if (Timer == 2010)
            {
                Ice_Wall();
            }
            if (Timer == 2040)
            {
                is_dashing = false;
                is_below = false;
                speed = 8f;
            }

            if (Timer == 2300)
            {
                Spinnn = true;
                Chasing = true;
                speed = 8f;
                Move_Location = 3;
            }
            if (Timer == 2700)
            {
                Spinnn = false;
                Chasing = false;
                Move_Location = 0;
            }







            /*
            if (Timer == 1020)
            {
                InfernusWorld.Ruderibus_Timer = 0;
                InfernusWorld.Ruderibus_Switch = false;
            }


            if (Timer == 10001)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
            }
            if (Timer == 10081)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
            }
            if (Timer == 10161)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
                is_dashing = true;
            }
            if (Timer == 10241)
            {
                //Dash 1 ends
                InfernusWorld.Ruderibus_Timer = 301;
                InfernusWorld.Ruderibus_Switch = false;
                is_dashing = false;
            }
            */
        }
        private void Ice_Wall()
        {
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Ruderibus_Gas_Mine>(), NPC.whoAmI);
        }
        private void Dash()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 2.25f;
                NPC.velocity.Y *= 2.25f;
                {
                    float rotation = (float)Math.Atan2((NPC.Center.Y) - (player.position.Y + (player.height)), (NPC.Center.X) - (player.position.X + (player.width)));
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 11) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 11) * -1;
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
                var fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.HallowedPlants, 0f, 0f, 100, default, 3.5f);
                fireDust.noGravity = true;
                fireDust.velocity *= 7f;
                fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Glass, 0f, 0f, 100, default, 1.5f);
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
        }
        private void Strong_Dash()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 2.25f;
                NPC.velocity.Y *= 2.25f;
                {
                    float rotation = (float)Math.Atan2((NPC.Center.Y) - (player.position.Y + (player.height)), (NPC.Center.X) - (player.position.X + (player.width)));
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 21) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 21) * -1;
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
                var fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.HallowedPlants, 0f, 0f, 100, default, 3.5f);
                fireDust.noGravity = true;
                fireDust.velocity *= 7f;
                fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Glass, 0f, 0f, 100, default, 1.5f);
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
        }

        public override void OnKill()
        {
            InfernusSystem.downedRuderibus = true;
            InfernusWorld.Ruderibus_Timer = 0;

            if (InfernusSystem.Equite_Generated == false)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    return;
                }
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("Equite has been generated in your caves!", 229, 214, 127);
                    }
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Equite has been generated in your caves!"), new(229, 214, 127), -1);
                    }
                    for (int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 1E-04); i++)
                    {
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)GenVars.rockLayer, Main.UnderworldLayer), WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(5, 9), (ushort)ModContent.TileType<Tiles.Equite_Ore_Tile>());
                    }
                    InfernusSystem.Equite_Generated = true;
                });
            }
        }

        private void Move(Vector2 offset)
        {
            if (should_move == false)
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.HallowedPlants, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f);
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.ApprenticeStorm, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f);
                return;
            }
            player = Main.player[NPC.target];
            /*
            if (is_above == true)
            {
                speed = 20f;
            }
            else if (Main.expertMode == true)
            {
                speed = 9.7f;
            }
            else
            {
                speed = 8.6f;
            }
            */

            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
            if (Move_Location == Move_Location && Chasing == false)
            {
                speed += 0.1f;
            }
            if (magnitude <= 80f && Chasing == false)
            {
                Move_Location = Main.rand.Next(3);
                Check_below = false;
                speed = 8f;
            }
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance;
            turnResistance = 40f;
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity = move;
        }
        private void Ice_Cone_Blast()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                {
                    Vector2 velocity = new(0f, 8f);
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
        private void Spawn_IceShards()
        {
            for (int i = 0; i < 4; i++)
            {
                int NPC_ = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Ruderibus_Hand>(), NPC.whoAmI);
                NPC Ice = Main.npc[NPC_];

                if (Ice.ModNPC is Ruderibus_Hand minion)
                {
                    minion.ParentIndex = NPC.whoAmI;
                    minion.PositionIndex = i;
                    minion.Which_Fist = i;
                }

                if (Main.netMode == NetmodeID.Server && NPC_ < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: NPC_);
                }
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 48; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ApprenticeStorm, 4f, -2.5f, 0, default, 1f);
                }
                for (int k = 0; k < 48; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.HallowedPlants, 4f, -2.5f, 0, default, 1f);
                }
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            }
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = (int)(NPC.damage * 1.15f);
            NPC.lifeMax = (int)(NPC.lifeMax = 12750 * (int)balance);

            if (Main.masterMode == true)
            {
                NPC.lifeMax = (int)(NPC.lifeMax = 15600 * (int)balance);
                NPC.life = (int)(NPC.lifeMax = 15600 * (int)balance);
                NPC.damage = ((NPC.damage / 2) * 3);
            }
            if (Main.getGoodWorld == true)
            {
                NPC.scale = .8f;
                NPC.lifeMax = (int)(NPC.lifeMax = 21000 * (int)balance);
                NPC.life = (int)(NPC.lifeMax = 21000 * (int)balance);
                NPC.damage = ((NPC.damage / 10) * 13);
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            InfernusWorld.Ruderibus_Timer = 0;
            Spawn_IceShards();
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            int chance = 13;

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.BossSummon.OctopusBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Placeable.RudeTrophy>(), 10));

            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Placeable.RudeRelic>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Pets.RudeItem>()));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Materials.IceSpikes>(), 1, 58, 58));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.FlurryBoots, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.IceMirror, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.IceBoomerang, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.IceBlade, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.BlizzardinaBottle, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.SnowballCannon, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.IceSkates, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.IceMachine, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.Fish, chance, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Infernal_Bat>()));

            npcLoot.Add(notExpertRule);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("A giant ice construct. Filled with the life of a frozen heart, this machine prows the underground ice caverns and leads a hive of ice catalyst. Some call it a hive mind how it reacts quickly to signals and how it reaches it's sight over the terrain.")
            });
        }
    }
}