using Infernus.Buffs;
using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Infernus.Items.Weapon.HardMode.Accessories;
using Infernus.Projectiles;
using Infernus.Projectiles.Temporal_Glow_Squid.Drops;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

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

        // aeritite shield variables
        public bool Aeritite_Shield_Equipped = false;
        public float Aeritite_DR = 0.33f;

        // equite armor variables
        public bool Equite_Equipped = false;
        public static int Equite_Amount = 0;
        public int Equite_Regen = 200;
        public static NPC Equite_target;

        public bool Equite_Accessory_Equipped = false;

        // Boulder Expert Variable
        public bool Boulder_Equipped = false;
        public int Boulder_Timer = 0;

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

        public bool Meteor_Whetstone = false;

        // Condensed iceicle
        public bool NPC_Iced = false;

        // quiver
        public bool Quiver_Equipped = false;

        // radinat squid heart
        public bool Squid_Heart = false;

        // whip heads
        public bool Ice_Whiphead = false;
        public bool Elemental_Whiphead = false;
        public bool Basalt_Whiphead = false;
        public bool Defiled_Whiphead = false;
        public bool Ancient_Whiphead = false;

        // Soul drinker
        public bool Soul_Drinker = false;

        // lectric'
        public bool Lectric = false;

        // Hellion spite
        public bool Hellion_Spite = false;
        public bool Rage = false;
        public int life_steal_timer = 0;

        // phantom blade
        public bool Phantom_Blade = false;

        // Magic Pouch
        public bool Magic_Pouch = false;

        public override void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
        {
            if (Player.statLife < Player.statLifeMax)
            {
                price = 100;
            }
        }
        public override void OnEnterWorld()
        {
            Stress_Current = 0;
            Equite_Amount = 0;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText("Welcome to Infernus Mod! If you have any questions the Discord is the place to ask." + "\nYou are playing on Infernus V1.6.7", GainXP_Resource);
            }
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                Main.NewText("Welcome to Infernus Mod! If you have any questions the Discord is the place to ask." + "\nYou are playing on Infernus V1.6.7", GainXP_Resource);
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Phantom_Blade == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center.X, target.Center.Y * 1.35f, Main.rand.Next(-1, 1), Main.rand.Next(-20, 10), ModContent.ProjectileType<Projectiles.Phantom>(), (int)(damageDone * 0.75f), 0, 0);
                }
            }
        }
        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (Quiver_Equipped == true && item.DamageType == DamageClass.Ranged)
            {
                velocity *= 1.2f;
            }
        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (Ink_Storm_Equipped == true)
            {
                if (Main.rand.Next(3) < 1)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<Ink_EmergencyTyphoon>(), 25, 0, 0);
                }
            }
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            if (InfernusNPC.Is_Spawned == true)
            {
                Stress_Current += 1;
            }
            if (Aeritite_Shield_Equipped == true && Player.HasBuff(ModContent.BuffType<Aeritite_Timer>()))
            {
                Player.ClearBuff(ModContent.BuffType<Aeritite_Timer>());
            }
            if (Aeritite_Equipped == true)
            {
                Player.AddBuff(ModContent.BuffType<Aeritite_Timer>(), 1800);
            }
            if(Player.HasBuff(ModContent.BuffType<Buffs.Rage_Cooldown>()) || Player.HasBuff(ModContent.BuffType<Buffs.Rage>()))
            {
                return;
            }
            if (Hellion_Spite == true && info.Damage >= 10)
            {
                Player.AddBuff(ModContent.BuffType<Buffs.Rage>(), 480);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int heal = (int)(damageDone * 0.07f);
            if (heal < 1)
            {
                heal = 1;
            }
            if (Rage == true && life_steal_timer == 0)
            {
                Player.Heal(heal);
                if(heal > 1)
                {
                    life_steal_timer = 30;
                }
            }
            if(Soul_Drinker == true)
            {
                if (Main.rand.Next(2) < 1)
                {
                    Player.AddBuff(ModContent.BuffType<Souldrinker_Buff>(), 120);
                }
            }
            if (Equite_Equipped == true && target.type != NPCID.TargetDummy)
            {
                Equite_target = target;
            }
            if (NPC_Iced == true && hit.DamageType == DamageClass.Ranged)
            {
                target.AddBuff(BuffID.Frostburn, 120);
            }
            if (Ice_Whiphead == true && hit.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.rand.Next(3) < 1)
                {
                    target.AddBuff(BuffID.Frostburn, 120);
                }
            }
            if (Lectric == true && hit.Crit == true)
            {
                for (int i = 0; i < 13; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center.X, target.Center.Y, Main.rand.Next(-10, 11), Main.rand.Next(-10, -5), ModContent.ProjectileType<Projectiles.Lectric>(), (int)(damageDone * 1.15f), 0, 0);
                }
            }
            if (Elemental_Whiphead == true && hit.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.rand.Next(3) < 1)
                {
                    target.AddBuff(BuffID.Frostburn2, 120);
                }
                if (Main.rand.Next(3) < 1)
                {
                    target.AddBuff(BuffID.OnFire3, 120);
                }
                if (Main.rand.Next(3) < 1)
                {
                    target.AddBuff(BuffID.Venom, 120);
                }
            }
            if (Basalt_Whiphead == true && hit.DamageType == DamageClass.SummonMeleeSpeed)
            {
                for (int i = 0; i < 5; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center.X, target.Center.Y / 1.35f, Main.rand.Next(-2, 3), Main.rand.Next(-2, 2), ModContent.ProjectileType<Basalt_Whip_Proj>(), (int)(damageDone * 0.5f), 0, 0);
                }
            }
            if (Ancient_Whiphead == true && hit.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.rand.Next(5) < 1)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center.X, target.Center.Y, Main.rand.Next(-2, 3), Main.rand.Next(-2, -2), ModContent.ProjectileType<Bone_Whip>(), (int)(damageDone * 0.5f), 0, 0);
                }
            }
            if (Defiled_Whiphead == true && hit.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.rand.Next(3) < 1)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center.X, target.Center.Y, 0, 0, ModContent.ProjectileType<Sandstorm_Whip>(), (int)(damageDone * 0.6f), 0, 0);
                }
            }
            if (Magic_Pouch == true && hit.DamageType == DamageClass.Magic)
            {
                if (Main.rand.Next(20) < 1)
                {
                    Item.NewItem(Entity.GetSource_OnHit(target), new Vector2(target.Center.X, target.Center.Y), ItemID.Star, 1);
                }
            }
            if (Meteor_Whetstone == true)
            {
                if (Main.rand.Next(5) < 1)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center.X, target.Center.Y, 0, 0, ModContent.ProjectileType<Meteor_Whetstone_Explosion>(), (int)(damageDone * 0.5f), 0, 0);
                }
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
            if (Squid_Heart == true && modifiers.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.rand.Next(10) < 1)
                {
                    for (int k = 0; k < 13; k++)
                    {
                        Vector2 speed = Main.rand.NextVector2Circular(0.5f, 1f);
                        Dust Sword = Dust.NewDustPerfect(target.Center + speed * 32, DustID.Vortex, speed * 3, Scale: 2f);
                        Sword.noGravity = true;
                    }
                    modifiers.SourceDamage *= 1.65f;
                }
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
            Stress_Current = 0;
            ResetVariables();
            Stress_Current = 0;
        }
        public override void PostUpdateMiscEffects()
        {
            if (life_steal_timer > 0)
            {
                life_steal_timer--;
            }
            
            Have_HeartAttack();
            Stress_Buffs();

            if(Aeritite_Shield_Equipped == true)
            {
                Aeritite_DR = 0.25f;
            }
            else
            {
                Aeritite_DR = 0.33f;
            }

            if (Equite_Equipped == true && Equite_Accessory_Equipped == true)
            {
                Player.statManaMax2 += 80;
                Player.maxMinions += 2;
                Player.GetArmorPenetration(DamageClass.Generic) += 1;
            }

            if(Equite_Amount < 4 && Equite_Equipped == true)
            {
                Equite_Regen--;
            }
            if(Equite_Regen == 0 && Equite_Equipped == true)
            {
                SoundEngine.PlaySound(SoundID.MaxMana, Player.position);
                for (int k = 0; k < 12; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(0.3f, 1f);
                    Dust Sword = Dust.NewDustPerfect(Player.Center + speed * 32, DustID.SandstormInABottle, speed * 3, Scale: 1.4f);
                    Sword.noGravity = true;
                }
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Player.Center.X, Player.Center.Y, 0, 0, ModContent.ProjectileType<Equite_Leaf>(), 75, 5f, Player.whoAmI);
                Equite_Amount++;
                Equite_Regen = 200;
            }
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
            Equite_Equipped = false;
            NPC_Bleeding = false;
            Equite_Accessory_Equipped = false;
            Aeritite_Shield_Equipped = false;
            NPC_Iced = false;
            Quiver_Equipped = false;
            Meteor_Whetstone = false;
            Squid_Heart = false;
            Ice_Whiphead = false;
            Elemental_Whiphead = false;
            Basalt_Whiphead = false;
            Defiled_Whiphead = false;
            Ancient_Whiphead = false;
            Soul_Drinker = false;
            Lectric = false;
            Hellion_Spite = false;
            Phantom_Blade = false;
            Rage = false;
            Magic_Pouch = false;

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