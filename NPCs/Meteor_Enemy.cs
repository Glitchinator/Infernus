using Infernus.Items.Accesories;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    public class Meteor_Enemy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 18;
            NPC.damage = 16;
            NPC.defense = 3;
            NPC.knockBackResist = 0.0f;
            NPC.width = 50;
            NPC.height = 30;
            NPC.aiStyle = 10;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.Item62;
            NPC.DeathSound = SoundID.NPCDeath14;
            Banner = Item.NPCtoBanner(NPCID.MeteorHead);
            BannerItem = Item.BannerToItem(Banner);
            NPC.value = 45;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 16; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 4f, -2.5f, 0, default, 1f);
                }
                for (int k = 0; k < 16; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SolarFlare, 4f, -2.5f, 0, default, 1f);
                }
                for (int k = 0; k < 16; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 4f, -2.5f, 0, default, 1f);
                }
            }
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
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneNormalSpace && spawnInfo.PlayerInTown == false || spawnInfo.Player.ZoneMeteor)
            {
                return .6f;
            }
            return 0f;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Strange to say that a Meteorite came to life, but its even stranger trying to figure out where the smaller ones come from.")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Materials.Scorched_Sinew>(), 1, 0, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.MilkCarton, 50, 1, 1));

            npcLoot.Add(ItemDropRule.ByCondition(new Special_DropRules.Raiko_Drop_Rule(), ModContent.ItemType<Meteor_Core>(), 10));
        }
    }
}