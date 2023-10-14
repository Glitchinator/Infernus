using Infernus.Placeable;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Melee
{
    public class EarthShock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthquake");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 66;
            Item.DamageType = DamageClass.Melee;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5.8f;
            Item.value = 150000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item19;
            Item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.StoneBlock, 80)
            .AddIngredient(ModContent.ItemType<Rock>(), 14)
            .AddIngredient(ItemID.SoulofLight, 4)
            .AddIngredient(ItemID.SoulofNight, 4)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Right.X, target.Right.Y / 1.35f, 0, 0, ModContent.ProjectileType<Projectiles.Boulder2>(), damage, 0, player.whoAmI);
        }
    }
}