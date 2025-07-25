﻿using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Infernus.Placeable
{
    public class Lucky_Candle_Pl : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(copper: 2);

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useTime = 10;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.consumable = true;
            Item.maxStack = 999;

            Item.createTile = TileType<Tiles.Lucky_Flame_Tile>();
        }
        /*
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Scorched_Sinew>(), 8)
            .AddIngredient(ItemID.Candle, 1)
            .AddTile(TileID.Furnaces)
            .Register();
        }
        */
    }
}