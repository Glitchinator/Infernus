using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class LunarPole : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.CanFishInLava[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 42;
            Item.value = 100000;
            Item.CloneDefaults(ItemID.SittingDucksFishingRod);
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ProjectileID.BobberFiberglass;
            Item.fishingPole = 55;
            Item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.LunarBar, 8)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
        public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            // Change these two values in order to change the origin of where the line is being drawn.
            // This will make it draw 43 pixels right and 30 pixels up from the player's center, while they are looking right and in normal gravity.

            // Thank you example mod
            lineOriginOffset = new Vector2(43, -30);
        }
    }
}
