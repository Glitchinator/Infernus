using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class DoomLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 300000;
            Item.rare = ItemRarityID.Cyan;
            Item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += .15f;
            player.GetCritChance(DamageClass.Generic) += 14;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ChlorophyteGreaves, 1)
            .AddIngredient(ItemID.AncientHallowedGreaves, 1)
            .AddIngredient(ItemID.MartianConduitPlating, 32)
            .AddIngredient(ItemID.SoulofMight, 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}