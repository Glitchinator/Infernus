using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Infernus.Tiles
{
    public class ChiseledRock : ModTile
    {
        public override bool CanExplode(int i, int j)
        {
            return true;
        }
        public override void SetStaticDefaults()
        {

            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(207, 196, 162), Language.GetText("Chiseled Rock"));

            DustType = DustID.Stone;
        }
    }
}