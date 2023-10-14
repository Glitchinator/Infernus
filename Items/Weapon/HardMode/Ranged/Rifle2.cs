using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Ranged
{
    public class Rifle2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Ectofire");
            Tooltip.SetDefault("Converts gel into homing flames");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 58;
            Item.height = 20;
            Item.useAnimation = 20;
            Item.useTime = 4;
            Item.reuseDelay = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 23, 50, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Flicker_Flame>();
            Item.shootSpeed = 18f;
            Item.useAmmo = AmmoID.Gel;
            Item.consumeAmmoOnFirstShotOnly = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            for (int i = 0; i < 7; i++)
            {
                velocity *= 1f - Main.rand.NextFloat(.24f);

                Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Ectoplasm, 16)
            .AddIngredient(ItemID.Flamethrower, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
