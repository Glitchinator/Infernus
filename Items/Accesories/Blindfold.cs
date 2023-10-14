using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Blindfold : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silky Sash");
            Tooltip.SetDefault("Decreases defense but exchanges it for offense.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 28;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Blue;
            Item.defense = -3;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += .09f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Silk, 12)
            .AddRecipeGroup(RecipeGroupID.IronBar, 4)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
