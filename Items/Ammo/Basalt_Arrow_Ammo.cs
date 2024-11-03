using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Ammo
{
    public class Basalt_Arrow_Ammo : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 4f;
            Item.value = 20;
            Item.rare = ItemRarityID.LightPurple;
            Item.shoot = ModContent.ProjectileType<Projectiles.Basalt_Arrow>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            CreateRecipe(70)
            .AddIngredient(ItemID.WoodenArrow, 70)
            .AddIngredient(ModContent.ItemType<Materials.Crumbling_Basalt>(), 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
