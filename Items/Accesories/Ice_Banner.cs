﻿using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Ice_Banner : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 36;
            Item.accessory = true;
            Item.value = 60000;
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 60;
            player.manaCost -= .12f;
            player.manaRegen += 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Scroll>(), 1)
            .AddIngredient(ItemID.Bone, 24)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
