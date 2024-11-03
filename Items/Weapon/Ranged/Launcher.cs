using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Ranged
{
    public class Launcher : ModItem
    {
        int cycle;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 56;
            Item.height = 22;
            Item.useAnimation = 43;
            Item.useTime = 43;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 8, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item75 with
            {
                Volume = .65f,
                SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
            };
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 22f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            cycle += 1;

            if (cycle == 4)
            {
                SoundEngine.PlaySound(SoundID.Item36 with
                {
                    SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                });
                for (int i = 0; i < 22; i++)
                {

                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(30));


                    newVelocity *= 1f - Main.rand.NextFloat(0.9f);

                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
                cycle = 0;
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Placeable.Rock>(), 42)
            .AddIngredient(ItemID.MusketBall, 999)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}