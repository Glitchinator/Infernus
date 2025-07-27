using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Iceicle_Necklace : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 34;
            Item.accessory = true;
            Item.value = 60000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().NPC_Iced = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Iceicle_Necklace = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Condensed_Iceicle>(), 1)
            .AddIngredient(ItemID.Silk, 12)
            .AddRecipeGroup(RecipeGroupID.IronBar, 4)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
