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
    public class Boulder_Slime : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 164;
            NPC.damage = 30;
            NPC.defense = 14;
            NPC.knockBackResist = 0.6f;
            NPC.width = 38;
            NPC.height = 34;
            NPC.aiStyle = 41;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath15;
            Banner = Item.NPCtoBanner(NPCID.RockGolem);
            BannerItem = Item.BannerToItem(Banner);
            NPC.value = 35;
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (NPC.downedPlantBoss == true)
            {
                NPC.lifeMax = 1204;
                NPC.life = NPC.lifeMax;
                NPC.damage = 56;
                NPC.knockBackResist = 0.4f;
                NPC.defense = 26;
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
                for (int k = 0; k < 20; k++)
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
                new FlavorTextBestiaryInfoElement("A boulder with overgrown moss enveloping it. Some tales say that the dark spots were once eyes.")
            });
        }
    }
}