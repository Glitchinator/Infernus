using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Equite_Helmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.value = 6000;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 9;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 9;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Equite_Breastplate>() && legs.type == ModContent.ItemType<Equite_Leggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Pineapple Power" + "\n You gain deadly leaves over time, up to a max of 4" + "\n Hitting enemies will make them attack that enemy";
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Equipped = true;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Equite_Bar>(), 14)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}