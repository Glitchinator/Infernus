using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.BossSummon
{
    public class Squid_BossSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = 0;
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.maxStack = 20;
            Item.bait = 69;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FallenStar, 2)
            .AddIngredient(ItemID.Lens, 1)
            .AddIngredient(ItemID.Gel, 6)
            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }
}