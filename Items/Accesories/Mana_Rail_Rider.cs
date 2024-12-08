using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Mana_Rail_Rider : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.accessory = true;
            Item.value = 60000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.mount.Active)
            {
                player.statManaMax2 += 45;
                player.manaRegen += 1;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Condensed_Iceicle>(), 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
