using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Ranged
{
    public class Crystal_Bow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 32;
            Item.height = 54;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 14, 50, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(8));

                newVelocity *= 1f - Main.rand.NextFloat(.32f);

                Projectile.NewProjectileDirect(source, position, newVelocity, ModContent.ProjectileType<Crystal_Arrow>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrystalShard, 24)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
