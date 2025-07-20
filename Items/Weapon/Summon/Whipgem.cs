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
        }

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<Projectiles.Whipgem>(), 22, 3, 4.7f, 27);
            Item.value = Item.buyPrice(0, 7, 50, 0);

            //Item.shootSpeed = 4;
            Item.rare = ItemRarityID.Green;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 6)
            .AddIngredient(ItemID.TissueSample, 8)
            .AddIngredient(ItemID.Sapphire, 2)
            .AddIngredient(ItemID.Emerald, 2)
            .AddIngredient(ItemID.Ruby, 2)
            .AddIngredient(ItemID.Amethyst, 2)
            .AddIngredient(ItemID.Topaz, 2)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.DemoniteBar, 6)
            .AddIngredient(ItemID.ShadowScale, 8)
            .AddIngredient(ItemID.Sapphire, 2)
            .AddIngredient(ItemID.Emerald, 2)
            .AddIngredient(ItemID.Ruby, 2)
            .AddIngredient(ItemID.Amethyst, 2)
            .AddIngredient(ItemID.Topaz, 2)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}