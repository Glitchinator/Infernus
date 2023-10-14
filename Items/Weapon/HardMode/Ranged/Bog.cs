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
    public class Bog : ModItem
    {
        int cycle;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hero's Launcher");
            Tooltip.SetDefault("Charge up a powerful bouncing boulder"
                + "\n1.6 Sec Charge Time");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 86;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 52;
            Item.height = 32;
            Item.useAnimation = 1;
            Item.useTime = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6.7f;
            Item.value = Item.buyPrice(0, 22, 50, 0);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item75 with
            {
                Volume = .65f,
                SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
            };
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Boulder_Cannon>();
            Item.shootSpeed = 22f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            cycle += 1;

            if (cycle == 4)
            {
                SoundEngine.PlaySound(SoundID.Item62 with
                {
                    SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                });
                Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                cycle = 0;
            }
            return false;
        }
    }
}