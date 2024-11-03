using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Icy_Quiver : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 52;
            Item.accessory = true;
            Item.value = 60000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().NPC_Iced = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Quiver_Equipped = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Condensed_Iceicle>(), 1)
            .AddIngredient(ModContent.ItemType<Quiver>(), 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
