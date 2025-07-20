using Infernus.Items.Mounts;
using Infernus.Items.Tools;
using Infernus.Items.Weapon.Melee;
using Infernus.Items.Weapon.Summon;
using Infernus.Placeable;
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
    public class Boulder_Beetle : ModNPC
    {
        private Player player;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 4160;
            NPC.damage = 68;
            NPC.defense = 20;
            NPC.knockBackResist = 0.0f;
            NPC.width = 50;
            NPC.height = 26;
            NPC.aiStyle = 3;
            AIType = NPCID.AnomuraFungus;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            Banner = Item.NPCtoBanner(NPCID.RockGolem);
            BannerItem = Item.BannerToItem(Banner);
            NPC.value = 35;
            AnimationType = NPCID.AnomuraFungus;
        }

        int timer;
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest();

            if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
            {
                NPC.TargetClosest();
                NPC.velocity.Y = NPC.velocity.Y + .3f;
                if (NPC.timeLeft > 20)
                {
                    NPC.timeLeft = 20;
                    return;
                }
            }

            timer++;

            if (timer == 250)
            {
                timer = 5000;
            }
            if (timer == 399)
            {
                NPC.aiStyle = -1;
                AIType = 0;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
                NPC.noGravity = true;
            }
            if (timer == 400)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
            }
            if (timer == 420)
            {
                NPC.noGravity = false;
            }
            if (timer == 459)
            {
                NPC.noGravity = true;
            }
            if(timer == 460)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
            }
            if (timer == 480)
            {
                NPC.noGravity = false;
            }
            if (timer == 519)
            {
                NPC.noGravity = true;
            }
            if (timer == 520)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.position);
                Dash();
            }
            if (timer == 540)
            {
                NPC.noGravity = false;
            }
            if (timer == 600)
            {
                NPC.aiStyle = 3;
                AIType = NPCID.AnomuraFungus;
                timer = 0;
            }

            if (timer >= 5001)
            {
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
            if (timer == 5026 || timer == 5051 || timer ==5076 || timer == 5101|| timer == 5126)
            {
                SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                Shoot();
            }
            if (timer == 5160)
            {
                timer = 260;
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
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 15) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 15) * -1;
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
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        private void Shoot()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 9f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 6f);
                }

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Projectiles.Ice_Rain>(), 14, NPC.whoAmI);
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
                for (int k = 0; k < 24; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Stone, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.StoneBlock, 4, 4, 6));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Rock>(), 1, 11, 26));

            int[] Which_One = [ModContent.ItemType<bold>(), ModContent.ItemType<Boulder_Saddle>(), ModContent.ItemType<Boulder_Flail>()];

            npcLoot.Add(ItemDropRule.OneFromOptions(1, Which_One));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Ranged.July4th>(), 400, 1, 1));
            LeadingConditionRule in_hardmode = new LeadingConditionRule(new Conditions.IsHardmode());

            in_hardmode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Materials.Broken_Heros_Staff>()));

            npcLoot.Add(in_hardmode);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("A miner once lost in the caves of this world, its corpse filled with rocky shrapnel and overgrown flora.")
            });
        }
    }
}