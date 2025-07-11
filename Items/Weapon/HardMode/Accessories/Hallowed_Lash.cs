using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class Hallowed_Lash : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 48;
            Item.value = Item.buyPrice(0, 24, 50, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.14f;
            player.GetDamage(DamageClass.Summon) += .10f;
            player.GetKnockback(DamageClass.Summon) += .12f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Hallowed_Chain>(), 1)
            .AddIngredient(ItemID.HerculesBeetle)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}