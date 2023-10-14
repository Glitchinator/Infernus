using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class Flower : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Budsnap");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.width = 34;
            Item.height = 30;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(0, 5, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<Projectiles.Leaf>();
            Item.shootSpeed = 10f;
            Item.autoReuse = true;
            Item.mana = 8;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.JungleSpores, 12)
            .AddIngredient(ItemID.Vine, 2)
            .AddIngredient(ItemID.RichMahogany, 6)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
