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

        public int Boulder_Cooldown = 43200; // 12 mins cooldown for boulder invasion

        public int Stress_Enemy_There = 30; // stress update tick


        public int Last_Check_Arms = 30;

        public int Last_Plant_Spawn = 10800;

        public int Last_Cursed_Spawn = 10800;

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

            if (InfernusNPC.Is_Spawned == true && Stress_Enemy_There > 0)
            {
                Stress_Enemy_There--;
            }
            if (Stress_Enemy_There == 0)
            {
                InfernusNPC.Is_Spawned = false;
                Stress_Enemy_There = 30;
            }
            if (InfernusNPC.Arms_Alive == true && Last_Check_Arms > 0)
            {
                Last_Check_Arms--;
            }
            if (Last_Check_Arms == 0)
            {
                InfernusNPC.Arms_Alive = false;
                Last_Check_Arms = 30;
            }
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
            if (Main.dayTime == true && Boulder_Cooldown > 0)
            {
                Boulder_Cooldown--;
            }
            if (Boulder_Cooldown <= 0 && Main.rand.NextBool(18000) && BoulderInvasionUp == false && NPC.downedBoss3 == true && InfernusNPC.Is_Spawned != true)
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
                Boulder_Cooldown = 43200; // 12 mins reset
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
            if (bossChecklistMod.Version < new Version(1, 3, 1))
            {
                return;
            }
            string bossName = "Raiko";

            int bossType = ModContent.NPCType<NPCs.Raiko>();

            float weight = 3.5f;

            Func<bool> downed = () => InfernusSystem.downedRaiko;

            Func<bool> available = () => true;

            List<int> collection = new()
            {
                ModContent.ItemType<Placeable.RaikoRelic>(),
                ModContent.ItemType<Items.Pets.RaikoPetItem>(),
                ModContent.ItemType<Placeable.Trophy>(),
                ModContent.ItemType<Items.Weapon.Meteor>(),
                ModContent.ItemType<Items.Weapon.Ranged.Firebow>(),
                ModContent.ItemType<Items.Weapon.Magic.MeteorEater>(),
                ModContent.ItemType<Items.Weapon.Melee.BoldnBash>(),
                ModContent.ItemType<Items.Weapon.Summon.Minion>(),
                ModContent.ItemType<Items.Tools.Day>()
            };

            int summonItem = ModContent.ItemType<Items.BossSummon.Boss1sum>();

            string spawnInfo = $"Use a [i:{summonItem}] during night.";

            string despawnInfo = null;

            var customBossPortrait = (SpriteBatch sb, Rectangle rect, Color color) =>
            {
                Texture2D texture = ModContent.Request<Texture2D>("Infernus/BossChecklist/Raiko").Value;
                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
                "AddBoss",
                Mod,
                bossName,
                bossType,
                weight,
                downed,
                available,
                collection,
                summonItem,
                spawnInfo,
                despawnInfo,
                customBossPortrait
            );


            string bossName1 = "Ruderibus";

            int bossType1 = ModContent.NPCType<NPCs.Ruderibus>();

            float weight1 = 6.9f;

            Func<bool> downed1 = () => InfernusSystem.downedRuderibus;

            Func<bool> available1 = () => true;

            List<int> collection1 = new()
            {
                ModContent.ItemType<Placeable.RudeRelic>(),
                ModContent.ItemType<Items.Pets.RudeItem>(),
                ModContent.ItemType<Placeable.RudeTrophy>(),
                ModContent.ItemType<Items.Materials.IceSpikes>(),
                ItemID.FlurryBoots,
                ItemID.IceMirror,
                ItemID.IceBoomerang,
                ItemID.IceBlade,
                ItemID.BlizzardinaBottle,
                ItemID.SnowballCannon,
                ItemID.IceSkates,
                ItemID.IceMachine,
                ItemID.Fish,
            };

            int summonItem1 = ModContent.ItemType<Items.BossSummon.BossSummon>();

            string spawnInfo1 = $"While in the snow biome, use [i:{summonItem1}].";

            string despawnInfo1 = null;

            var customBossPortrait1 = (SpriteBatch sb, Rectangle rect, Color color) =>
            {
                Texture2D texture = ModContent.Request<Texture2D>("Infernus/BossChecklist/Ruderibus").Value;
                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
                "AddBoss",
                Mod,
                bossName1,
                bossType1,
                weight1,
                downed1,
                available1,
                collection1,
                summonItem1,
                spawnInfo1,
                despawnInfo1,
                customBossPortrait1
            );



            string bossName2 = "Serphious";

            int bossType2 = ModContent.NPCType<NPCs.Shark>();

            float weight2 = 16.9f;

            Func<bool> downed2 = () => InfernusSystem.downedTigerShark;

            Func<bool> available2 = () => true;

            List<int> collection2 = new()
            {
                ModContent.ItemType<Placeable.SharkRelic>(),
                ModContent.ItemType<Placeable.SharkTrophy>(),
                ModContent.ItemType<Items.Weapon.HardMode.Accessories.eleScale>(),
                ModContent.ItemType<Items.Weapon.HardMode.Melee.Electricice>(),
                ModContent.ItemType<Items.Weapon.HardMode.Ranged.Electricbow>(),
                ModContent.ItemType<Items.Weapon.HardMode.Magic.Lightning>(),
                ModContent.ItemType<Items.Weapon.HardMode.Summon.whiplight>(),
                ItemID.JellyfishDivingGear,
                ItemID.WaterWalkingBoots,
                ItemID.SharkFin,
                ItemID.FloatingTube
            };

            int summonItem2 = ModContent.ItemType<Items.BossSummon.BeetleBait>();

            string spawnInfo2 = $"Use a [i:{summonItem2}]";

            string despawnInfo2 = null;

            var customBossPortrait2 = (SpriteBatch sb, Rectangle rect, Color color) =>
            {
                Texture2D texture = ModContent.Request<Texture2D>("Infernus/BossChecklist/TigerShark").Value;
                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
                "AddBoss",
                Mod,
                bossName2,
                bossType2,
                weight2,
                downed2,
                available2,
                collection2,
                summonItem2,
                spawnInfo2,
                despawnInfo2,
                customBossPortrait2
            );

            string minibossName = "Temporal Glow Squid";

            int minibossType = ModContent.NPCType<NPCs.TemporalSquid>();

            float miniweight = 1.9f;

            Func<bool> minidowned = () => InfernusSystem.downedSquid;

            Func<bool> miniavailable = () => true;

            List<int> minicollection = new()
            {
                ModContent.ItemType<Items.Consumable.Potion>()
            };

            int minisummonItem = ModContent.ItemType<Items.BossSummon.Squid_BossSummon>();

            string minispawnInfo = $"While in the ocean biome use [i:{minisummonItem}] no matter the time.";

            string minidespawnInfo = null;

            bossChecklistMod.Call(
                "AddBoss",
                Mod,
                minibossName,
                minibossType,
                miniweight,
                minidowned,
                miniavailable,
                minicollection,
                minisummonItem,
                minispawnInfo,
                minidespawnInfo
            );
            string EventName = "Boulder Invasion Pre-HM";

            int EventType = ModContent.NPCType<NPCs.Boulder_Corpse>();

            float Eventweight = 5.8f;

            Func<bool> Eventdowned = () => InfernusSystem.downedBoulderInvasionPHM;

            Func<bool> Eventavailable = () => true;

            List<int> Eventcollection = new()
            {
                ModContent.ItemType<Items.Weapon.Ranged.July4th>()
            };

            int EventsummonItem = ModContent.ItemType<ThickBoulder>();

            string EventspawnInfo = $"Spawns naturally when daytime or, use a [i:{EventsummonItem}] during day.";

            string EventdespawnInfo = null;

            var EventcustomBossPortrait = (SpriteBatch sb, Rectangle rect, Color color) =>
            {
                Texture2D texture = ModContent.Request<Texture2D>("Infernus/BossChecklist/BIPHM").Value;
                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
                "AddBoss",
                Mod,
                EventName,
                EventType,
                Eventweight,
                Eventdowned,
                Eventavailable,
                Eventcollection,
                EventsummonItem,
                EventspawnInfo,
                EventdespawnInfo,
                EventcustomBossPortrait
            );

            string EventName2 = "Boulder Invasion HM";

            int EventType2 = ModContent.NPCType<NPCs.Boulder_Corpse>();

            float Eventweight2 = 12.4f;

            Func<bool> Eventdowned2 = () => InfernusSystem.downedBoulderInvasionHM;

            Func<bool> Eventavailable2 = () => true;

            List<int> Eventcollection2 = new()
            {
                ModContent.ItemType<Items.Weapon.HardMode.Ranged.Bog>(),
                ModContent.ItemType<Items.Weapon.HardMode.Summon.Whiprock>(),
                ModContent.ItemType<Items.Weapon.HardMode.Melee.bould>(),
                ModContent.ItemType<Items.Weapon.HardMode.Magic.Bouldermagicweapon>(),
                ModContent.ItemType<Items.Weapon.HardMode.Accessories.Wings>(),
                ModContent.ItemType<Items.Weapon.HardMode.Accessories.HiveHeart>()
            };

            int EventsummonItem2 = ModContent.ItemType<ThickBoulder>();

            string EventspawnInfo2 = $"Spawns naturally when daytime or, use a [i:{EventsummonItem2}] during day.";

            string EventdespawnInfo2 = null;

            var EventcustomBossPortrait2 = (SpriteBatch sb, Rectangle rect, Color color) =>
            {
                Texture2D texture = ModContent.Request<Texture2D>("Infernus/BossChecklist/BIHM").Value;
                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
                "AddBoss",
                Mod,
                EventName2,
                EventType2,
                Eventweight2,
                Eventdowned2,
                Eventavailable2,
                Eventcollection2,
                EventsummonItem2,
                EventspawnInfo2,
                EventdespawnInfo2,
                EventcustomBossPortrait2
            );

            string bossName3 = "Calypsical";

            int bossType3 = ModContent.NPCType<NPCs.Calypsical>();

            float weight3 = 19.6f;

            Func<bool> downed3 = () => InfernusSystem.downedCalypsical;

            Func<bool> available3 = () => true;

            List<int> collection3 = new()
            {
                ModContent.ItemType<Items.Weapon.HardMode.Ranged.miniholy>(),
                ModContent.ItemType<Items.Weapon.HardMode.Summon.Mecharmr>(),
                ModContent.ItemType<Items.Weapon.HardMode.Summon.MechWhip>(),
                ModContent.ItemType<Items.Weapon.HardMode.Melee.HolyRam>(),
                ModContent.ItemType<Items.Weapon.HardMode.Magic.Cyclone>(),
                ModContent.ItemType<Items.Weapon.HardMode.Accessories.Mechwings>()
            };

            int summonItem3 = ModContent.ItemType<Items.BossSummon.Mechsummon>();

            string spawnInfo3 = $"Use a [i:{summonItem3}] after Moonlord's defeat";

            string despawnInfo3 = null;

            bossChecklistMod.Call(
                "AddBoss",
                Mod,
                bossName3,
                bossType3,
                weight3,
                downed3,
                available3,
                collection3,
                summonItem3,
                spawnInfo3,
                despawnInfo3
            );


            string bossName4 = "Corrupted Husk";

            int bossType4 = ModContent.NPCType<NPCs.Boulderminiboss>();

            float weight4 = 12.2f;

            Func<bool> downed4 = () => InfernusSystem.downedBoulderBoss;

            Func<bool> available4 = () => true;

            List<int> collection4 = new()
            {
                ModContent.ItemType<Items.Weapon.HardMode.Ranged.Bog>(),
                ModContent.ItemType<Items.Weapon.HardMode.Summon.Whiprock>(),
                ModContent.ItemType<Items.Weapon.HardMode.Melee.bould>(),
                ModContent.ItemType<Items.Weapon.HardMode.Magic.BoulderTomb>(),
                 ModContent.ItemType<Items.Weapon.HardMode.Magic.Bouldermagicweapon>(),
                ModContent.ItemType<Items.Weapon.HardMode.Accessories.HiveHeart>(),
                 ModContent.ItemType<Items.Weapon.HardMode.Accessories.Wings>()
            };

            int summonItem4 = ModContent.ItemType<ThickBoulder>();

            string spawnInfo4 = $"Use a [i:{summonItem4}] to spawn the Boulder Invasion. At the end of the Hardmode Boulder Invasion. This boss will spawn.";

            string despawnInfo4 = null;

            bossChecklistMod.Call(
                "AddBoss",
                Mod,
                bossName4,
                bossType4,
                weight4,
                downed4,
                available4,
                collection4,
                summonItem4,
                spawnInfo4,
                despawnInfo4
            );
        }
    }
}

