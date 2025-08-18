using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Igneous_Arrowhead : ModItem
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
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetArmorPenetration(DamageClass.Ranged) += 8;
            player.GetCritChance(DamageClass.Ranged) += 9;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Chiseled_Arrowhead>(), 1)
            .AddIngredient(ModContent.ItemType<Crumbling_Basalt>(), 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}