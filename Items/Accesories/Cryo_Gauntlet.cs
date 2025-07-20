using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Cryo_Gauntlet : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 46;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.whipRangeMultiplier += 0.11f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Ice_Whiphead = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Cryo_Gauntlet = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Cryo_Lash>(), 1)
            .AddIngredient(ModContent.ItemType<Antlion_Fist>(), 1)
            .AddIngredient(ModContent.ItemType<IceSpikes>(), 12)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
