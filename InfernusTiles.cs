using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus
{
    public class InfernusTiles : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!noItem && type == TileID.LeafBlock)
            {
                if (Main.rand.NextBool(70))
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Accesories.Tree_Branch>());
                }
            }
            if (!noItem && type == TileID.Chlorophyte)
            {
                if (Main.rand.NextBool(50))
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Weapon.HardMode.Accessories.Chlorophyte_Growth>());
                }
            }
        }
    }
}
