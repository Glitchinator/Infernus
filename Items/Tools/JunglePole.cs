using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class JunglePole : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorophyte Pole");
            Tooltip.SetDefault("More powerful in the jungle");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 34;
            Item.value = 100000;
            Item.CloneDefaults(ItemID.SittingDucksFishingRod);
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ProjectileID.BobberWooden;
            Item.fishingPole = 40;
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ChlorophyteBar, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
        public override void HoldItem(Player player)
        {
            if (player.ZoneJungle)
            {
                Item.fishingPole = 55;
            }
            else
            {
                Item.fishingPole = 40;
            }
        }
    }
}
