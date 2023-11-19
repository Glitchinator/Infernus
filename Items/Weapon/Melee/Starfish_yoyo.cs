using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Melee
{
    public class Starfish_yoyo : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Melee;
            Item.width = 28;
            Item.height = 26;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.5f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 165000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.StarFish_Yoyo>();
            Item.shootSpeed = 10f;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Starfish, 6)
            .AddIngredient(ItemID.WoodYoyo, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}