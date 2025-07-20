using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Mana_Gift : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 46;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statMana > (int)(player.statManaMax * 0.7f))
            {
                player.GetDamage(DamageClass.Magic) += 0.1f;
                return;
            }
            if (player.statMana > (int)(player.statManaMax * 0.5f))
            {
                player.GetDamage(DamageClass.Magic) += 0.05f;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Thorny_Extension>(), 1)
            .AddIngredient(ModContent.ItemType<Frozen_Whiphead>(), 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
