using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Infernus.Tiles
{
    public class Ore : ModTile
    {
        public override bool CanExplode(int i, int j)
        {
            return true;
        }
        public override void SetStaticDefaults()
        {

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileShine2[Type] = true;

            ItemDrop = ItemType<Placeable.Ore>();

            AddMapEntry(new Color(121, 153, 163), Language.GetText("Aeritite Ore"));

            DustType = DustID.BubbleBurst_White;
        }
    }
}