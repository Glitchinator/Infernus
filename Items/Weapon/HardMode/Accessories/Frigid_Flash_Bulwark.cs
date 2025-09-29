using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using rail;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Frigid_Flash_Bulwark : ModItem
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
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
            Item.defense = 14;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Ice_Banner = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Enchanted_Femur = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Manic_Flash = true;
            player.starCloakItem = Item;
            player.starCloakItem_manaCloakOverrideItem = Item;
            player.magicCuffs = true;
            player.statManaMax2 += 40;
            player.starCloakItem = Item;
            player.starCloakItem_manaCloakOverrideItem = Item;
            player.manaMagnet = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Glacial_Shield>(), 1)
            .AddIngredient(ModContent.ItemType<Manic_Flash>(), 1)
            .AddIngredient(ItemID.BeetleHusk, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}