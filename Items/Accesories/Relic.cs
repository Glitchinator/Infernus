using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Relic : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 32;
            Item.accessory = true;
            Item.value = 53000;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 6;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 1;
            player.statLifeMax2 += 20;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Tree_Band>(), 1)
            .AddIngredient(ModContent.ItemType<Stone_Emblem>(), 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
