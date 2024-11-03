using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Materials
{
    public class Crumbling_Basalt : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 14;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightPurple;
            Item.maxStack = 999;
        }

    }
}
