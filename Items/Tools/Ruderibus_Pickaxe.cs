using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class Ruderibus_Pickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Melee;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6f;
            Item.value = 80000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.pick = 105;
            Item.axe = 28;
            Item.useTurn = true;
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.IceSpikes>(), 12)
            .AddIngredient(ModContent.ItemType<Materials.Cursed_Plasma>(), 16)
            .AddIngredient(ItemID.Bone, 30)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
