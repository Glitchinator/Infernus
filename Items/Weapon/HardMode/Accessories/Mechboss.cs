using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Mechboss : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 14, 25, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Mech_Equipped = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HallowedBar, 8)
            .AddIngredient(ItemID.SoulofFright, 4)
            .AddIngredient(ItemID.SoulofMight, 4)
            .AddIngredient(ItemID.SoulofSight, 4)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}