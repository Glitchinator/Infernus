﻿using Terraria;
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
            Item.width = 28;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 14, 25, 0);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 6;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Heart_Equipped = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Accesories.Stone_Emblem>(), 1)
            .AddIngredient(ModContent.ItemType<Toxic_Fang>(), 1)
            .AddIngredient(ItemID.HoneyComb, 1)
            .AddIngredient(ItemID.Bezoar, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}