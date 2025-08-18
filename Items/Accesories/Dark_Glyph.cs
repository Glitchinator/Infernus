using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Dark_Glyph : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 44;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statMana > (int)(player.statManaMax * 0.7f))
            {
                player.GetDamage(DamageClass.Magic) += 0.16f;
                player.manaRegen += 12;
                return;
            }
            if (player.statMana > (int)(player.statManaMax * 0.5f))
            {
                player.manaRegen += 8;
                player.GetDamage(DamageClass.Magic) += 0.08f;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Dark_Heart>(), 1)
            .AddIngredient(ModContent.ItemType<Magic_Glyph>(), 1)
            .AddIngredient(ModContent.ItemType<Cursed_Plasma>(), 16)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
