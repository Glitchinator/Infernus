using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Equite_Quiver : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 50;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Quiver_Equipped = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Quiver_Equipped = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Ranged) += 0.04f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Equite_Bar>(), 8)
            .AddIngredient(ModContent.ItemType<Quiver>(), 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
