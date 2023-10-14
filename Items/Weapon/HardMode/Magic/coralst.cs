using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Magic
{
    public class coralst : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starfish Stalker");
            Tooltip.SetDefault("Casts a starfish that locks onto enemies"
                + "\n Ignores a substantial amount of enemy defense");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 46;
            Item.DamageType = DamageClass.Magic;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 185000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item82;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Sea_Star>();
            Item.autoReuse = true;
            Item.shootSpeed = 26f;
            Item.mana = 10;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HallowedBar, 14)
            .AddIngredient(ModContent.ItemType<Weapon.Magic.Coralstaff>(), 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}