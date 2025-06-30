using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Infernus.Biome
{
    internal class BiomeGen : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(t => t.Name.Equals("Shinies"));
            if (shiniesIndex != -1)
            {
                tasks.Insert(shiniesIndex + 1, new AerititeGen("Aeritite Ore Pass", 320f));
            }
            int Raiko_Summon_Tile = tasks.FindIndex(j => j.Name.Equals("Buried Chests"));
            if (Raiko_Summon_Tile != -1)
            {
                tasks.Insert(Raiko_Summon_Tile + 1, new Raiko_Tile_Gen("Raiko Tile Pass", 100f));
            }
        }
    }
}
