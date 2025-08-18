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
            Item.width = 28;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 14, 25, 0);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Cursed_Bayonett = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 -= 80;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Bayonett>(), 8)
            .AddIngredient(ItemID.SoulofNight, 4)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}