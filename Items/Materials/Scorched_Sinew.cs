using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Materials
{
    public class Scorched_Sinew : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 18;
            Item.value = 8000;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 999;
        }

    }
}
