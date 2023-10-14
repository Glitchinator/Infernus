using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Summon
{
    public class Whipice : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Iceicle Snap");
            Tooltip.SetDefault("Your minions will attack struck foes"
                                + "\n + Frostburn"
                                + "\n + 7 summon tag damage");
        }

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<Projectiles.Whipice>(), 32, 3, 7);
            Item.value = Item.buyPrice(0, 10, 50, 0);

            Item.shootSpeed = 5;
            Item.rare = ItemRarityID.Orange;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.IceSpikes>(), 25)
            .AddIngredient(ItemID.IceBlock, 36)
            .AddIngredient(ModContent.ItemType<Whipaer>(), 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}