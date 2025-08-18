using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Consumable
{
    public class BrickFall_Potion : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 1000;
            Item.buffType = ModContent.BuffType<Buffs.Equite_Melee_Debuff>();
            Item.buffTime = 36000;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Placeable.Rock>(), 4)
            .AddIngredient(ItemID.Blinkroot, 1)
            .AddIngredient(ItemID.BottledWater, 1)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}
