using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class MechPole : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Divine Pole");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.CanFishInLava[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 32;
            Item.value = 1000000;
            Item.CloneDefaults(ItemID.ScarabFishingRod);
            Item.rare = ItemRarityID.Red;
            Item.shoot = ProjectileID.BobberHotline;
            Item.fishingPole = 68;
            Item.shootSpeed = 18f;
        }
    }
}
