using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class Coralstaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.width = 34;
            Item.height = 34;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 1, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Sea_Star_Lite>();
            Item.shootSpeed = 17f;
            Item.mana = 8;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Starfish, 14)
            .AddIngredient(ItemID.ShellPileBlock, 28)
            .AddIngredient(ItemID.FallenStar, 3)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}