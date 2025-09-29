using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using rail;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Manic_Flash : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 50;
            Item.value = Item.buyPrice(0, 24, 50, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Manic_Flash = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Enchanted_Femur = true;
            player.starCloakItem = Item;
            player.starCloakItem_manaCloakOverrideItem = Item;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Enchanted_Femur>(), 1)
            .AddIngredient(ItemID.StarCloak, 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}