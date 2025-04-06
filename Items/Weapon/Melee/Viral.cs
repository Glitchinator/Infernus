using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Melee
{
    public class Viral : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        int combo = 0;

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(0, 6, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Viral>();
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 9f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 10)
            .AddIngredient(ItemID.TissueSample, 14)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.DemoniteBar, 10)
            .AddIngredient(ItemID.ShadowScale, 14)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (combo == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), position, velocity, ModContent.ProjectileType<Projectiles.Viral>(), damage, knockback, player.whoAmI);
                combo = 1;
            }
            else if (combo == 1)
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), position, velocity, ModContent.ProjectileType<Projectiles.ViralCRIM>(), damage, knockback, player.whoAmI);
                combo = 0;
            }
            return false;
        }
    }
}