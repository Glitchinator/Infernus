using Infernus.Items.Accesories;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Blazing_Cloak : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            Item.value = Item.buyPrice(0, 14, 25, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Enchanted_Femur = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Meteor_Core = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Raging_Bone = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Deceit_Cloak = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Blazing_Cloak = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Cloak_of_Deceit>(), 1)
            .AddIngredient(ModContent.ItemType<Raging_Bone>(), 1)
            .AddIngredient(ItemID.BeetleHusk, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}