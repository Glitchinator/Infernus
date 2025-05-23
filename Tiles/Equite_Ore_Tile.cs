﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Infernus.Tiles
{
    public class Equite_Ore_Tile : ModTile
    {
        public override bool CanExplode(int i, int j)
        {
            return true;
        }
        public override void SetStaticDefaults()
        {

            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
            Main.tileOreFinderPriority[Type] = 330; // Metal Detector value, see https://terraria.wiki.gg/wiki/Metal_Detector
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 800; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            MinPick = 55;
            MineResist = 0.8f;

            AddMapEntry(new Color(229, 214, 127), Language.GetText("Equite Ore"));

            HitSound = SoundID.DD2_CrystalCartImpact;
            DustType = DustID.SpelunkerGlowstickSparkle;
        }
    }
}