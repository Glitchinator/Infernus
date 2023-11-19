using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class DoomChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = 300000;
            Item.rare = ItemRarityID.Cyan;
            Item.defense = 20;
        }
        public override void UpdateEquip(Player player)
        {
            player.onHitRegen = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ChlorophytePlateMail, 1)
            .AddIngredient(ItemID.HallowedPlateMail, 1)
            .AddIngredient(ItemID.MartianConduitPlating, 62)
            .AddIngredient(ItemID.SoulofFright, 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}