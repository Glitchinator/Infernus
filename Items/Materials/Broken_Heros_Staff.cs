using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Materials
{
    public class Broken_Heros_Staff : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 8;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 38;
            Item.value = 700000;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
        }
    }
}