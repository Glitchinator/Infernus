using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Squid_Expert : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emergency Ink");
            Tooltip.SetDefault("Has a 33% chance to summon an ink typhoon when the player takes damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 36;
            Item.accessory = true;
            Item.value = 85000;
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Ink_Storm_Equipped = true;
        }
    }
}
