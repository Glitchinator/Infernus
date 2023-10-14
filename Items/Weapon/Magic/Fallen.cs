using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class Fallen : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorched Stick");
            Tooltip.SetDefault("Shoots stars and fire");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 1, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ProjectileID.HallowStar;
            Item.shootSpeed = 12f;
            Item.mana = 10;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar, 3)
            .AddIngredient(ModContent.ItemType<Rifle>(), 1)
            .AddIngredient(ModContent.ItemType<Materials.Hot>(), 32)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numb = 1 + Main.rand.Next(1);
            for (int i = 0; i < numb; i++)
            {
                type = Main.rand.Next(new int[] { type, ProjectileID.BallofFire, ProjectileID.Flames, });
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), position, velocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}