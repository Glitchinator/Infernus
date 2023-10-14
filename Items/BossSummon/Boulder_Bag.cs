using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.BossSummon
{
    public class Boulder_Bag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cracked Husk");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 30;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Weapon.HardMode.Accessories.HiveHeart>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Weapon.HardMode.Accessories.Wings>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Weapon.HardMode.Melee.bould>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Weapon.HardMode.Magic.Bouldermagicweapon>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Weapon.HardMode.Magic.BoulderTomb>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Weapon.HardMode.Ranged.Bog>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Weapon.HardMode.Summon.Whiprock>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Materials.Broken_Heros_Staff>(), 2));
        }
    }
}

