using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Ranged
{
    public class MissleSilo_Bow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 44;
            Item.height = 60;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(0, 6, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 4.5f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override void UseAnimation(Player player)
        {
            for (int k = 0; k < 11; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                Dust Sword = Dust.NewDustPerfect(player.Center + speed * 32, DustID.SolarFlare, speed * 3, Scale: 1f);
                Sword.noGravity = true;
            }
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .33f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            for (int i = 0; i < 1; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(10) * player.direction, 4f);
                position.Y -= 4 * i;
                position.X -= 20 * i;
                Vector2 heading = target - position;

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-300, 300) * 0.02f;
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ModContent.ProjectileType<Projectiles.Missle_Bow>();
                }
                Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI, 0f, 0);
            }

            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Weapon.Ranged.Firebow>(), 1)
            .AddIngredient(ItemID.IllegalGunParts, 2)
            .AddIngredient(ItemID.FragmentVortex, 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
