using Infernus.Items.BossSummon;
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
    public class Boulderminiboss : ModNPC
    {
        private Player player;
        private float speed;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 36000;
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
            Music = MusicID.Boss4;
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
            NPC.rotation = NPC.velocity.X * 0.02f;

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
                Move(new Vector2((Main.rand.Next(0)), -350f));
            }
            Timer++;
            if (Timer == 60)
            {
                Timer = 11000;
            }
            if (Timer == 100)
            {
                Boulder_Rain();
                SoundEngine.PlaySound(SoundID.Item74, NPC.position);
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
                Boulder_Rain();
                SoundEngine.PlaySound(SoundID.Item74, NPC.position);
            }
            if (Timer == 600)
            {
                Timer = 12000;
            }
            if (Timer == 700)
            {
                Boulder_Rain();
                SoundEngine.PlaySound(SoundID.Item74, NPC.position);
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
            if (Timer == 10010)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10020)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10030)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10040)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10050)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10060)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10070)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10075)
            {
                Dash();
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
            }
            if (Timer == 10080)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10090)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10100)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10110)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10120)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10130)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10140)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10150)
            {
                Rock_Rain_Dash();
                Dash();
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
            }
            if (Timer == 10160)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10170)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10180)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10190)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10200)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10210)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10220)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10225)
            {
                Dash();
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
            }
            if (Timer == 10230)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10240)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10250)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10260)
            {
                Rock_Rain_Dash();
            }
            if (Timer == 10270)
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
        }
        private void Move(Vector2 offset)
        {
            if (Timer >= 10000)
            {
                return;
            }
            player = Main.player[NPC.target];
            speed = 11.5f;
            Vector2 moveTo = player.Center + offset;

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
        private void Dash()
        {
            player = Main.player[NPC.target];
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 2.4f;
                NPC.velocity.Y *= 2.4f;
                {
                    float rotation = (float)Math.Atan2((NPC.Center.Y) - (player.position.Y + (player.height)), (NPC.Center.X) - (player.position.X + (player.width)));
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 15) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 15) * -1;
                }
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
                for (int i = 0; i < 18; i++)
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
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(360));

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, newVelocity, ModContent.ProjectileType<Boulder_Bolt>(), 20, NPC.whoAmI);
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
                for (int i = 0; i < 2; i++)
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
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(25));

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, newVelocity, ModContent.ProjectileType<Boulder_Bolt>(), 20, NPC.whoAmI);
                }
            }
        }
        private void Rock_Rain_Dash()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 velocity = new(Main.rand.Next(new[] { -5f, 8f, 5f, -8f }), -6f);

                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top, newVelocity, ModContent.ProjectileType<Boulder_Rain2>(), 20, NPC.whoAmI);
                }
            }
        }
        private void Homing_Shotts()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 6; i++)
                {
                    player = Main.player[NPC.target];
                    Vector2 velocity = player.Center - NPC.Bottom;
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 11f / magnitude;
                    }
                    else
                    {
                        velocity = new Vector2(0f, 5f);
                    }
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(65));

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, newVelocity, ModContent.ProjectileType<Boulder_Rocket>(), 20, NPC.whoAmI);
                }
            }
        }
        private void Boulder_Rain()
        {
            int damage = 16;
            var proj = ModContent.ProjectileType<Boulder_Rain>();
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(100f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(200f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(300f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(400f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(500f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(600f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(700f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-100f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-200f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-300f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-400f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-500f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-600f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-700f, Main.rand.Next(new[] { 75f, 150f, 200f }) - 800f), new Vector2(0f, Main.rand.Next(new[] { 4f, 7f })), proj, damage, NPC.whoAmI);
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
                new FlavorTextBestiaryInfoElement("Don't get too indulged by it's fasinating looks, these squids might be small but pack a punch.")
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
            NPC.lifeMax = (NPC.lifeMax = 44000 * (int)balance);

            if (Main.masterMode == true)
            {
                NPC.lifeMax = (NPC.lifeMax = 52000 * (int)balance);
                NPC.life = (NPC.lifeMax = 52000 * (int)balance);
                NPC.damage = ((NPC.damage / 2) * 3);
            }
            if (Main.getGoodWorld == true)
            {
                NPC.scale = 2f;
                NPC.lifeMax = (NPC.lifeMax = 60000 * (int)balance);
                NPC.life = (NPC.lifeMax = 60000 * (int)balance);
                NPC.damage = ((NPC.damage / 10) * 13);
            }
        }
    }
}