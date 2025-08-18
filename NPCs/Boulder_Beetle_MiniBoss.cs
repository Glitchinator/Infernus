using Infernus.Items.Accesories;
using Infernus.Items.BossSummon;
using Infernus.Items.Mounts;
using Infernus.Items.Weapon.Melee;
using Infernus.Items.Weapon.Summon;
using Infernus.Placeable;
using Infernus.Projectiles;
using Infernus.Projectiles.Raiko.Boss;
using Infernus.Projectiles.Temporal_Glow_Squid.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    [AutoloadBossHead]
    public class Boulder_Beetle_MiniBoss : ModNPC
    {
        private Player player;
        private float speed;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;

            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 2600;
            NPC.damage = 36;
            NPC.defense = 14;
            NPC.knockBackResist = 0.0f;
            NPC.width = 50;
            NPC.height = 32;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 35000;
            NPC.boss = true;
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.npcSlots = 4f;
            Music = -1;
        }
        bool is_dashing = false;
        int rand_i = 0;
        int Timer;
        int Move_Location = 0;
        int Move_X = 0;
        int Move_Y = -250;
        bool should_move = true;
        bool Check_Pos = false;
        int Dust_amount = 6;
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest();

            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.05f;

            if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
            {
                NPC.velocity.Y = NPC.velocity.Y - .3f;
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
            Move(new Vector2(Move_X, Move_Y));
            if (Move_Location == 0)
            {
                Move_X = 0;
                Move_Y = -250;
            }
            if (Move_Location == 1)
            {
                Move_X = 250;
                Move_Y = -150;
            }
            if (Move_Location == 2)
            {
                Move_X = -250;
                Move_Y = -150;
            }
            if (Move_Location == 3)
            {
                Move_X = -300;
                Move_Y = -150;
            }
            if (Move_Location == 4)
            {
                Move_X = 300;
                Move_X = -150;
            }
           
            if (Check_Pos == false)
            {
                Timer++;
            }

            string y = Timer.ToString();
            Main.NewText(y, 229, 214, 127);
            if (Timer == 120)
            {
                Check_Pos = true;
            }
            if (Timer == 141)
            {
                PreDash();
            }
            if (Timer == 151)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                Dash();
            }
            if (Timer == 211)
            {
                should_move = true;
                is_dashing = false;
                Timer = 0;
            }
        }
        private void Move(Vector2 offset)
        {
            if (should_move == false)
            {
                return;
            }
            if (Timer >= 10000)
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.SolarFlare, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f, 0, default, 1.7f);
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Torch, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f, 0, default, 1.7f);
                return;
            }
            player = Main.player[NPC.target];
            //speed = 7f;
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
            if ( magnitude <= 80f)
            {
                if (Check_Pos == true)
                {
                    Timer = 121;
                    Check_Pos = false;
                    should_move = false;
                    is_dashing = true;
                }
                Move_Location = Main.rand.Next(5);
                speed = 6f;
            }
            if(Move_Location == Move_Location)
            {
                speed += 0.1f;
            }
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 16f;
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity = move;
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
        private void Dash()
        {
            player = Main.player[NPC.target];
            var distancevec2 = player.position - NPC.position;
            float magnitude = Magnitude(distancevec2);
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 2.25f;
                NPC.velocity.Y *= 2.25f;
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - (player.position.Y + player.velocity.Y * 10), NPC.Center.X - (player.position.X + player.velocity.X * 10));
                    if (magnitude >= 700f)
                    {
                        magnitude = 700;
                    }
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 9 * (magnitude / 230)) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 9 * (magnitude / 230)) * -1;
                }
            }
            for (int i = 0; i < 30; i++)
            {
                var smoke = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                smoke.velocity *= 1.4f;
            }

            // Spawn a bunch of fire dusts.
            for (int j = 0; j < 14; j++)
            {
                var fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Stone, 0f, 0f, 100, default, 3.5f);
                fireDust.noGravity = true;
                fireDust.velocity *= 7f;
                fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Stone, 0f, 0f, 100, default, 1.5f);
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
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 1;

            int frameSpeed = 4;
            NPC.frameCounter += 0.7f;
            NPC.frameCounter += NPC.velocity.Length() / 13f;
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

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 16; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 4f, -2.5f, 0, default, 1f);
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Meteorite, 4f, -2.5f, 0, default, 1f);
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SolarFlare, 4f, -2.5f, 0, default, 1f);
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Beetle_1").Type);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Beetle_2").Type);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Beetle_3").Type);
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
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
                new FlavorTextBestiaryInfoElement("Big beetle")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.StoneBlock, 4, 4, 6));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Rock>(), 1, 11, 26));

            int[] Which_One = [ModContent.ItemType<bold>(), ModContent.ItemType<Boulder_Saddle>(), ModContent.ItemType<Boulder_Flail>(), ModContent.ItemType<Chiseled_Arrowhead>(), ModContent.ItemType<Rock_Shield>()];

            npcLoot.Add(ItemDropRule.OneFromOptions(1, Which_One));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Ranged.July4th>(), 400, 1, 1));
            LeadingConditionRule in_hardmode = new LeadingConditionRule(new Conditions.IsHardmode());

            in_hardmode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Materials.Broken_Heros_Staff>()));

            npcLoot.Add(in_hardmode);
        }
    }
}