using Infernus.Items.BossSummon;
using Infernus.Items.Weapon.Melee;
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
    public class Raiko : ModNPC
    {
        private Player player;
        private float speed;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;

            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 3250;
            NPC.damage = 26;
            NPC.defense = 16;
            NPC.knockBackResist = 0.0f;
            NPC.width = 218;
            NPC.height = 204;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.Item62;
            NPC.DeathSound = SoundID.NPCDeath10;
            NPC.value = 35000;
            NPC.boss = true;
            Music = MusicID.Boss2;
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.npcSlots = 6;
        }

        int Timer;
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            NPC.rotation = NPC.velocity.ToRotation();

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
            Move(new Vector2((Main.rand.Next(0)), -410f));
            {
                Timer++;
                if (Timer == 60)
                {
                    //Dash begins 1
                    Timer = 10000;
                }
                if (Timer == 120)
                {
                    SoundEngine.PlaySound(SoundID.Item124, NPC.position);
                    Meteor_Shower();
                }
                if (Timer == 200)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
                    Meteor_Spawner();
                }
                if (Timer == 260)
                {
                    //Dash begins 2
                    Timer = 11000;
                }
                if (Timer == 320)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
                    Meteor_Spawner();
                }
                if (Timer == 380)
                {
                    SoundEngine.PlaySound(SoundID.Item124, NPC.position);
                    Meteor_Shower();
                }
                if (Timer == 460)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
                    Meteor_Spawner();
                }
                if (Timer == 540)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
                    Meteor_Spawner();
                }
                if (Timer == 600)
                {
                    //Dash begins 3
                    Timer = 12000;
                }
                if (Timer == 660)
                {
                    SoundEngine.PlaySound(SoundID.Item124, NPC.position);
                    Meteor_Shower();
                }
                if (Timer == 740)
                {
                    SoundEngine.PlaySound(SoundID.Item124, NPC.position);
                    Meteor_Shower();
                }
                if (Timer == 800)
                {
                    Timer = 0;
                }


                if (Timer == 10001)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash();
                }
                if (Timer == 10051)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash();
                }
                if (Timer == 10101)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash();
                }
                if (Timer == 10151)
                {
                    //Dash ends 1
                    Timer = 80;
                }


                if (Timer == 11001)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash();
                }
                if (Timer == 11051)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash();
                }
                if (Timer == 11101)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash();
                }
                if (Timer == 11151)
                {
                    //Dash ends 2
                    Timer = 260;
                }



                if (Timer == 12001)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash();
                }
                if (Timer == 12051)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash();
                }
                if (Timer == 12101)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                    Dash();
                }
                if (Timer == 12151)
                {
                    //Dash ends 3
                    Timer = 620;
                }
            }
        }
        private void Move(Vector2 offset)
        {
            if (Timer >= 10000)
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.SolarFlare, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f);
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Torch, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f);
                return;
            }
            player = Main.player[NPC.target];
            speed = 7f;
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
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
        private void Dash()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 2.1f;
                NPC.velocity.Y *= 2.1f;
                {
                    float rotation = (float)Math.Atan2((NPC.Center.Y) - (player.position.Y + (player.height)), (NPC.Center.X) - (player.position.X + (player.width)));
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 11) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 11) * -1;
                }
            }

            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && InfernusSystem.Level_systemON == true)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 10f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 2f);
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, velocity, ProjectileID.InfernoHostileBlast, 15, NPC.whoAmI);
            }
        }
        public override void OnKill()
        {
            InfernusSystem.downedRaiko = true;
        }
        private void Meteor_Spawner()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = new(10f, -5f);
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 3f / magnitude;
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Meteor_Flare>(), 13, NPC.whoAmI);

                Vector2 velocity2 = new(-10f, -5f);
                float magnitude2 = Magnitude(velocity);
                if (magnitude2 > 0)
                {
                    velocity2 *= 3f / magnitude;
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity2, ModContent.ProjectileType<Meteor_Flare>(), 13, NPC.whoAmI);
                if (InfernusSystem.Level_systemON == true)
                {
                    Vector2 velocity3 = new(0f, -5f);
                    float magnitude3 = Magnitude(velocity);
                    if (magnitude3 > 0)
                    {
                        velocity3 *= 3f / magnitude;
                    }
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity3, ModContent.ProjectileType<Meteor_Flare>(), 13, NPC.whoAmI);
                }
            }
        }
        private void Meteor_Shower()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(100f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(200f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(300f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(400f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(500f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(600f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(700f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-100f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-200f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-300f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-400f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-500f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-600f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-700f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 5f, 7f, 10f })), ModContent.ProjectileType<Meteor_Storm>(), 13, NPC.whoAmI);
            }
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 2;

            int frameSpeed = 8;
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
            if (NPC.life <= 0)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText("Meteors storm the upper atmosphere. Burning before they touch ground, such a sight to see.", 239, 106, 15);
                }
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    Main.NewText("Meteors storm the upper atmosphere. Burning before they touch ground, such a sight to see.", 239, 106, 15);
                }
            }

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
                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("Raiko_Head").Type);
                }
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            }
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * 1.15f);
            NPC.lifeMax = (int)(NPC.lifeMax = 4860 * (int)balance);

            if (Main.masterMode == true)
            {
                NPC.lifeMax = (int)(NPC.lifeMax = 5980 * (int)balance);
                NPC.life = (int)(NPC.lifeMax = 5980 * (int)balance);
                NPC.damage = ((NPC.damage / 2) * 3);
            }
            if (Main.getGoodWorld == true)
            {
                NPC.scale = 1.1f;
                NPC.lifeMax = (int)(NPC.lifeMax = 6750 * (int)balance);
                NPC.life = (int)(NPC.lifeMax = 6750 * (int)balance);
                NPC.damage = ((NPC.damage / 10) * 13);
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
                new FlavorTextBestiaryInfoElement("Conceived from space itself, the vengeful spite meteor is here to get revenge for your actions")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Boss1bag>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Tools.Day>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Placeable.Trophy>(), 10));

            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Placeable.RaikoRelic>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Pets.RaikoPetItem>()));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Magic.MeteorEater>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BoldnBash>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Ranged.Firebow>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Summon.Minion>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Meteor>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Materials.Hot>(), 1, 58, 58));

            npcLoot.Add(notExpertRule);
        }
    }
}