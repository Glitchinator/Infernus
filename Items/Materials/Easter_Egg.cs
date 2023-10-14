using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Materials
{
    public class Easter_Egg : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Easter Egg");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.rare = ItemRarityID.White;
            Item.maxStack = 999;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {

            itemLoot.Add(ItemDropRule.Common(ItemID.Milkshake, 2));
            itemLoot.Add(ItemDropRule.Common(ItemID.FlaskofGold, 2));
            itemLoot.Add(ItemDropRule.Common(ItemID.JestersArrow, 2, 30, 60));

            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCID.BlueSlime));
        }
    }
}
