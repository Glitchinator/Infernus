using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Blood_Quiver : ModItem
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
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Soul_Drinker = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().NPC_Iced = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Quiver_Equipped = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Icy_Quiver>(), 1)
            .AddIngredient(ModContent.ItemType<Souldrinker>(), 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}