using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class EqualStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Equite Staff");
            Tooltip.SetDefault("Conjures magic spears and daggers");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.Magic;
            Item.width = 48;
            Item.height = 48;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(0, 1, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.EqualSword>();
            Item.shootSpeed = 26f;
            Item.mana = 11;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Equite_Bar>(), 16)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ProjectileID.MagicDagger, damage, knockback, player.whoAmI, 0f);
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.EqualSword>(), damage, knockback, player.whoAmI, 0f);
            return false;
        }
    }
}