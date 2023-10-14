using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    [AutoloadEquip(EquipType.Back)]
    public class Shiny : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Raiko's Toothpaste");
            Tooltip.SetDefault("Allows the holder to double jump"
                + "\n 4% increased movement speed" + "\n 4% increased jump speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.accessory = true;
            Item.value = 95000;
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += .04f;
            player.maxRunSpeed += .04f;
            player.hasJumpOption_WallOfFleshGoat = true;
            player.jumpSpeedBoost += .04f;
        }
    }
}
