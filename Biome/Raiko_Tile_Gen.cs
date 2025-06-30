using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace Infernus.Biome
{
    internal class Raiko_Tile_Gen : GenPass
    {
        public Raiko_Tile_Gen(string name, float weight) : base(name, weight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "A meteor alter is being placed somewhere on the surface";

            // find surface tile
            int x = WorldGen.genRand.Next(Main.maxTilesX / 4, Main.maxTilesX / 2);
            bool foundSurface = false;
            int y = 1;
            while (y < GenVars.worldSurfaceHigh)
            {
                if (WorldGen.SolidTile(x, y))
                {
                    foundSurface = true;
                    break;
                }
                y++;
            }
            Point point = new Point(x, y);
            //WorldGen.Place3x4(x, y, (ushort)ModContent.TileType<Tiles.Raiko_Summon_Tile>(), -1);
            WorldUtils.Gen(point, new Shapes.Circle(8, 8), new Actions.SetTile(TileID.Ash));
            WorldUtils.Gen(new Point(x, y - 12), new Shapes.Circle(4, 4), new Actions.SetTile(TileID.Ash));
            //WorldUtils.Gen(new Point(x, y - 12), new Shapes.Rectangle(3, 4), new Actions.SetTile((ushort)ModContent.TileType<Tiles.Raiko_Summon_Tile>()));\
            //WorldUtils.Gen(new Point(x, y - 17), new Shapes.Rectangle(1, 1), new Actions.SetTile(TileID.EmeraldGemspark));
            //WorldGen.Place1x1(x, y - 17, TileID.SapphireGemspark);
            WorldGen.Place3x4(x, y - 17, (ushort)ModContent.TileType<Tiles.Raiko_Summon_Tile>(), style: 0);
            //WorldGen.Place3x4(x, y, (ushort)ModContent.TileType<Tiles.Raiko_Summon_Tile>(), -1);
            //WorldGen.PlaceChest(x - 2, y - 16,type: 21, style: 10);
        }
    }
}
