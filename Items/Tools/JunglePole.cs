using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class JunglePole : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 34;
            Item.value = 100000;
            Item.CloneDefaults(ItemID.SittingDucksFishingRod);
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ProjectileID.BobberWooden;
            Item.fishingPole = 45;
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ChlorophyteBar, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
        public override void HoldItem(Player player)
        {
            if (player.ZoneJungle)
            {
                Item.fishingPole = 55;
            }
            else
            {
                Item.fishingPole = 45;
            }
        }
        public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            // Change these two values in order to change the origin of where the line is being drawn.
            // This will make it draw 43 pixels right and 28 pixels up from the player's center, while they are looking right and in normal gravity.

            // Thank you example mod
            lineOriginOffset = new Vector2(43, -28);
        }
    }
}
