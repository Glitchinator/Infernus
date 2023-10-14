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
    public class Lightning_Daggers : ModItem
    {
        int cycle;
        int numb;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Daggers");
            Tooltip.SetDefault("Charge up a barrage of daggers"
                + "\n 25% chance to shoot a overpowered dagger"
                + "\n 1 Sec Charge Time");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 92;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 22;
            Item.height = 32;
            Item.useAnimation = 1;
            Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 22, 50, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item75 with
            {
                Volume = .65f,
                SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
            };
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Bouldermagicweaponshot>();
            Item.shootSpeed = 22f;
            Item.noUseGraphic = true;
            Item.maxStack = 9999;
            Item.consumable = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            cycle += 1;
            numb = Main.rand.Next(new int[] { 0, 1, 2 ,3});

            if (cycle == 3)
            {
                SoundEngine.PlaySound(SoundID.Item39 with
                {
                    SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
                });
                if(numb == 2)
                {

                    Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Lightning_Dagger_Giant>(), damage * 4, knockback, player.whoAmI);
                    cycle = 0;
                    numb = 0;
                    return false;
                }
                for (int i = 0; i < 6; i++)
                {

                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));

                    Projectile.NewProjectileDirect(source, position, newVelocity, ModContent.ProjectileType<Lightning_Daggers_Shot>(), damage, knockback, player.whoAmI);
                }
                cycle = 0;
            }
            return false;
        }
    }
}