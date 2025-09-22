using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Raging_Bone : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 40;
            Item.accessory = true;
            Item.value = 60000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Enchanted_Femur = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Meteor_Core = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Raging_Bone = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Meteor_Core>(), 1)
            .AddIngredient(ModContent.ItemType<Enchanted_Femur>(), 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
