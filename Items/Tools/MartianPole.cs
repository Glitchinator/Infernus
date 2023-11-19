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
    }
}
