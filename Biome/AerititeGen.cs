using Terraria;
using Terraria.IO;
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

            for (int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-04); i++)
            {
                int x = WorldGen.genRand.Next(100, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(2, 6), TileType<Tiles.Ore>());
            }
        }
    }
}
