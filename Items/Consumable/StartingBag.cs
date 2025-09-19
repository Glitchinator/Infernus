using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Consumable
{
    public class StartingBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 28;
            Item.height = 32;
            Item.rare = ItemRarityID.White;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {

            itemLoot.Add(ItemDropRule.Common(ItemID.CopperHammer, 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.GrapplingHook, 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.ShinePotion, 1, 2, 2));
            itemLoot.Add(ItemDropRule.Common(ItemID.MiningPotion, 1, 2, 2));
            itemLoot.Add(ItemDropRule.Common(ItemID.BuilderPotion, 1, 2, 2));
            itemLoot.Add(ItemDropRule.Common(ItemID.RecallPotion, 1, 2, 2));
            itemLoot.Add(ItemDropRule.Common(ItemID.WormholePotion, 1, 2, 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Tools.Magic9Ball>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Tools.LevelEnabler>()));
        }
    }
}