using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class deitygift : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 44;
            Item.value = Item.buyPrice(0, 24, 50, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife > (int)(player.statLifeMax * 0.5f))
            {
                player.GetDamage(DamageClass.Generic) += 0.15f;
            }
            else
            {
                player.statDefense += 12;
                player.moveSpeed += 0.1f;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Crumbling_Basalt>(), 8)
            .AddIngredient(ItemID.SpookyWood, 8)
            .AddIngredient(ItemID.Ectoplasm, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}