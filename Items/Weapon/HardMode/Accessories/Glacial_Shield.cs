using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using rail;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Glacial_Shield : ModItem
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
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
            Item.defense = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.manaCost -= 0.12f;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Ice_Banner = true;
            player.magicCuffs = true;
            player.statManaMax2 += 40;
            player.manaMagnet = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Ice_Banner>(), 1)
            .AddIngredient(ItemID.CelestialCuffs, 1)
            .AddIngredient(ItemID.SoulofNight, 8)
            .AddIngredient(ItemID.FrostCore, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}