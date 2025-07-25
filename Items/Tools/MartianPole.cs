using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class MartianPole : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.CanFishInLava[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 32;
            Item.value = 100000;
            Item.CloneDefaults(ItemID.FiberglassFishingPole);
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ProjectileID.BobberFiberglass;
            Item.fishingPole = 45;
            Item.shootSpeed = 14f;
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
