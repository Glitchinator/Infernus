using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Equalibrium : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 24;
            Item.value = Item.buyPrice(0, 14, 25, 0);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Compound_Uplift = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Cursed_Bayonett = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Cursed_Bayonett>(), 1)
            .AddIngredient(ModContent.ItemType<Compound_Kit>(), 1)
            .AddIngredient(ItemID.Ectoplasm, 14)
            .AddIngredient(ItemID.SpookyWood, 40)
            .AddIngredient(ModContent.ItemType<Crumbling_Basalt>(), 16)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}