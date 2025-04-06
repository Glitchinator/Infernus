using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Ranged
{
    public class Firebow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 44;
            Item.height = 58;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(0, 6, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 8f;
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
            for (int i = 0; i < 2; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(0) * player.direction, 0f);
                position.Y -= 0 * i;
                position.X -= 20 * i;
                Vector2 heading = target - position;

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-15, 15) * 0.013f;
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ModContent.ProjectileType<Projectiles.Raiko_Bow_Arrow>();
                }
                Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI, 0f, 0);
            }

            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
    }
}
