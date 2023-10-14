using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Bone_Sash : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silky Ossein Sash");
            Tooltip.SetDefault("Decreases defense and life regen but exchanges it for offense");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 28;
            Item.accessory = true;
            Item.value = 65000;
            Item.rare = ItemRarityID.Orange;
            Item.defense = -6;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += .19f;
            if (player.lifeRegenCount >= 2)
            {
                player.lifeRegenCount -= 1;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Blindfold>(), 1)
            .AddIngredient(ItemID.Bone, 16)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
