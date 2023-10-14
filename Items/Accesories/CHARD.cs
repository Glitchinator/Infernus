using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class CHARD : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sentinel Battery");
            Tooltip.SetDefault("+12% Summon damage + 1 Minion");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.accessory = true;
            Item.value = 85000;
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += .12f;
            player.maxMinions++;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Charge>(), 1)
            .AddIngredient(ModContent.ItemType<Materials.Hot>(), 7)
            .AddIngredient(ModContent.ItemType<Materials.IceSpikes>(), 7)
            .AddIngredient(ItemID.HellstoneBar, 10)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
