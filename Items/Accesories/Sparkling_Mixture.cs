using Infernus.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Sparkling_Mixture : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 28;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Sparkling_Mixture = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ShadowScale, 4)
            .AddIngredient(ItemID.FallenStar, 6)
            .AddIngredient(ItemID.GlowingMushroom, 20)
            .AddIngredient(ItemID.Bottle, 1)
            .AddTile(TileID.WorkBenches)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.TissueSample, 4)
            .AddIngredient(ItemID.FallenStar, 6)
            .AddIngredient(ItemID.GlowingMushroom, 20)
            .AddIngredient(ItemID.Bottle, 1)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}
