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
    public class Boulder_Golem : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 274;
            NPC.damage = 35;
            NPC.defense = 16;
            NPC.knockBackResist = 0.5f;
            NPC.width = 34;
            NPC.height = 42;
            NPC.aiStyle = 26;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit38;
            NPC.DeathSound = SoundID.NPCDeath38;
            Banner = Item.NPCtoBanner(NPCID.RockGolem);
            BannerItem = Item.BannerToItem(Banner);
            NPC.value = 35;
            AnimationType = NPCID.Zombie;
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (NPC.downedPlantBoss == true)
            {
                NPC.lifeMax = 1190;
                NPC.life = NPC.lifeMax;
                NPC.damage = 76;
                NPC.knockBackResist = 0.2f;
                NPC.defense = 28;
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
                new FlavorTextBestiaryInfoElement("A sentient piece of earth, dwells in the caves below as it moves through the walls of them as it were one.")
            });
        }
    }
}