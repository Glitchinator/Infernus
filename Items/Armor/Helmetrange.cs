using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Helmetrange : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aeritite Headgear");
            Tooltip.SetDefault("6% increased ranged damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 6000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 5;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += .06f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ChestPLate>() && legs.type == ModContent.ItemType<Leggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Ranger's Steadiness" + "\n 6% increased ranged critical strike chance";
            player.GetCritChance(DamageClass.Ranged) += 6;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Gaming>(), 20)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}