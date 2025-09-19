using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Tree_Branch : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Morning_Dew>();
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 30;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 1;
        }
    }
}
