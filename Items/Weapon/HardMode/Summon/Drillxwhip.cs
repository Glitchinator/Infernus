using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Summon
{
    public class Drillxwhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<Projectiles.WhipDrillx>(), 95, 3, 8, 38);
            Item.value = Item.buyPrice(0, 30, 50, 0);

            Item.rare = ItemRarityID.Yellow;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Cog, 82)
            .AddIngredient(ItemID.ExplosivePowder, 32)
            .AddIngredient(ItemID.LihzahrdBrick, 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}