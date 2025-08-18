using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Melee
{
    public class Frisbe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 220;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6f;
            Item.noUseGraphic = true;
            Item.value = Item.buyPrice(0, 26, 50, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item19;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Frisbe>();
            Item.shootSpeed = 20;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.LunarBar, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}