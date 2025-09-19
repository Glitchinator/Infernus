using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Ammo
{
    public class Crystal_Arrow_Ammo : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = 2000;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<Projectiles.Crystal_Arrow>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            CreateRecipe(70)
            .AddIngredient(ItemID.WoodenArrow, 70)
            .AddIngredient(ItemID.CrystalShard, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
