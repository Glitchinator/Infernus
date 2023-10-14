using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class LunarPole : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Pole");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.CanFishInLava[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 42;
            Item.value = 100000;
            Item.CloneDefaults(ItemID.SittingDucksFishingRod);
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ProjectileID.BobberFiberglass;
            Item.fishingPole = 55;
            Item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.LunarBar, 8)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
    }
}
