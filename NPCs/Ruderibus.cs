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
    public class Ruderibus : ModNPC
    {
        private Player player;
        private float speed;

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
            NPC.width = 144;
            NPC.height = 140;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit42;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.value = 45000;
            NPC.boss = true;
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
            NPC.rotation = NPC.velocity.X * 0.01f;

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
            Move(new Vector2(Main.rand.Next(0), -320f));

            Timer++;

            if (Timer == 60)
            {
                SoundEngine.PlaySound(SoundID.Item28, NPC.position);
                Shoot();
            }
            if (Timer == 120)
            {
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Cone_Blast();
            }
            if (Timer == 180)
            {
                SoundEngine.PlaySound(SoundID.Item28, NPC.position);
                Shoot();
            }
            if (Timer == 240)
            {
                SoundEngine.PlaySound(SoundID.Item164, NPC.position);
                Iceicle_Wind();
            }
            if (Timer == 300)
            {
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Cone_Blast();
            }
            if (Timer == 360)
            {
                SoundEngine.PlaySound(SoundID.Item28, NPC.position);
                Shoot();
            }
            if (Timer == 400)
            {
                Timer = 10000;
                // Dash1 start
            }
            if (Timer == 700)
            {
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Cone_Blast();
            }
            if (Timer == 760)
            {
                SoundEngine.PlaySound(SoundID.Item28, NPC.position);
                Shoot();
            }
            if (Timer == 820)
            {
                SoundEngine.PlaySound(SoundID.Item28, NPC.position);
                Shoot();
            }
            if (Timer == 860)
            {
                Timer = 20000;
                //Dash 2 starts
            }
            if (Timer == 900)
            {
                SoundEngine.PlaySound(SoundID.Item164, NPC.position);
                Iceicle_Wind();
            }
            if (Timer == 960)
            {
                SoundEngine.PlaySound(SoundID.Item28, NPC.position);
                Shoot();
            }
            if (Timer == 1020)
            {
                SoundEngine.PlaySound(SoundID.Item28, NPC.position);
                Shoot();
            }
            if (Timer == 1080)
            {
                SoundEngine.PlaySound(SoundID.Item28, NPC.position);
                Shoot();
            }
            if (Timer == 1140)
            {
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Cone_Blast();
            }
            if (Timer == 1190)
            {
                Timer = 0;
            }






            if (Timer == 10001)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Blast_Dash();
            }
            if (Timer == 10061)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Blast_Dash();
            }
            if (Timer == 10121)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Blast_Dash();
            }
            if (Timer == 10181)
            {
                //Dash 1 ends
                Timer = 699;
            }


            if (Timer == 20001)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Blast_Dash();
            }
            if (Timer == 20061)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Blast_Dash();
            }
            if (Timer == 20121)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
                SoundEngine.PlaySound(SoundID.Item30, NPC.position);
                Ice_Blast_Dash();
            }
            if (Timer == 20181)
            {
                //Dash 2 ends
                Timer = 899;
            }
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
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 12) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 12) * -1;
                }
            }
        }

        public override void OnKill()
        {
            InfernusSystem.downedRuderibus = true;
        }

        private void Move(Vector2 offset)
        {
            if (Timer >= 10000)
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.HallowedPlants, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f);
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.ApprenticeStorm, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f);
                return;
            }
            player = Main.player[NPC.target];
            speed = 8.6f;
            if (Main.expertMode == true)
            {
                speed = 9.7f;
            }
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 60f;
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity = move;
        }
        private void Shoot()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                var entitySource = NPC.GetSource_FromAI();
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 12f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 8f);
                }

                Projectile.NewProjectile(entitySource, NPC.Center, velocity, ModContent.ProjectileType<Ice_Bolt_Homing>(), 16, NPC.whoAmI);
            }
        }
        private void Ice_Cone_Blast()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 velocity = player.Center - NPC.Center;
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 8f / magnitude;
                    }
                    else
                    {
                        velocity = new Vector2(0f, 7f);
                    }
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(35));
                    newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, newVelocity, ModContent.ProjectileType<Ice_Bolt>(), 18, NPC.whoAmI);
                }
            }
        }
        private void Iceicle_Wind()
        {
            var Ice_Rain = ModContent.ProjectileType<Ice_Rain>();
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(0f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(50f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(100f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(150f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(200f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(250f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(300f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(350f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(400f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(450f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(500f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(550f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(600f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(650f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(700f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-50f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-100f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-150f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-200f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-250f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-300f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-350f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-400f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-450f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-500f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-550f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-600f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-650f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(-700f, Main.rand.Next(new[] { 25f, 100f, 140f }) - 800f), new Vector2(player.velocity.X, Main.rand.Next(new[] { 5f, 7f, 10f })), Ice_Rain, 16, NPC.whoAmI);
            }
        }
        private void Ice_Blast_Dash()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = new(0f, 5f);
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 4.5f / magnitude;
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ProjectileID.FrostWave, 15, NPC.whoAmI);

                Vector2 velocity2 = new(0f, -5f);
                float magnitude2 = Magnitude(velocity);
                if (magnitude2 > 0)
                {
                    velocity2 *= 4.5f / magnitude;
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity2, ProjectileID.FrostWave, 15, NPC.whoAmI);

                Vector2 velocity3 = new(-5f, 0f);
                float magnitude3 = Magnitude(velocity);
                if (magnitude3 > 0)
                {
                    velocity3 *= 4.5f / magnitude;
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity3, ProjectileID.FrostWave, 15, NPC.whoAmI);

                Vector2 velocity4 = new(5f, 0f);
                float magnitude4 = Magnitude(velocity);
                if (magnitude4 > 0)
                {
                    velocity4 *= 4.5f / magnitude;
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity4, ProjectileID.FrostWave, 15, NPC.whoAmI);
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
                }

                if (Main.netMode == NetmodeID.Server && NPC_ < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: NPC_);
                }
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText("A storm rumbles in the distance. Flashes of lightning and thunder are known from afar.", 159, 197, 232);
                }
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    Main.NewText("A storm rumbles in the distance. Flashes of lightning and thunder are known from afar.", 159, 197, 232);
                }
            }
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
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
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