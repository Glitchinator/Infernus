using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Consumable
{
    public class Thicker_Boulder : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thicker Boulder");
            Tooltip.SetDefault("Don't throw it, worst thing I've done in my life");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 0;
        }
        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.ForceRoar, player.position);

            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), player.Right.X, player.Right.Y / 1.5f, 0, 0, ProjectileID.Boulder, 500, 0, player.whoAmI);
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.StoneBlock, 495)
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ItemID.Ectoplasm, 4)
            .Register();
        }
    }
}
