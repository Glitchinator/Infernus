using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class Solder : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("First Prism");
            Tooltip.SetDefault("conjures exploding aeritite shards");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.width = 30;
            Item.height = 30;
            Item.useAnimation = 42;
            Item.useTime = 32;
            Item.reuseDelay = 38;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(0, 4, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ProjectileID.NailFriendly;
            Item.shootSpeed = 9f;
            Item.autoReuse = true;
            Item.mana = 10;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Gaming>(), 9)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
