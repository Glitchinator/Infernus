using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Ammo
{
    public class Ruderibus_Rounds_Ammo : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 4f;
            Item.value = 20;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ruderibus_Rounds>();
            Item.shootSpeed = 1f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe(70)
            .AddIngredient(ItemID.MusketBall, 70)
            .AddIngredient(ModContent.ItemType<Materials.Ruderibus_Shard>(), 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
