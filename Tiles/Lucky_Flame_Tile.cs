using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Infernus.Tiles
{
    public class Lucky_Flame_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {

            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(207, 196, 162), Language.GetText("Lucky Flame"));

            DustType = DustID.Torch;
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Dust dust = Dust.NewDustDirect(new Vector2(i * 16 + 2, j * 16 - 4), 4, 8, DustID.Smoke, 0f, 0f, 100);
            //dust.position.X -= Main.rand.Next(8);

            dust.alpha += Main.rand.Next(100);
            dust.velocity *= 0.2f;
            dust.velocity.Y -= 0.5f + Main.rand.Next(10) * 0.1f;
            dust.fadeIn = 0.5f + Main.rand.Next(10) * 0.1f;
            // HasCampfire is a gameplay effect, so we don't run the code if closer is true.
            Player player = Main.LocalPlayer;
            if (Main.tile[i, j].TileFrameY < 36)
            {
                player.AddBuff(ModContent.BuffType<Buffs.Lucky_Flame>(), 3);
                player.buffTime[player.FindBuffIndex(ModContent.BuffType<Buffs.Lucky_Flame>())] = 3;
            }
            if (closer)
            {
                return;
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameY < 36)
            {
                float pulse = Main.rand.Next(28, 42) * 0.005f;
                pulse += (270 - Main.mouseTextColor) / 700f;
                r = 0.1f + pulse;
                g = 0.9f + pulse;
                b = 0.3f + pulse;
            }
        }
    }
}