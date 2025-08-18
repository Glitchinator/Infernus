using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Magic_Glyph : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 40;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statMana > (int)(player.statManaMax * 0.7f))
            {
                player.manaRegen += 8;
                return;
            }
            if (player.statMana > (int)(player.statManaMax * 0.5f))
            {
                player.manaRegen += 4;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.DemoniteBar, 4)
            .AddIngredient(ModContent.ItemType<Gaming>(), 12)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 4)
            .AddIngredient(ModContent.ItemType<Gaming>(), 12)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
