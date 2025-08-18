using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Scroll : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.accessory = true;
            Item.value = 60000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Ice_Scroll = true;
            player.manaCost -= .08f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.IceBlock, 50)
            .AddIngredient(ItemID.Silk, 12)
            .AddIngredient(ItemID.TissueSample, 4)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.IceBlock, 50)
            .AddIngredient(ItemID.Silk, 12)
            .AddIngredient(ItemID.ShadowScale, 4)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
