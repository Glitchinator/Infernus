using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace Infernus.Biome
{
    internal class AerititeGen : GenPass
    {
        public AerititeGen(string name, float weight) : base(name, weight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Aeritite ore is being added to your amazing world";

            for (int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 4E-05); i++)
            {
                WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(Main.maxTilesY - 260, Main.maxTilesY - 200), WorldGen.genRand.Next(12, 17), WorldGen.genRand.Next(12, 17), ModContent.TileType<Tiles.Ore>());
            }
        }
    }
}
