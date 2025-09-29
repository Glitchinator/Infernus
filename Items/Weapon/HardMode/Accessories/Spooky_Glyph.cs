using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Spooky_Glyph : ModItem
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
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaRegenDelay -= 0.65f;
            if (player.statMana > (int)(player.statManaMax * 0.7f))
            {
                player.GetDamage(DamageClass.Magic) += 0.35f;
                player.manaRegen += 12;
                return;
            }
            if (player.statMana > (int)(player.statManaMax * 0.5f))
            {
                player.manaRegen += 8;
                player.GetDamage(DamageClass.Magic) += 0.2f;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Hallowed_Glyph>(), 1)
            .AddIngredient(ItemID.SpookyWood, 60)
            .AddIngredient(ItemID.Pumpkin, 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}