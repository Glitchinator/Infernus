using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Ranged
{
    public class Squid_FlameThrower : ModItem
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
            Item.width = 52;
            Item.height = 20;
            Item.useAnimation = 20;
            Item.useTime = 5;
            Item.reuseDelay = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1.4f;
            Item.value = Item.buyPrice(0, 8, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Temporal_Glow_Squid.Drops.Ink_Flamethrower>();
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Gel;
            Item.consumeAmmoOnFirstShotOnly = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; i++)
            {
                velocity *= 1f - Main.rand.NextFloat(0.14f);

                Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}
