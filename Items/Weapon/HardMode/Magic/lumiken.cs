using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Magic
{
    public class lumiken : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Three");
            Tooltip.SetDefault("Throws three different shurikens.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        int numb;

        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.DamageType = DamageClass.Magic;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 26, 50, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Shuriken_Luminite>();
            Item.shootSpeed = 16f;
            Item.mana = 10;
            Item.noUseGraphic = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.LunarBar, 12)
            .AddIngredient(ItemID.FragmentNebula, 2)
            .AddIngredient(ItemID.FragmentSolar, 2)
            .AddIngredient(ItemID.FragmentStardust, 2)
            .AddIngredient(ItemID.FragmentVortex, 2)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            numb = Main.rand.Next(new int[] { 0, 1, 2 });
            float rotation = MathHelper.ToRadians(9);
            float rotation2 = MathHelper.ToRadians(18);

            if (numb == 0)
            {
                Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Shuriken_Nebula>(), damage * 3, knockback, player.whoAmI);
            }
            if (numb == 1)
            {
                Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Shuriken_HellFire>(), damage, knockback, player.whoAmI);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i));
                    Projectile.NewProjectileDirect(source, position, perturbedSpeed, ModContent.ProjectileType<Shuriken_HellFire>(), damage * 2, knockback, player.whoAmI);
                }
            }
            if (numb == 2)
            {
                Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Shuriken_Luminite>(), damage, knockback, player.whoAmI);
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i));
                    Projectile.NewProjectileDirect(source, position, perturbedSpeed, ModContent.ProjectileType<Shuriken_Luminite>(), damage, knockback, player.whoAmI);
                }
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation2, rotation2, i));
                    Projectile.NewProjectileDirect(source, position, perturbedSpeed, ModContent.ProjectileType<Shuriken_Luminite>(), damage, knockback, player.whoAmI);
                }
            }
            return false;
        }
    }
}