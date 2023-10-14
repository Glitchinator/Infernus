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
            DisplayName.SetDefault("Ice Scroll");
            Tooltip.SetDefault("Cold to the touch"
                + "\n 8% Reduced Mana Usage +40 Mana");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.accessory = true;
            Item.value = 60000;
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 40;
            player.manaCost -= .08f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.IceBlock, 26)
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ItemID.Cobweb, 8)
            .AddIngredient(ItemID.ManaCrystal, 2)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
