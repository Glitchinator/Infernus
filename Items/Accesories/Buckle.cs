using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Buckle : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chipped Whetstone");
            Tooltip.SetDefault("Weapon armor penetration increased.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetArmorPenetration(DamageClass.Generic) += 4;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.StoneBlock, 16)
            .AddTile(TileID.Anvils)
            .AddCondition(Recipe.Condition.NearWater)
            .Register();
        }
    }
}
