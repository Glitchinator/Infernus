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
            Item.width = 26;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 24, 50, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions++;
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.14f;
            player.GetDamage(DamageClass.Summon) += .18f;
            player.GetKnockback(DamageClass.Summon) += .12f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Hallowed_Lash>(), 1)
            .AddIngredient(ModContent.ItemType<CHARD>(), 1)
            .AddRecipeGroup(RecipeGroupID.Fragment, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}