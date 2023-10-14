using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class SkullStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skull Staff");
            Tooltip.SetDefault("Conjures bouncin bones");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Magic;
            Item.width = 48;
            Item.height = 48;
            Item.useAnimation = 38;
            Item.useTime = 38;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 6, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Bone>();
            Item.shootSpeed = 13f;
            Item.mana = 10;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Bone, 32)
            .AddIngredient(ItemID.Silk, 8)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}