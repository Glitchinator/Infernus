using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Melee
{
    public class Swingy2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thiriscus");
            Tooltip.SetDefault("Cursed by the dungeon, the blade turns blue. Showing a thousand souls a fight.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 54;
            Item.useTime = 7;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 9, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Swingy2>();
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 40f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Swingy>(), 1)
            .AddIngredient(ItemID.Muramasa, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}