using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Cursed_Bayonett : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 34;
            Item.value = Item.buyPrice(0, 14, 25, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Cursed_Bayonett = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 -= 50;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Bayonett>(), 1)
            .AddIngredient(ItemID.SoulofNight, 4)
            .AddIngredient(ItemID.CobaltBar, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();

            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Bayonett>(), 1)
            .AddIngredient(ItemID.SoulofNight, 4)
            .AddIngredient(ItemID.PalladiumBar, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}