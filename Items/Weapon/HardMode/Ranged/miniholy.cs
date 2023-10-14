using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Ranged
{
    public class miniholy : ModItem
    {
        public int pumps;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pump Charge Shotgun");
            Tooltip.SetDefault("Fires a spread of flaming bullets"
                                + "\n Right click to charge the shotgun loading more bullets into the chamber"
                                 + "\n If you charge too many times the player will throw the exploding shotgun"
                                  + "\n Overcharging increases damages to a max of 10 charges");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 155;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 56;
            Item.height = 20;
            Item.useAnimation = 49;
            Item.useTime = 49;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 37, 50, 0);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = new SoundStyle("Infernus/Sounds/Charge_ShotGun_Shoot") with
            {
                MaxInstances = 2,
                SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
            };
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Pump_Fire_Slug>();
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
            Item.holdStyle = ItemHoldStyleID.HoldHeavy;
            Item.noUseGraphic = false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .55f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.UseSound = new SoundStyle("Infernus/Sounds/Charge") with
                {
                    MaxInstances = 2,
                    SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
                };
                Item.noUseGraphic = false;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.useAnimation = 29;
                Item.useTime = 29;
            }
            else
            {
                SetDefaults();
                if (pumps >= 3)
                {
                    Item.UseSound = SoundID.Item1;
                    Item.useStyle = ItemUseStyleID.Swing;
                    Item.noUseGraphic = true;
                }
                else
                {
                    SetDefaults();
                }
            }
            return base.CanUseItem(player);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 0);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if(pumps >= 3)
            {
                velocity *= .55f;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                pumps += 1;
                if (pumps <= 1)
                {
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Green, pumps, true);
                }
                if (pumps == 2)
                {
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Orange, pumps, true);
                }
                if (pumps >= 3)
                {
                    if(pumps >= 10)
                    {
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Purple, pumps, true);
                        return false;
                    }
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Red, pumps, true);
                }
            }
            if (pumps == 0 && player.altFunctionUse != 2)
            {
                for (int i = 0; i < 9; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(11));

                    newVelocity *= 1f - Main.rand.NextFloat(.2f);

                    Projectile.NewProjectileDirect(source, position, newVelocity, ModContent.ProjectileType<Pump_Fire_Slug>(), damage, knockback, player.whoAmI);
                }
            }
            if (pumps == 1 && player.altFunctionUse != 2)
            {
                for (int i = 0; i < 15; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                    newVelocity *= 1f - Main.rand.NextFloat(.2f);

                    Projectile.NewProjectileDirect(source, position, newVelocity, ModContent.ProjectileType<Pump_Fire_Slug>(), damage * 2, knockback, player.whoAmI);
                }
                pumps = 0;
            }
            if (pumps == 2 && player.altFunctionUse != 2)
            {
                for (int i = 0; i < 21; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(19));

                    newVelocity *= 1f - Main.rand.NextFloat(.2f);

                    Projectile.NewProjectileDirect(source, position, newVelocity, ModContent.ProjectileType<Pump_Fire_Slug>(), damage * 3, knockback, player.whoAmI);
                }
                pumps = 0;
            }
            if (pumps >= 3 && player.altFunctionUse != 2)
            {
                if(pumps >= 10)
                {
                    Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Pump_Charge_Shotgun>(), 3080, knockback, player.whoAmI);
                    pumps = 0;
                    return false;
                }
                Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Pump_Charge_Shotgun>(), (int)((damage * pumps) * .90f), knockback, player.whoAmI);
                pumps = 0;
            }
            return false;
        }
    }
}