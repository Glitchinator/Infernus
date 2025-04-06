using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Ranged
{
    public class Flarovolver : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 46;
            Item.height = 20;
            Item.useAnimation = 42;
            Item.useTime = 42;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 6, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ProjectileID.Flare;
            Item.shootSpeed = 15f;
            Item.useAmmo = AmmoID.Flare;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar, 8)
            .AddIngredient(ItemID.FlareGun, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .22f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.damage = 13;
                Item.useAnimation = 30;
                Item.useTime = 6;
                Item.reuseDelay = 70;
                Item.shootSpeed = 11f;
                Item.knockBack = 1f;
            }
            else
            {
                Item.reuseDelay = 0;
                SetDefaults();
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));

                newVelocity *= 1f - Main.rand.NextFloat(0.4f);

                int p = Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                Main.projectile[p].timeLeft = 180;
            }
            else
            {
                int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                Main.projectile[p].timeLeft = 180;
            }
            return false;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(26));
            }
        }
    }
}