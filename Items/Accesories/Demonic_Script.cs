using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Demonic_Script : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 34;
            Item.accessory = true;
            Item.value = 60000;
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.manaSickReduction *= 0.5f;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Demonic_Script = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HellstoneBar, 8)
            .AddIngredient(ItemID.Silk, 18)
            .AddTile(TileID.Furnaces)
            .Register();
        }
    }
}
