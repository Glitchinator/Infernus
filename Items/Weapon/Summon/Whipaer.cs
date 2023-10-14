using Infernus.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Summon
{
    public class Whipaer : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Aeritite Whip");
            Tooltip.SetDefault("Your minions will attack struck foes"
                + "\n + 4 summon tag damage");
        }

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<Whip>(), 16, 3, 4);
            Item.value = Item.buyPrice(0, 4, 50, 0);

            Item.shootSpeed = 3;
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Gaming>(), 5)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}