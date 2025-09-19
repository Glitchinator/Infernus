using Infernus.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Morning_Dew : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Tree_Branch>();
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 26;
            Item.accessory = true;
            Item.value = 45000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Morning_Dew = true;
        }
    }
}
