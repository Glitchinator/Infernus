using Infernus.Items.Accesories;
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
            .AddIngredient(ModContent.ItemType<Placeable.Equite_Ore_Item>(), 3)
            .AddIngredient(ItemID.Bone, 1)
            .AddIngredient(ItemID.BeeWax, 1)
            .AddIngredient(ItemID.TissueSample, 1)
            .AddTile(TileID.Furnaces)
            .Register();

            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Placeable.Equite_Ore_Item>(), 3)
            .AddIngredient(ItemID.Bone, 1)
            .AddIngredient(ItemID.BeeWax, 1)
            .AddIngredient(ItemID.ShadowScale, 1)
            .AddTile(TileID.Furnaces)
            .Register();
        }
    }
}
