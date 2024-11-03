using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Lacerator_Whiphead : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 48;
            Item.value = Item.buyPrice(0, 24, 50, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Basalt_Whiphead = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Defiled_Whiphead = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Ancient_Whiphead = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Defiled_Whiphead>(), 1)
            .AddIngredient(ModContent.ItemType<Basalt_Whiphead>(), 1)
            .AddIngredient(ItemID.BeetleHusk, 1)

            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}