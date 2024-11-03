using Infernus.Items.Accesories;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Elemental_Lash : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 48;
            Item.value = Item.buyPrice(0, 24, 50, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.18f;
            player.whipRangeMultiplier += 0.11f;
            player.GetDamage(DamageClass.SummonMeleeSpeed) += .15f;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Elemental_Whiphead = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Hallowed_Lash>(), 1)
            .AddIngredient(ItemID.AvengerEmblem, 1)
            .AddIngredient(ItemID.FragmentStardust, 4)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}