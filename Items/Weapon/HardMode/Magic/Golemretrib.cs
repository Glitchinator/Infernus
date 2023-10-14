using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Magic
{
    public class Golemretrib : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Telsa Revolt");
            Tooltip.SetDefault("Shoots waves of electricity frying anything in it's way");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 68;
            Item.DamageType = DamageClass.Magic;
            Item.width = 24;
            Item.height = 56;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5f;
            Item.value = 290000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item94;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Tesla>();
            Item.shootSpeed = 5f;
            Item.mana = 8;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            float rotation = MathHelper.ToRadians(17);
            float rotation2 = MathHelper.ToRadians(8);
            float rotation3 = MathHelper.ToRadians(3);
            Vector2 velocity2 = velocity * 2;
            Vector2 velocity3 = velocity * 3;
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i));
                Projectile.NewProjectileDirect(source, position, perturbedSpeed, ModContent.ProjectileType<Tesla>(), damage * 3, knockback, player.whoAmI);
            }

            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity2.RotatedBy(MathHelper.Lerp(-rotation2, rotation2, i));
                Projectile.NewProjectileDirect(source, position, perturbedSpeed, ModContent.ProjectileType<Tesla>(), damage * 2, knockback, player.whoAmI);
            }
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity3.RotatedBy(MathHelper.Lerp(-rotation3, rotation3, i));
                Projectile.NewProjectileDirect(source, position, perturbedSpeed, ModContent.ProjectileType<Tesla>(), damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}