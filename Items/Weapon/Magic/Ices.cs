using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class Ices : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacies Lancea");
            Tooltip.SetDefault("A spear of winter, throw it. And a path is opened.");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Magic;
            Item.width = 52;
            Item.height = 52;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 7, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ice_Spear>();
            Item.shootSpeed = 9f;
            Item.mana = 10;
            Item.noUseGraphic = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.IceSpikes>(), 26)
            .AddIngredient(ItemID.IceBlock, 30)
            .AddIngredient(ItemID.Obsidian, 25)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}