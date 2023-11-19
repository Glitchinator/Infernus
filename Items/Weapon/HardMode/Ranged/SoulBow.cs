using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Ranged
{
    public class SoulBow : ModItem
    {
        int cycle;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 34;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 32;
            Item.height = 44;
            Item.useTime = 20;
            Item.useAnimation = 1;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 14, 50, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item75 with
            {
                Volume = .65f,
                SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
            };
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 11f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            cycle += 1;

            if (cycle == 4)
            {
                SoundEngine.PlaySound(SoundID.Item5 with
                {
                    SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                });
                for (int i = 0; i < 7; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(8));

                    newVelocity *= 1f - Main.rand.NextFloat(.32f);

                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
                cycle = 0;
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SoulofNight, 8)
            .AddIngredient(ItemID.SoulofLight, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
