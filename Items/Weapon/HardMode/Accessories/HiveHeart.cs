using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class HiveHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 14, 25, 0);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) += 0.15f;
            player.manaCost -= 0.15f;
            player.GetCritChance(DamageClass.Magic) += 10;
            player.statManaMax2 += 100;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Undying_Ember>(), 1)
            .AddIngredient(ModContent.ItemType<Dusk_Sigil>(), 1)
            .AddIngredient(ModContent.ItemType<Ocean_Crest>(), 1)
            .AddIngredient(ModContent.ItemType<Crumbling_Basalt>(), 30)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}