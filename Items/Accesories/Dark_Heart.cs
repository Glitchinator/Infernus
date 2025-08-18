using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Dark_Heart : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 30;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statMana > (int)(player.statManaMax * 0.7f))
            {
                player.GetDamage(DamageClass.Magic) += 0.16f;
                return;
            }
            if (player.statMana > (int)(player.statManaMax * 0.5f))
            {
                player.GetDamage(DamageClass.Magic) += 0.08f;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.DemoniteBar, 6)
            .AddIngredient(ItemID.LifeCrystal, 1)
            .AddIngredient(ItemID.RottenChunk, 8)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 6)
            .AddIngredient(ItemID.LifeCrystal, 1)
            .AddIngredient(ItemID.Vertebrae, 8)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
