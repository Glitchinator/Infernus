using Infernus.Buffs;
using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Infernus
{
    internal class InfernusPlayer : ModPlayer
    {
        // Doom Guy dash variables
        public const int DashRight = 2;
        public const int DashLeft = 3;
        public const int DashCooldown = 150;
        public const int DashDuration = 39;
        public const float DashVelocity = 16.6f;
        public int DashDir = -1;
        public bool DoomDash;
        public int DashDelay = 0;
        public int DashTimer = 0;

        // stress variables
        public int Stress_Current;
        public const int DefaultStress_Max = 8;
        public int Stress_Max;
        public int Stress_Max2;
        public static readonly Color GainXP_Resource = new(247, 171, 72);
        public bool Stress_Buff_1 = false;
        public bool Stress_Buff_2 = false;

        // Ink Storm Variable
        public bool Ink_Storm_Equipped = false;

        // aeritite armor variables
        public bool Aeritite_Equipped = false;

        // Boulder Expert Variable
        public bool Boulder_Equipped = false;
        public int Boulder_Timer;

        // Meteor ring variables
        public bool Meteor_Storm_Active = false;

        // Snowfall tiara variables
        public bool Tiara_Equipped = false;

        // mechanical mind equipped
        public bool Mech_Equipped = false;

        // hive heart
        public bool Heart_Equipped = false;

        // chipped whetstone
        public bool NPC_Bleeding = false;

        public override void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
        {
            if (Player.statLife < Player.statLifeMax)
            {
                price = 100;
            }
        }
        public override void OnEnterWorld()
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText("Welcome to Infernus Mod! If you have any questions the Discord or Forum is the place to ask." + "\nYou are playing on Infernus V1.6.3", GainXP_Resource);
            }
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                Main.NewText("Welcome to Infernus Mod! If you have any questions the Discord or Forum is the place to ask." + "\nYou are playing on Infernus V1.6.3", GainXP_Resource);
            }
        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (Ink_Storm_Equipped == true)
            {
                if (Main.rand.Next(3) < 1)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<Ink_EmergencyTyphoon>(), 16, 0, 0);
                }
            }
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            if (InfernusNPC.Is_Spawned == true)
            {
                Stress_Current += 1;
            }
            if(Aeritite_Equipped == true)
            {
                Player.AddBuff(ModContent.BuffType<Aeritite_Timer>(), 1800);
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Heart_Equipped == true)
            {
                target.AddBuff(BuffID.Venom, 700);
                target.AddBuff(BuffID.Poisoned, 700);
            }
            if(NPC_Bleeding == true)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Bleeding_Debuff>(), 180);
            }
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Mech_Equipped == true)
            {
                Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Lazar>(), (int)(damage * .35f), 0, 0);
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }
        public override void LoadData(TagCompound tag)
        {
            Stress_Buff_1 = tag.GetBool("Stress_Buff_1");
            Stress_Buff_2 = tag.GetBool("Stress_Buff_2");
            Stress_Current = tag.GetInt("Stress_Current");
        }
        public override void SaveData(TagCompound tag)
        {
            tag["Stress_Current"] = Stress_Current;
            tag["Stress_Buff_1"] = Stress_Buff_1;
            tag["Stress_Buff_2"] = Stress_Buff_2;
        }
        public override void Initialize()
        {
            Stress_Max = DefaultStress_Max;
        }

        public override void UpdateDead()
        {
            ResetVariables();
            Stress_Current = 0;
        }
        public override void PostUpdateMiscEffects()
        {
            Have_HeartAttack();
            Stress_Buffs();
        }

        private void ResetVariables()
        {
            Stress_Max2 = DefaultStress_Max;
        }
        public override void ResetEffects()
        {
            ResetVariables();

            DoomDash = false;
            Ink_Storm_Equipped = false;
            Meteor_Storm_Active = false;
            Tiara_Equipped = false;
            Mech_Equipped = false;
            Heart_Equipped = false;
            Boulder_Equipped = false;
            Aeritite_Equipped = false;
            NPC_Bleeding = false;

            if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
            {
                DashDir = DashRight;
            }
            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
            {
                DashDir = DashLeft;
            }
            else
            {
                DashDir = -1;
            }
        }
        private void Have_HeartAttack()
        {
            if (InfernusSystem.Level_systemON == false)
            {
                return;
            }
            if (Stress_Current >= Stress_Max)
            {
                Player.lifeRegen -= 200;
                Player.lifeRegenCount = -200;
            }
        }
        private void Stress_Buffs()
        {
            if (InfernusSystem.Level_systemON == false)
            {
                return;
            }
            if (Stress_Current >= 1)
            {
                if (Stress_Buff_1 == true)
                {
                    Player.moveSpeed += .12f;
                    Player.maxRunSpeed += .12f;
                    return;
                }
                Player.moveSpeed += .06f;
                Player.maxRunSpeed += .06f;
            }
            if (Stress_Current >= 3)
            {
                if (Stress_Buff_1 == true)
                {
                    Player.jumpSpeedBoost += .12f;
                    return;
                }
                Player.jumpSpeedBoost += .06f;
            }
            if (Stress_Current >= 5)
            {
                if (Stress_Buff_2 == true)
                {
                    Player.GetDamage(DamageClass.Generic) += .25f;
                    return;
                }
                Player.GetDamage(DamageClass.Generic) += .12f;
            }
            if (Stress_Current >= 7)
            {
                if (Stress_Buff_2 == true)
                {
                    Player.GetCritChance(DamageClass.Generic) += 35;
                    return;
                }
                Player.GetCritChance(DamageClass.Generic) += 20;
            }
        }

        public override void PreUpdateMovement()
        {
            if (CanUseDash() && DashDir != -1 && DashDelay == 0)
            {
                Vector2 newVelocity = Player.velocity;

                switch (DashDir)
                {
                    case DashLeft when Player.velocity.X > -DashVelocity:
                    case DashRight when Player.velocity.X < DashVelocity:
                        {
                            float dashDirection = DashDir == DashRight ? 1 : -1;
                            newVelocity.X = dashDirection * DashVelocity;
                            break;
                        }
                    default:
                        return;
                }

                DashDelay = DashCooldown;
                DashTimer = DashDuration;
                Player.velocity = newVelocity;

                SoundEngine.PlaySound(SoundID.NPCDeath14, Player.position);

            }

            if (DashDelay > 0)
                DashDelay--;

            if (DashTimer > 0)
            {
                DashTimer--;
            }
        }

        private bool CanUseDash()
        {
            return DoomDash
                && !Player.mount.Active;
        }
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {

            return new[] {
                new Item(ModContent.ItemType<Items.Consumable.StartingBag>(), 1)
            };
        }
    }
}