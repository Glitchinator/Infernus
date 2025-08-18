using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Infernus
{
    public class InfernusSystem : ModSystem
    {
        public static bool downedRaiko = false;
        public static bool downedRuderibus = false;
        public static bool downedTigerShark = false;
        public static bool downedSquid = false;
        public static bool downedBoulderInvasionPHM = false;
        public static bool downedBoulderInvasionHM = false;
        public static bool downedBoulderBoss = false;
        public static bool downedCalypsical = false;
        public static bool Level_systemON = false;
        public static bool Equite_Generated = false;
        public static bool downedWanderer = false;
        public static bool downedFlower = false;

        public override void OnWorldLoad()
        {
            downedRaiko = false;
            downedRuderibus = false;
            downedTigerShark = false;
            downedSquid = false;
            downedBoulderInvasionPHM = false;
            downedBoulderInvasionHM = false;
            downedBoulderBoss = false;
            downedCalypsical = false;
            Level_systemON = false;
            Equite_Generated = false;
            downedWanderer = false;
            downedFlower = false;
        }

        public override void OnWorldUnload()
        {
            downedRaiko = false;
            downedRuderibus = false;
            downedTigerShark = false;
            downedSquid = false;
            downedBoulderInvasionPHM = false;
            downedBoulderInvasionHM = false;
            downedBoulderBoss = false;
            downedCalypsical = false;
            Level_systemON = false;
            Equite_Generated = false;
            downedWanderer = false;
            downedFlower = false;
        }
        public override void SaveWorldData(TagCompound tag)
        {
            if (downedRaiko)
            {
                tag["downedRaiko"] = true;
            }
            if (downedRuderibus)
            {
                tag["downedRuderibus"] = true;
            }
            if (downedTigerShark)
            {
                tag["downedTigerShark"] = true;
            }
            if (downedSquid)
            {
                tag["downedSquid"] = true;
            }
            if (downedBoulderInvasionHM)
            {
                tag["downedHM"] = true;
            }
            if (downedBoulderInvasionPHM)
            {
                tag["downedPHM"] = true;
            }
            if (downedCalypsical)
            {
                tag["downedMech"] = true;
            }
            if (downedBoulderBoss)
            {
                tag["downedboulder"] = true;
            }
            if (Level_systemON)
            {
                tag["LevelON"] = true;
            }
            if (Equite_Generated)
            {
                tag["EquiteGenerated"] = true;
            }
            if(downedWanderer)
            {
                tag["downedWanderer"] = true;
            }
            if(downedFlower)
            {
                tag["downedFlower"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedRaiko = tag.ContainsKey("downedRaiko");
            downedRuderibus = tag.ContainsKey("downedRuderibus");
            downedTigerShark = tag.ContainsKey("downedTigerShark");
            downedSquid = tag.ContainsKey("downedSquid");
            downedBoulderInvasionHM = tag.ContainsKey("downedHM");
            downedBoulderInvasionPHM = tag.ContainsKey("downedPHM");
            downedBoulderBoss = tag.ContainsKey("downedboulder");
            downedCalypsical = tag.ContainsKey("downedMech");
            Level_systemON = tag.ContainsKey("LevelON");
            Equite_Generated = tag.ContainsKey("EquiteGenerated");
            downedWanderer = tag.ContainsKey("downedWanderer");
            downedFlower = tag.ContainsKey("downedFlower");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedRaiko;
            flags[1] = downedRuderibus;
            flags[2] = downedTigerShark;
            flags[3] = downedSquid;
            flags[4] = downedBoulderInvasionPHM;
            flags[5] = downedBoulderInvasionHM;
            flags[6] = downedCalypsical;
            flags[7] = Level_systemON;
            flags[8] = downedBoulderBoss;
            flags[9] = Equite_Generated;
            flags[10] = downedWanderer;
            flags[11] = downedFlower;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedRaiko = flags[0];
            downedRuderibus = flags[1];
            downedTigerShark = flags[2];
            downedSquid = flags[3];
            downedBoulderInvasionPHM = flags[4];
            downedBoulderInvasionHM = flags[5];
            downedCalypsical = flags[6];
            Level_systemON = flags[7];
            downedBoulderBoss = flags[8];
            Equite_Generated = flags[9];
            downedWanderer = flags[10];
            downedFlower = flags[11];
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.TryGetIngredient(ItemID.Ruby, out Item ingredient2) && recipe.TryGetIngredient(ItemID.Robe, out Item ingredient3))
                {
                    recipe.RemoveIngredient(ItemID.Ruby);
                    recipe.AddIngredient(ItemID.Ruby, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Emerald, out Item ingredient) && recipe.TryGetIngredient(ItemID.Robe, out Item ingredient4))
                {
                    recipe.RemoveIngredient(ItemID.Emerald);
                    recipe.AddIngredient(ItemID.Emerald, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Sapphire, out Item ingredient5) && recipe.TryGetIngredient(ItemID.Robe, out Item ingredient6))
                {
                    recipe.RemoveIngredient(ItemID.Sapphire);
                    recipe.AddIngredient(ItemID.Sapphire, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Topaz, out Item ingredient7) && recipe.TryGetIngredient(ItemID.Robe, out Item ingredient8))
                {
                    recipe.RemoveIngredient(ItemID.Topaz);
                    recipe.AddIngredient(ItemID.Topaz, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Amethyst, out Item ingredient1) && recipe.TryGetIngredient(ItemID.Robe, out Item ingredient9))
                {
                    recipe.RemoveIngredient(ItemID.Amethyst);
                    recipe.AddIngredient(ItemID.Amethyst, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Amber, out Item ingredient11) && recipe.TryGetIngredient(ItemID.Robe, out Item ingredient12))
                {
                    recipe.RemoveIngredient(ItemID.Amber);
                    recipe.AddIngredient(ItemID.Amber, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Diamond, out Item ingredient13) && recipe.TryGetIngredient(ItemID.Robe, out Item ingredient14))
                {
                    recipe.RemoveIngredient(ItemID.Diamond);
                    recipe.AddIngredient(ItemID.Diamond, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Ruby, out Item ingredient21) && recipe.TryGetIngredient(ItemID.GoldBar, out Item ingredient31))
                {
                    recipe.RemoveIngredient(ItemID.Ruby);
                    recipe.AddIngredient(ItemID.Ruby, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Emerald, out Item ingredient111) && recipe.TryGetIngredient(ItemID.TungstenBar, out Item ingredient4111))
                {
                    recipe.RemoveIngredient(ItemID.Emerald);
                    recipe.AddIngredient(ItemID.Emerald, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Sapphire, out Item ingredient5111) && recipe.TryGetIngredient(ItemID.SilverBar, out Item ingredient611))
                {
                    recipe.RemoveIngredient(ItemID.Sapphire);
                    recipe.AddIngredient(ItemID.Sapphire, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Topaz, out Item ingredient7111) && recipe.TryGetIngredient(ItemID.TinBar, out Item ingredient81))
                {
                    recipe.RemoveIngredient(ItemID.Topaz);
                    recipe.AddIngredient(ItemID.Topaz, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Amethyst, out Item ingredient1111) && recipe.TryGetIngredient(ItemID.CopperBar, out Item ingredient91))
                {
                    recipe.RemoveIngredient(ItemID.Amethyst);
                    recipe.AddIngredient(ItemID.Amethyst, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Amber, out Item ingredient11111) && recipe.TryGetIngredient(ItemID.FossilOre, out Item ingredient121))
                {
                    recipe.RemoveIngredient(ItemID.Amber);
                    recipe.AddIngredient(ItemID.Amber, 3);
                }
                if (recipe.TryGetIngredient(ItemID.Diamond, out Item ingredient131) && recipe.TryGetIngredient(ItemID.PlatinumBar, out Item ingredient141))
                {
                    recipe.RemoveIngredient(ItemID.Diamond);
                    recipe.AddIngredient(ItemID.Diamond, 3);
                }
            }
        }
    }
}