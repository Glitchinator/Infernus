using Infernus.Invas;
using Infernus.Items.Weapon.HardMode.Accessories;
using Infernus.NPCs;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus
{
    public class InfernusNPC : GlobalNPC
    {
        public static bool Is_Spawned = false;

        public static bool Plant_Spawned = false;

        public static bool Cursed_Spawned = false;

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.RedDevil)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Summon.DemonStaff>(), 40, 1, 1));
            }
            if (npc.type == NPCID.MartianSaucer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Tools.MartianPole>(), 20, 1, 1));
            }
            if (npc.type == NPCID.ShadowFlameApparition || npc.type == NPCID.GoblinSummoner)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Summon.GoblinStaff>(), 50, 1, 1));
            }
            if (npc.type == NPCID.GiantBat)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.HardMode.Summon.BatStaff>(), 80, 1, 1));
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapon.Ranged.Eye_Launcher>(), 1, 1, 1));
            }
            if (npc.type == NPCID.Bunny || npc.type == NPCID.CorruptBunny || npc.type == NPCID.CrimsonBunny || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BunnyXmas || npc.type == NPCID.GemBunnyAmber || npc.type == NPCID.GemBunnyAmethyst || npc.type == NPCID.GemBunnyDiamond || npc.type == NPCID.GemBunnyEmerald || npc.type == NPCID.GemBunnyRuby || npc.type == NPCID.GemBunnySapphire || npc.type == NPCID.GemBunnyTopaz || npc.type == NPCID.GoldBunny)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Materials.Easter_Egg>(), 18, 1, 3));
            }
            if (npc.type == NPCID.UndeadMiner || npc.type == NPCID.ArmoredSkeleton)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Accesories.Drill_Bit>(), 40, 1, 1));
            }
            if (npc.type == NPCID.Vampire || npc.type == NPCID.VampireBat)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Souldrinker>(), 40, 1, 1));
            }
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (InfernusSystem.Level_systemON == true && npc.boss == true)
            {
                npc.damage = ((npc.damage / 5) * 7);
                npc.lifeMax = ((npc.life / 5) * 7);


                npc.life = npc.lifeMax;
                npc.lifeMax = npc.life;

            }
            if (npc.type == ModContent.NPCType<Chorus_Plant>())
            {
                if (Plant_Spawned == false)
                {
                    Plant_Spawned = true;
                }
            }
            if (npc.type == ModContent.NPCType<Cursed_Wanderer>())
            {
                if (Cursed_Spawned == false)
                {
                    Cursed_Spawned = true;
                }
            }
        }
        public override void AI(NPC npc)
        {
            if (InfernusSystem.Level_systemON == true && npc.boss == true)
            {
                if(Is_Spawned == false)
                {
                    Is_Spawned = true;
                }
                return;
            }
        }
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add(ItemID.FlareGun);
                shop.Add(ItemID.Flare);
            }
            if (shop.NpcType == NPCID.Cyborg)
            {
                shop.Add(ModContent.ItemType<Items.Weapon.HardMode.Magic.Golemretrib>(), Condition.DownedGolem);
            }
            if (shop.NpcType == NPCID.ArmsDealer)
            {
                shop.Add(ModContent.ItemType<Items.Weapon.HardMode.Ranged.ClockSMG>(), Condition.DownedMechBossAny);
            }
            if (shop.NpcType == NPCID.ArmsDealer)
            {
                shop.Add(ModContent.ItemType<Items.Weapon.HardMode.Ranged.TommyGun>(), Condition.DownedPlantera);
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (InfernusWorld.BoulderInvasionUp && (Main.invasionX == Main.spawnTileX))
            {
                pool.Clear();
                if (NPC.downedPlantBoss)
                {
                    if (Main.rand.Next(5) > 0)
                    {
                        foreach (int i in BoulderInvasion.HMInvaders)
                        {
                            pool.Add(i, 1f);
                        }

                        foreach (int i in BoulderInvasion.PHMInvaders)
                        {
                            pool.Add(i, 1f);
                        }
                    }
                }
                else
                {
                    foreach (int i in BoulderInvasion.PHMInvaders)
                    {
                        pool.Add(i, 1f);
                    }
                }
            }
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (InfernusWorld.BoulderInvasionUp && (Main.invasionX == Main.spawnTileX))
            {
                spawnRate = 5;
                maxSpawns = 14;
            }
        }

        public override void PostAI(NPC npc)
        {
            if (InfernusWorld.BoulderInvasionUp && (Main.invasionX == Main.spawnTileX))
            {
                npc.timeLeft = 300;
            }
        }

        public override void OnKill(NPC npc)
        {
            Player player = new Player();
            if (InfernusWorld.BoulderInvasionUp)
            {
                int[] FullList = BoulderInvasion.GetInvadersNOW();
                foreach (int invader in FullList)
                {
                    if (npc.type == invader)
                    {
                        Main.invasionSize -= 1;
                        NetMessage.SendData(MessageID.InvasionProgressReport, player.whoAmI, -1, null, Main.invasionSizeStart - Main.invasionSize, Main.invasionSizeStart, Main.invasionType, 0f, 0, 0, 0);
                    }
                }
            }
            if (InfernusSystem.Level_systemON == true && npc.boss == true)
            {
                var modPlayer = Main.LocalPlayer.GetModPlayer<InfernusPlayer>();
                modPlayer.Stress_Current = 0;
            }
            if (InfernusSystem.Level_systemON == true)
            {
                if (npc.type == ModContent.NPCType<Chorus_Plant>())
                {
                    npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<Items.Tools.Chorus_Bloom>(), 99);
                }
                if (npc.type == ModContent.NPCType<Cursed_Wanderer>())
                {
                    npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<Items.Tools.Cursed_Soul>(), 99);
                }
            }
        }
    }
}
