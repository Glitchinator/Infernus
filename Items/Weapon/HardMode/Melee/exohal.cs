using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Melee
{
    public class exohal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shining Gladius");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 130;
            Item.DamageType = DamageClass.Melee;
            Item.width = 75;
            Item.height = 75;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7f;
            Item.value = 240000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item19;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Ectoplasm, 8)
            .AddIngredient(ItemID.ChlorophyteClaymore, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Right.X, target.Right.Y, 0, 0, ProjectileID.DD2ExplosiveTrapT1Explosion, damage * 2, 0, player.whoAmI);
        }
    }
}