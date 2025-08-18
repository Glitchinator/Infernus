using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Melee
{
    public class golemsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 125;
            Item.DamageType = DamageClass.Melee;
            Item.width = 54;
            Item.height = 54;
            Item.useTime = 20;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 24, 50, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item19;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.SwordProjecilte>();
            Item.shootSpeed = 17f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.LihzahrdBrick, 46)
            .AddIngredient(ItemID.GoldDust, 22)
            .AddIngredient(ItemID.ExplosivePowder, 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}