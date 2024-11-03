using Infernus.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    public class Boulder_Bat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 120;
            NPC.damage = 26;
            NPC.defense = 12;
            NPC.knockBackResist = 0.6f;
            NPC.width = 28;
            NPC.height = 30;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath4;
            Banner = Item.NPCtoBanner(NPCID.RockGolem);
            BannerItem = Item.BannerToItem(Banner);
            NPC.value = 40;
            AnimationType = NPCID.GiantBat;
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (NPC.downedPlantBoss == true)
            {
                NPC.lifeMax = 1500;
                NPC.life = NPC.lifeMax;
                NPC.damage = 60;
                NPC.knockBackResist = 0.2f;
                NPC.defense = 18;
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
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Rock>(), 7, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Ranged.July4th>(), 400, 1, 1));
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("A bat stuffed in a rocky sarcophagus, forever claimed by the stone that ensnared it.")
            });
        }
    }
}