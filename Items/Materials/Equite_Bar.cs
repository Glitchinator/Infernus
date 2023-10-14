using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Materials
{
    public class Equite_Bar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Equite Bar");
            Tooltip.SetDefault("It's not moldy cheese");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 999;
            Item.value = 18000;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Hot>(), 2)
            .AddIngredient(ModContent.ItemType<IceSpikes>(), 2)
            .AddTile(TileID.Furnaces)
            .Register();
        }
    }
}
