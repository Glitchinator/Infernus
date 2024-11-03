using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Materials
{
    public class Ruderibus_Shard : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 22;
            Item.value = 4000;
            Item.rare = ItemRarityID.White;
            Item.maxStack = 999;
        }

    }
}
