using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Invas
{
    public class ThickBoulder : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thick Boulder");
            Tooltip.SetDefault("Don't throw it, the boulders are watching");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.noUseGraphic = true;
        }
        public override bool CanUseItem(Player player)
        {
            return Main.dayTime == true;
        }
        public override bool? UseItem(Player player)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText("A storm rolls overhead from the west. The ground trembles with vibrations deep below.", 207, 196, 162);
            }
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                Main.NewText("A storm rolls overhead from the west. The ground trembles with vibrations deep below.", 207, 196, 162);
            }
            BoulderInvasion.StartBoulderInvasion();
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.StoneBlock, 99)
            .AddIngredient(ItemID.Bone, 4)
            .Register();
        }

    }
}