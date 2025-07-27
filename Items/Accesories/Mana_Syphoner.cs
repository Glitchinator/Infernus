using Infernus.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Mana_Syphoner : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 46;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Mana_Syphoner = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (hideVisual == false)
            {
                for (int i = 0; i < 24; i++)
                {
                    Vector2 dust_v = player.Center - new Vector2(300, 0).RotatedBy(i * Math.PI * 2 / 24f) - player.Center;
                    var dest = dust_v.SafeNormalize(Vector2.Zero);
                    Dust h = Dust.NewDustPerfect(player.Center + new Vector2(300, 0).RotatedBy(i * Math.PI * 2 / 24f), DustID.PurpleCrystalShard, dest * 20f);
                    h.noGravity = true;
                    h.noLight = true;
                }
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Thorny_Extension>(), 1)
            .AddIngredient(ModContent.ItemType<Frozen_Whiphead>(), 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
