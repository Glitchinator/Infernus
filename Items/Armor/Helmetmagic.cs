using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Helmetmagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aeritite Hood");
            Tooltip.SetDefault("Increases your max number of minions by 1");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 6000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 4;
        }
        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ChestPLate>() && legs.type == ModContent.ItemType<Leggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Summoner's Pride" + "\n 12% increased melee speed";
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