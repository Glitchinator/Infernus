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
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 19;
            Item.DamageType = DamageClass.Magic;
            Item.width = 48;
            Item.height = 48;
            Item.useAnimation = 34;
            Item.useTime = 34;
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
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            for (int i = 0; i < 3; i++)
            {
                position = Main.MouseWorld - new Vector2(Main.rand.NextFloat(201) * player.direction, 10f);
                position.Y -= 20 * i;
                Vector2 heading = target - position;

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-10, 11) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI, 0f, 0);
            }

            return false;
        }
    }
}