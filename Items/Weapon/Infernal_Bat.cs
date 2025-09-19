using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata.Ecma335;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon
{
    public class Infernal_Bat : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Generic;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 34;
            Item.useAnimation = 34;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.5f;
            Item.value = Item.buyPrice(0, 8, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.DD2_MonkStaffSwing;
            Item.autoReuse = true;
            Item.holdStyle = ItemHoldStyleID.HoldGuitar;
            Item.ArmorPenetration = 10000;
            Item.shoot = ModContent.ProjectileType<Infernus_Bat_Energy>();
            Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
            Item.autoReuse = true;
            Item.shootSpeed = 24f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI); // Sync the changes in multiplayer.

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            hit.HideCombatText = true;
            CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), InfernusPlayer.GainXP_Resource, Main.rand.Next(new[] { "Bonk", "Wham", "Kablam", "Pow" }), true);
            SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, target.position);
        }
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HideCombatText();
            modifiers.FinalDamage += (int)(target.lifeMax * 0.02f) - 1;
        }
    }
}