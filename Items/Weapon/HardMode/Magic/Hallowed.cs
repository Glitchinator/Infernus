using Infernus.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Magic
{
    public class Hallowed : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exgnir");
            Tooltip.SetDefault("Shoots homing shots");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Magic;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.value = 185000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item8;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Homing>();
            Item.shootSpeed = 17f;
            Item.mana = 14;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HallowedBar, 16)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}