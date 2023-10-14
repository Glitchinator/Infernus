using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Melee
{
    public class Swingy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoldered Sword");
            Tooltip.SetDefault("Goodbye space sword");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 54;
            Item.useTime = 7;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(0, 8, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Swingy>();
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 40f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Hot>(), 24)
            .AddIngredient(ItemID.WoodenSword, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}