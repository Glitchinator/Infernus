using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    public class HorseShoeCrab : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Horseshoe Crab");
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 54;
            NPC.damage = 0;
            NPC.defense = 8;
            NPC.knockBackResist = 0.9f;
            NPC.width = 80;
            NPC.height = 56;
            NPC.aiStyle = 7;
            NPC.noGravity = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath28;
            AnimationType = NPCID.DemonEye;
            NPC.breath = 999;
            NPC.breathCounter = 999;
            Banner = Item.NPCtoBanner(NPCID.Crab);
            BannerItem = Item.BannerToItem(Banner);
            NPC.value = 25;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("horse1").Type);
                }
                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("horse2").Type);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneBeach)
            {
                return .5f;
            }
            return 0f;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("These horseshoe crabs arn't naturally this big, these pals make good friends for being dinosaurs.")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Pets.CrabItem>(), 40, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Hotdog, 50, 1, 1));
        }
    }
}