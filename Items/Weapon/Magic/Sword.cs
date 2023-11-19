using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class Sword : ModItem
    {
        int cycle;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.width = 28;
            Item.height = 32;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(0, 5, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Cloud_Knife>();
            Item.noUseGraphic = true;
            Item.shootSpeed = 12f;
            Item.mana = 8;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            cycle += 1;

            if (cycle == 4)
            {
                SoundEngine.PlaySound(SoundID.Item9 with
                {
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                });
                Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Projectiles.Cloud_Knife_Prime>(), damage, knockback, player.whoAmI);
                cycle = 0;
                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Cloud, 80)
            .AddIngredient(ItemID.Feather, 12)
            .AddIngredient(ItemID.SunplateBlock, 40)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}