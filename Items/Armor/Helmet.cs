using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Helmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aeritite Helm");
            Tooltip.SetDefault("6% increased melee damage");
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
            player.GetDamage(DamageClass.Melee) += .06f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ChestPLate>() && legs.type == ModContent.ItemType<Leggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Melee's Speed" + "\n 12% increased melee speed";
            player.GetAttackSpeed(DamageClass.Melee) += .12f;

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