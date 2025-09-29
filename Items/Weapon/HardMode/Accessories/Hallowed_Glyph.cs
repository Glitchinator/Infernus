using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Hallowed_Glyph : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 24, 50, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaRegenDelay -= 0.50f;
            if (player.statMana > (int)(player.statManaMax * 0.7f))
            {
                player.GetDamage(DamageClass.Magic) += 0.25f;
                player.manaRegen += 10;
                return;
            }
            if (player.statMana > (int)(player.statManaMax * 0.5f))
            {
                player.manaRegen += 6;
                player.GetDamage(DamageClass.Magic) += 0.15f;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.AvengerEmblem, 1)
            .AddIngredient(ModContent.ItemType<Dark_Glyph>(), 1)
            .AddIngredient(ItemID.HallowedBar, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}