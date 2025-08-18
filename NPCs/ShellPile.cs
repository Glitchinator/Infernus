using Infernus.Items.Accesories;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    public class ShellPile : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 76;
            NPC.damage = 22;
            NPC.defense = 8;
            NPC.knockBackResist = 0.9f;
            NPC.width = 80;
            NPC.height = 56;
            NPC.aiStyle = 1;
            NPC.noGravity = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.breath = 999;
            NPC.breathCounter = 999;
            Banner = Item.NPCtoBanner(NPCID.SandSlime);
            BannerItem = Item.BannerToItem(Banner);
            NPC.value = 32;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 1; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Dirt, 4f, -2.5f, 0, default, 1f);
                }
                int frontGoreType = Mod.Find<ModGore>("Shell_Pile").Type;

                var entitySource = NPC.GetSource_Death();
                for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), frontGoreType);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneBeach && spawnInfo.PlayerInTown == false)
            {
                return .6f;
            }
            return 0f;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("The gelatinous creature that was once a slime is now a mixture of sand,tar and shells.")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Ocean_Crest>(), 4));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wave_Crasher>(), 4));
            npcLoot.Add(ItemDropRule.Common(ItemID.ShellPileBlock, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Coral, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Seashell, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.JunoniaShell, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.LightningWhelkShell, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.TulipShell, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Starfish, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.BBQRibs, 50, 1, 1));
        }
    }
}