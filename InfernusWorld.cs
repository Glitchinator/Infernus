using Infernus.Config;
using Infernus.Invas;
using Infernus.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace Infernus
{
    public class InfernusWorld : ModSystem
    {
        public static bool BoulderInvasionUp = false;
        public static bool downedBoulderInvasion = false;

        public int Boulder_Cooldown = 360000; // 1.6 hour cooldown for boulder invasion, for natural spawning

        public static int Ruderibus_Timer;

        public static bool Ruderibus_Switch = false;


        public int Last_Plant_Spawn = 10800; // miniboss natural spawning timer

        public int Last_Cursed_Spawn = 10800; // miniboss natural spawning timer

        private UserInterface XP_BarUserInterface;

        internal Level_UI Level_UI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                Level_UI = new();
                XP_BarUserInterface = new();
                XP_BarUserInterface.SetState(Level_UI);
            }
        }
        public override void UpdateUI(GameTime gameTime)
        {
            XP_BarUserInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "Infernus: XP Bar",
                    delegate
                    {
                        XP_BarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public override void OnWorldLoad()
        {
            Main.invasionSize = 0;
            BoulderInvasionUp = false;
            downedBoulderInvasion = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            var downed = new List<string>();
            if (downedBoulderInvasion) downed.Add("BoulderInvasion");

            new TagCompound {
                {"downed", downed}
            };
        }

        public override void LoadWorldData(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedBoulderInvasion = downed.Contains("BoulderInvasion");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new();
            flags[0] = downedBoulderInvasion;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedBoulderInvasion = flags[0];
        }
        public override void PostUpdateEverything()
        {
            if (BoulderInvasionUp)
            {
                if (Main.invasionX == Main.spawnTileX)
                {
                    BoulderInvasion.CheckInvasionProgress();
                }
                BoulderInvasion.UpdateBoulderInvasion();
            }
            Boulder_Invasion_Try_Spawn();

            if (InfernusNPC.Plant_Spawned == true && Last_Plant_Spawn > 0)
            {
                Last_Plant_Spawn--;
            }
            if (Last_Plant_Spawn == 0)
            {
                InfernusNPC.Plant_Spawned = false;
                Last_Plant_Spawn = 10800;
            }
            if (InfernusNPC.Cursed_Spawned == true && Last_Cursed_Spawn > 0)
            {
                Last_Cursed_Spawn--;
            }
            if (Last_Cursed_Spawn == 0)
            {
                InfernusNPC.Cursed_Spawned = false;
                Last_Cursed_Spawn = 10800;
            }
        }
        public override void PostSetupContent()
        {
            DoBossChecklistIntegration();
        }
        private void Boulder_Invasion_Try_Spawn()
        {
            if (ModContent.GetInstance<InfernusConfig>().No_Invasion == false)
            {
                if (Main.dayTime == true && Boulder_Cooldown > 0)
                {
                    Boulder_Cooldown--;
                }
                if (Boulder_Cooldown <= 0 && Main.rand.NextBool(50000) && BoulderInvasionUp == false && NPC.downedBoss3 == true && InfernusNPC.Is_Spawned != true)
                {
                    BoulderInvasion.StartBoulderInvasion();

                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("A storm rolls overhead from the west. The ground trembles with vibrations deep below.", 207, 196, 162);
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        Main.NewText("A storm rolls overhead from the west. The ground trembles with vibrations deep below.", 207, 196, 162);
                    }
                    Boulder_Cooldown = 360000; // 1.6 hour reset
                }
            }
        }
        /// Boss Checklist bugs

        /// DONT put a tile of any sort in collection. It bugs and breaks, it only allows if it's loaded from a enemy. So have the item in the enemies loot pool,
        /// but don't let it be dropped. No tile, None. Boss Checklist hates it.

        /// DONT TOUCH THE VERSION. That is a kill switch if it fails to load. DONT TOUCH IT.
        private void DoBossChecklistIntegration()
        {

            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
                return;
            }
            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }
            string RaikoName = "Raiko";

            int RaikoType = ModContent.NPCType<NPCs.Raiko>();

            float RaikoWeight = 3.5f;

            Func<bool> RaikoDowned = () => InfernusSystem.downedRaiko;

            List<int> RaikoCollection = new()
            {
                ModContent.ItemType<Placeable.RaikoRelic>(),
                ModContent.ItemType<Items.Pets.RaikoPetItem>(),
                ModContent.ItemType<Items.Accesories.Shiny>()
            };

            int RaikoSummonItem = ModContent.ItemType<Items.BossSummon.Boss1sum>();

            string RaikoSpawnInfo = $"Use a [i:{RaikoSummonItem}] during night.";

            var RaikoPortrait = (SpriteBatch sb, Rectangle rect, Color color) =>
            {
                Texture2D texture = ModContent.Request<Texture2D>("Infernus/BossChecklist/Raiko").Value;
                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };
            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                RaikoName,
                RaikoWeight,
                RaikoDowned,
                RaikoType,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = RaikoSummonItem,
                    ["spawninfo"] = RaikoSpawnInfo,
                    ["collectibles"] = RaikoCollection,
                    ["customPortrait"] = RaikoPortrait
                }
            );


            string RuderibusName = "Ruderibus";

            int RuderibusType = ModContent.NPCType<NPCs.Ruderibus>();

            float RuderibusWeight = 6.9f;

            Func<bool> RuderibusDowned = () => InfernusSystem.downedRuderibus;

            List<int> RuderibusCollection = new()
            {
                ModContent.ItemType<Placeable.RudeRelic>(),
                ModContent.ItemType<Items.Pets.RudeItem>(),
                ModContent.ItemType<Items.Accesories.Accessory>()
            };

            int RuderibusSummonItem = ModContent.ItemType<Items.BossSummon.BossSummon>();

            string RuderibusSpawnInfo = $"Either craft a [i:{RuderibusSummonItem}] or gain it from killing Ruderibus's Recievers in the snow biome.Then use [i:{RuderibusSummonItem}] in the snow biome to summon Ruderibus.";

            var RuderibusPortrait = (SpriteBatch sb, Rectangle rect, Color color) =>
            {
                Texture2D texture = ModContent.Request<Texture2D>("Infernus/BossChecklist/Ruderibus").Value;
                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                RuderibusName,
                RuderibusWeight,
                RuderibusDowned,
                RuderibusType,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = RuderibusSummonItem,
                    ["spawninfo"] = RuderibusSpawnInfo,
                    ["collectibles"] = RuderibusCollection,
                    ["customPortrait"] = RuderibusPortrait
                }
            );



            string SerphiousName = "Serphious";

            int SerphiousType = ModContent.NPCType<NPCs.Shark>();

            float SerphiousWeight = 16.1f;

            Func<bool> SerphiousDowned = () => InfernusSystem.downedTigerShark;

            List<int> SerphiousCollection = new()
            {
                ModContent.ItemType<Placeable.SharkRelic>(),
                ModContent.ItemType<Items.Weapon.HardMode.Accessories.eleScale>()
            };

            int SerphiousSummonItem = ModContent.ItemType<Items.BossSummon.BeetleBait>();

            string SerphiousSpawnInfo = $"Use a [i:{SerphiousSummonItem}] in the ocean biome";

            var SerphiousPortrait = (SpriteBatch sb, Rectangle rect, Color color) =>
            {
                Texture2D texture = ModContent.Request<Texture2D>("Infernus/BossChecklist/TigerShark").Value;
                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               SerphiousName,
               SerphiousWeight,
               SerphiousDowned,
               SerphiousType,
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = SerphiousSummonItem,
                   ["spawninfo"] = SerphiousSpawnInfo,
                   ["collectibles"] = SerphiousCollection,
                   ["customPortrait"] = SerphiousPortrait
               }
           );

            string TempName = "TemporalGlowSquid";

            int TempType = ModContent.NPCType<NPCs.TemporalSquid>();

            float TempProgression = 1.9f;

            Func<bool> TempDowned = () => InfernusSystem.downedSquid;

            List<int> TempCollection = new()
            {
                ModContent.ItemType<Placeable.Squid_Relic>(),
                ModContent.ItemType<Items.Pets.Squid_PetItem>(),
                ModContent.ItemType<Items.Accesories.Squid_Expert>()
            };

            int TempSummonItem = ModContent.ItemType<Items.BossSummon.Squid_BossSummon>();

            string TempSpawnInfo = $"While in the ocean biome use [i:{TempSummonItem}] no matter the time.";

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               TempName,
               TempProgression,
               TempDowned,
               TempType,
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = TempSummonItem,
                   ["spawninfo"] = TempSpawnInfo,
                   ["collectibles"] = TempCollection
               }
           );

            string EventName = "BoulderInvasionPreHm";

            List<int> EventType = new List<int>()
            {
                ModContent.NPCType<NPCs.Boulder_Bat>(),
                ModContent.NPCType<NPCs.Boulder_Cloud>(),
                ModContent.NPCType<NPCs.Boulder_Corpse>(),
                ModContent.NPCType<NPCs.Boulder_Golem>(),
                ModContent.NPCType<NPCs.Boulder_Slime>()
            };

            float EventWeight = 5.8f;

            Func<bool> EventDowned = () => InfernusSystem.downedBoulderInvasionPHM;

            List<int> EventCollection = new()
            {
            };

            int EventSummonItem = ModContent.ItemType<ThickBoulder>();

            string EventSpawnInfo = $"Spawns naturally when daytime or, use a [i:{EventSummonItem}] during day.";

            var EventPortrait = (SpriteBatch sb, Rectangle rect, Color color) =>
            {
                Texture2D texture = ModContent.Request<Texture2D>("Infernus/BossChecklist/BIPHM").Value;
                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
               "LogEvent",
               Mod,
               EventName,
               EventWeight,
               EventDowned,
               EventType,
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = EventSummonItem,
                   ["spawninfo"] = EventSpawnInfo,
                   ["collectibles"] = EventCollection,
                   ["customPortrait"] = EventPortrait
               }
           );

            string EventNameHM = "BoulderInvasionHm";

            List<int> EventTypeHM = new List<int>()
            {
                ModContent.NPCType<NPCs.Boulder_Beetle>(),
                ModContent.NPCType<NPCs.Boulder_Bat>(),
                ModContent.NPCType<NPCs.Boulderminiboss>(),
                ModContent.NPCType<NPCs.Boulder_Cloud>(),
                ModContent.NPCType<NPCs.Boulder_Corpse>(),
                ModContent.NPCType<NPCs.Boulder_Golem>(),
                ModContent.NPCType<NPCs.Boulder_Slime>(),
                ModContent.NPCType<NPCs.Basalt_Boulder>()
            };

            float EventWeightHM = 12.4f;

            Func<bool> EventDownedHM = () => InfernusSystem.downedBoulderInvasionHM;

            List<int> EventCollectionHM = new()
            {
            };

            int EventSummonItemHM = ModContent.ItemType<ThickBoulder>();

            string EventSpawnInfoHM = $"Spawns naturally when daytime or, use a [i:{EventSummonItemHM}] during day.";

            var EventPortraitHM = (SpriteBatch sb, Rectangle rect, Color color) =>
            {
                Texture2D texture = ModContent.Request<Texture2D>("Infernus/BossChecklist/BIHM").Value;
                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
               "LogEvent",
               Mod,
               EventNameHM,
               EventWeightHM,
               EventDownedHM,
               EventTypeHM,
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = EventSummonItemHM,
                   ["spawninfo"] = EventSpawnInfoHM,
                   ["collectibles"] = EventCollectionHM,
                   ["customPortrait"] = EventPortraitHM
               }
           );

            string CalypsicalName = "Calypsical";

            int CalypsicalType = ModContent.NPCType<NPCs.Calypsical>();

            float CalypsicalWeight = 19.6f;

            Func<bool> CalypsicalDowned = () => InfernusSystem.downedCalypsical;

            List<int> CalypsicalCollection = new()
            {
                ModContent.ItemType<Placeable.MechRelic>(),
                ModContent.ItemType<Items.Pets.MechItem>(),
                ModContent.ItemType<Items.Weapon.HardMode.Accessories.Mechwings>()
            };

            int CalypsicalSummonItem = ModContent.ItemType<Items.BossSummon.Mechsummon>();

            string CalypsicalSpawnInfo = $"Use a [i:{CalypsicalSummonItem}] after Moonlord's defeat";

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               CalypsicalName,
               CalypsicalWeight,
               CalypsicalDowned,
               CalypsicalType,
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = CalypsicalSummonItem,
                   ["spawninfo"] = CalypsicalSpawnInfo,
                   ["collectibles"] = CalypsicalCollection
               }
           );


            string HuskName = "CorruptedHusk";

            int HuskType = ModContent.NPCType<NPCs.Boulderminiboss>();

            float HuskWeight = 12.2f;

            Func<bool> HuskDowned = () => InfernusSystem.downedBoulderBoss;

            List<int> HuskCollection = new()
            {
            };

            int HuskSummonItem = ModContent.ItemType<ThickBoulder>();

            string HuskSpawnInfo = $"Use a [i:{HuskSummonItem}] to spawn the Boulder Invasion. At the end of the Hardmode Boulder Invasion. This boss will spawn.";

            bossChecklistMod.Call(
               "LogBoss",
               Mod,
               HuskName,
               HuskWeight,
               HuskDowned,
               HuskType,
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = HuskSummonItem,
                   ["spawninfo"] = HuskSpawnInfo,
                   ["collectibles"] = HuskCollection
               }
           );

            List<int> BCollection = new()
            {
            };

            bossChecklistMod.Call(
               "LogMiniBoss",
               Mod,
               "BoulderBeetle",
               5.81f,
               () => InfernusSystem.downedBoulderInvasionPHM,
               ModContent.NPCType<NPCs.Boulder_Beetle>(),
               new Dictionary<string, object>()
               {
                   ["spawnItems"] = ModContent.ItemType<ThickBoulder>(),
                   ["spawninfo"] = $"Mini-Boss of the Boulder Invasion. Spawns at 25% and 75% progress.",
                   ["collectibles"] = BCollection
               }
           );

            List<int> CCollection = new()
            {
                ModContent.ItemType<Items.Tools.Cursed_Soul>()
            };

            bossChecklistMod.Call(
               "LogMiniBoss",
               Mod,
               "CursedWanderer",
               5.1f,
               () => InfernusSystem.downedWanderer,
               ModContent.NPCType<NPCs.Cursed_Wanderer>(),
               new Dictionary<string, object>()
               {
                   ["spawninfo"] = $"Naturally spawns in the dungeon.",
                   ["collectibles"] = CCollection
               }
           );

            List<int> DCollection = new()
            {
                ModContent.ItemType<Items.Tools.Chorus_Bloom>()
            };

            bossChecklistMod.Call(
               "LogMiniBoss",
               Mod,
               "ChorusPlant",
               12.1f,
               () => InfernusSystem.downedFlower,
               ModContent.NPCType<NPCs.Chorus_Plant>(),
               new Dictionary<string, object>()
               {
                   ["spawninfo"] = $"Naturally spawns in the underground jungle",
                   ["collectibles"] = DCollection
               }
           );
        }
    }
}

