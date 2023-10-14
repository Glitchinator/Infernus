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
            DisplayName.SetDefault("Terrarians Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

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
            itemLoot.Add(ItemDropRule.Common(ItemID.AmethystStaff, 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.BlandWhip, 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.CopperBow, 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.CopperBroadsword, 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.WoodenArrow, 1, 100, 100));
            itemLoot.Add(ItemDropRule.Common(ItemID.ShinePotion, 1, 4, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.MiningPotion, 1, 4, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.SpelunkerPotion, 1, 4, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.GillsPotion, 1, 4, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.RecallPotion, 1, 4, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.WormholePotion, 1, 4, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.Torch, 1, 30, 30));
            itemLoot.Add(ItemDropRule.Common(ItemID.ManaCrystal, 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Tools.Magic9Ball>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Tools.LevelEnabler>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Tools.Dwarfern_Pickaxe>()));
        }
    }
}