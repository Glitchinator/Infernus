using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Summon
{
    public class Whipgem : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Gem Tremor");
            Tooltip.SetDefault("Your minions will attack struck foes"
                + "\n + 5 summon tag damage");
        }

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<Projectiles.Whipgem>(), 22, 3, 7);
            Item.value = Item.buyPrice(0, 7, 50, 0);

            Item.shootSpeed = 4;
            Item.rare = ItemRarityID.Green;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Sapphire, 3)
            .AddIngredient(ItemID.Emerald, 3)
            .AddIngredient(ItemID.Ruby, 3)
            .AddIngredient(ItemID.Amethyst, 3)
            .AddIngredient(ItemID.Topaz, 3)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}