using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ChestPLate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Aeritite Breastplate");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 20;
            Item.value = 7500;
            Item.rare = ItemRarityID.Green;
            Item.defense = 6;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Gaming>(), 30)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}