using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Dusk_Sigil : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 22;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 -= 50;
            player.GetDamage(DamageClass.Magic) += 0.12f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.DemoniteBar, 8)
            .AddIngredient(ItemID.ShadowScale, 12)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 8)
            .AddIngredient(ItemID.TissueSample, 12)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
