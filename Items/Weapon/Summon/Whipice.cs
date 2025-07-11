using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Summon
{
    public class Whipice : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<Projectiles.Whipice>(), 32, 3, 5.6f);
            Item.value = Item.buyPrice(0, 10, 50, 0);

            //Item.shootSpeed = 5;
            Item.rare = ItemRarityID.Orange;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.IceSpikes>(), 25)
            .AddIngredient(ItemID.IceBlock, 36)
            .AddIngredient(ModContent.ItemType<Whipaer>(), 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}