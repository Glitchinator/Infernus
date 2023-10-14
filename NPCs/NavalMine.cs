using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Infernus.NPCs
{
    public class NavalMine : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Naval Mine");
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 3600;
            NPC.damage = 70;
            NPC.defense = 1;
            NPC.knockBackResist = 0.0f;
            NPC.width = 34;
            NPC.height = 34;
            NPC.aiStyle = 20;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.breath = 999;
            NPC.breathCounter = 999;
            NPC.noTileCollide = true;
            Banner = Item.NPCtoBanner(NPCID.DeadlySphere);
            BannerItem = Item.BannerToItem(Banner);
            NPC.value = Item.buyPrice(0, 0, 5, 0);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Ichor, 4f * hitDirection, -2.5f, 0, default, 1f);
                }
                for (int k = 0; k < 26; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedsWingsRun, 4f * hitDirection, -2.5f, 0, default, 2f);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneBeach && NPC.downedPlantBoss)
            {
                return SpawnCondition.OceanMonster.Chance * .5f;
            }
            return 0f;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("Never have I ever thought to ever see the sight of a naval mine moving at me, quite terrifing.")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Ranged.SuperShredder>(), 40, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Melee.Subslicer>(), 40, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.MilkCarton, 50, 1, 1));
        }
    }
}