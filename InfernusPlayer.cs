using Infernus.Buffs;
using Infernus.Buffs.Whip_Debuffs;
using Infernus.Items.Accesories;
using Infernus.Items.Materials;
using Infernus.Items.Tools;
using Infernus.Items.Weapon.HardMode.Accessories;
using Infernus.Level;
using Infernus.Projectiles;
using Infernus.Projectiles.Temporal_Glow_Squid.Drops;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
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
        public static int Max_Equite_Amount = 4;
        public int Equite_Amount = 0;
        public int Equite_Regen = 140;
        public bool Equite_Hit = false;
        public int Equite_Cooldown = 0;
        public NPC Equite_Target;

        public bool Equite_Emblem_Equipped = false;
        public bool Equite_Charm_Equipped = false;
        public bool Equite_Knuckles_Equipped = false;
        public bool Equite_Quiver_Equipped = false;

        // Boulder Expert Variable
        public bool Boulder_Equipped = false;
        public int Boulder_Timer = 0;

        // Meteor ring variables
        public bool Meteor_Storm_Active = false;

        // Snowfall tiara variables
        public bool Tiara_Equipped = false;

        // mechanical mind equipped
        public bool Mech_Equipped = false;
        public int Mech_Timer = 0;

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

        // cryo Gauntlet
        public bool Cryo_Gauntlet = false;

        // Sentinel Battery
        public bool Sentinel_Battery = false;

        // Aerodynamics
        public bool Aerodynamics = false;

        // Corrupted Mask
        public bool Corrupted_Mask_Equipped = false;

        // Mana Syphoner
        public bool Mana_Syphoner = false;

        // Bayonett
        public bool Bayonett = false;
        public bool Cursed_Bayonett = false;

        // Iceicle Necklace
        public bool Iceicle_Necklace = false;
        public int Iceicle_Necklace_Timer = 0;

        // Plasma Splash
        public bool Plasma_Splash = false;

        // Burning Grasp
        public bool Burning_Grasp = false;
        public int Burning_Grasp_Timer = 0;

        // enchanted femur
        public bool Enchanted_Femur = false;

        //meteor core
        public bool Meteor_Core = false;

        // ammo pouch
        public bool Ammo_Pouch = false;

        // ink cartridge 
        public bool Ink_Cartridge = false;

        // Archaic Squid Scroll
        public bool Squid_Sroll = false;
        public int Squid_Scroll_Amount = 0;

        // extra barrel
        public bool Extra_Barrel = false;

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
                Main.NewText("Welcome to Infernus Mod! If you have any questions the Discord is the place to ask." + "\nYou are playing on Infernus V2.0", GainXP_Resource);
                /*
                if (Joined_Infernus == false)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 49; inventoryIndex++)
                    {
                        if (Player.inventory[inventoryIndex].type == ItemID.None)
                        {
                            Player.inventory[inventoryIndex].SetDefaults(ModContent.ItemType<Items.Consumable.StartingBag>());
                            Joined_Infernus = true;
                            break;
                        }
                        else
                        {
                            if (inventoryIndex == 49)
                            {
                                Item.NewItem(Entity.GetSource_DropAsItem(), Player.Center, ModContent.ItemType<Items.Consumable.StartingBag>(), 1);
                                Joined_Infernus = true;
                            }
                        }
                    }
                }
                */
            }
            if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("Welcome to Infernus Mod! If you have any questions the Discord is the place to ask." + "\nYou are playing on Infernus V1.6.7"), GainXP_Resource, Player.whoAmI);
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
        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (Ammo_Pouch == true)
            {
                if (Main.rand.Next(8) < 1)
                {
                    return false;
                }
            }
            return base.CanConsumeAmmo(weapon, ammo);
        }
        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (Quiver_Equipped == true && item.DamageType == DamageClass.Ranged)
            {
                velocity *= 1.2f;
            }
            if (Extra_Barrel == true && item.DamageType == DamageClass.Ranged && item.ammo == AmmoID.Bullet)
            {
                damage = (int)(damage * 0.5f);
                knockback = (int)(knockback * 0.5f);
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(12));

                velocity = newVelocity;
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
            if (Hellion_Spite == true && info.Damage >= 10 && (!Player.HasBuff(ModContent.BuffType<Buffs.Rage_Cooldown>()) && !Player.HasBuff(ModContent.BuffType<Buffs.Rage>())))
            {
                Player.AddBuff(ModContent.BuffType<Buffs.Rage>(), 480);
            }
            if (Enchanted_Femur == true && info.Damage >= 10 && !Player.HasBuff(ModContent.BuffType<Buffs.Enchanted_Femur_Cooldown>()))
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Player.Center, Vector2.Zero, ModContent.ProjectileType<Ink_EmergencyTyphoon>(), 25, 2f);
                Player.AddBuff(ModContent.BuffType<Buffs.Enchanted_Femur_Cooldown>(), 240);
            }
            if (Meteor_Core == true && info.Damage >= 10)
            {
                Player.AddBuff(ModContent.BuffType<Buffs.Meteor_Core_Buff>(), 480);
            }
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Ink_Cartridge == true && item.DamageType == DamageClass.Ranged)
            {
                if (Main.rand.Next(4) < 1)
                {
                    Projectile.NewProjectile(source, position, velocity * 0.8f, ModContent.ProjectileType<Projectiles.Temporal_Glow_Squid.Drops.Ink_Flamethrower>(), (int)(damage * 0.5f), 0, 0);
                }
            }
            if (Extra_Barrel == true && item.DamageType == DamageClass.Ranged)
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
            }
            if (Iceicle_Necklace == true && item.DamageType == DamageClass.Ranged && Iceicle_Necklace_Timer == 0)
            {
                Projectile.NewProjectile(source, position, velocity * 0.8f, ModContent.ProjectileType<Projectiles.Iceicle_Necklace_Shot>(), (int)(damage * 0.75f), 0, 0);
                Iceicle_Necklace_Timer = 120;
            }
            if (Burning_Grasp == true && item.DamageType == DamageClass.Ranged && Burning_Grasp_Timer == 0)
            {
                Projectile.NewProjectile(source, position, velocity * 0.8f, ModContent.ProjectileType<Projectiles.Shuriken_HellFire>(), (int)(damage * 0.75f), 0, 0);
                Burning_Grasp_Timer = 120;
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }
        public override async void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
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
            if (Basalt_Whiphead == true && hit.DamageType == DamageClass.SummonMeleeSpeed)
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center.X, target.Center.Y / 1.35f, Main.rand.Next(-2, 3), Main.rand.Next(-2, 2), ModContent.ProjectileType<Basalt_Whip_Proj>(), (int)(damageDone * 0.5f), 0, 0);
                }
            }
            /*
            if (Ancient_Whiphead == true && hit.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.rand.Next(5) < 1)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center.X, target.Center.Y, Main.rand.Next(-2, 3), Main.rand.Next(-2, -2), ModContent.ProjectileType<Bone_Whip>(), (int)(damageDone * 0.5f), 0, 0);
                }
            }
            */
            if (Defiled_Whiphead == true && hit.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.rand.Next(5) < 1)
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
            if (Equite_Equipped == true && Equite_Amount > 0 && Equite_Cooldown == 0 && target.type != NPCID.TargetDummy)
            {
                if (Equite_Amount > 0)
                {
                    Equite_Hit = true;
                    Equite_Cooldown = 30;
                    Equite_Target = target;
                }
                else 
                {
                    Equite_Hit = false;
                    Equite_Target = null;
                }

            }
            if (Cryo_Gauntlet == true && hit.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.rand.Next(5) < 1)
                {
                    Player.AddBuff(ModContent.BuffType<Cryo_Buff>(), 180);
                }
            }
            if (Corrupted_Mask_Equipped == true && target.type != NPCID.TargetDummy && target.lifeMax > 10 && Player.HasBuff(ModContent.BuffType<Corrupt_Cooldown>()) == false)
            {
                if (Main.rand.Next(10) < 1)
                {
                    if (target.GetGlobalNPC<EffectNPC>().Haunted_Status.Y == 0 && EffectNPC.Haunted_NPC == null)
                    {
                        Player.AddBuff(ModContent.BuffType<Corrupt_Cooldown>(), 1200);
                        target.GetGlobalNPC<EffectNPC>().Haunted_Status = new Vector2(0, 900);
                        EffectNPC.Haunted_NPC = target;
                    }
                }
            }
            if (Mana_Syphoner == true && hit.DamageType == DamageClass.Magic)
            {
                Vector2 h = Player.Center - target.Center;
                var destnorm = h.SafeNormalize(Vector2.UnitY);
                if (h.Length() <= 300f)
                {
                    Dust.QuickDustLine(Player.Center + destnorm * Player.width, target.Center, target.Center.Length() / 20f, Color.Purple);
                    Player.statMana += (int)(damageDone * 0.1f);
                }
            }
            if (Plasma_Splash == true && hit.DamageType == DamageClass.Ranged && hit.Crit == true)
            {
                var damage = (int)(damageDone * 0.3f);
                var proj = ModContent.ProjectileType<Flour_Homing>();
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, new Vector2(-7, 0), proj, damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, new Vector2(7, 0), proj, damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, new Vector2(0, 7), proj, damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, new Vector2(0, -7), proj, damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, new Vector2(-7, -7), proj, damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, new Vector2(7, -7), proj, damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, new Vector2(-7, 7), proj, damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, new Vector2(7, 7), proj, damage, 3f, Main.myPlayer);
            }
            if (Squid_Sroll == true && hit.DamageType == DamageClass.Magic)
            {
                Item.NewItem(Entity.GetSource_OnHit(target), new Vector2(target.Center.X, target.Center.Y), ModContent.ItemType<HP_Pickup>(), 1);
            }
        }
        /*
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (npc.GetGlobalNPC<EffectNPC>().Haunted == true)
            {
                modifiers.SourceDamage += 0.3f;
            }
        }
        */
        public override void GetHealMana(Item item, bool quickHeal, ref int healValue)
        {
            if (Squid_Sroll == true && Squid_Scroll_Amount >= 10)
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<Ink_EmergencyTyphoon>(), 25, 2f);
                Squid_Scroll_Amount = 0;
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
            if (Ancient_Whiphead == true && modifiers.DamageType == DamageClass.SummonMeleeSpeed)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Whip_Debuffs.Ancient_Whiphead>(), 300);
            }
            if (Basalt_Whiphead == true && modifiers.DamageType == DamageClass.SummonMeleeSpeed)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Whip_Debuffs.Basalt_Whiphead>(), 300);
            }
            if (Squid_Heart == true && modifiers.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.rand.Next(6) < 1)
                {
                    for (int k = 0; k < 19; k++)
                    {
                        Vector2 speed = Main.rand.NextVector2Circular(1f, 2f);
                        Dust Sword = Dust.NewDustPerfect(target.Center + speed * 32, DustID.Vortex, speed * 9, Scale: 3f);
                        Sword.noGravity = true;
                    }
                    modifiers.SourceDamage *= 2.1f;
                    target.AddBuff(ModContent.BuffType<Radiant_Squid_Buff>(), 300);
                    SoundEngine.PlaySound(SoundID.Item110, target.position);
                }
            }
            if (Bayonett == true && modifiers.DamageType == DamageClass.Ranged)
            {
                Vector2 h = Player.Center - target.Center;
                string g = h.ToString();
                if (h.Length() <= 200f)
                {
                    modifiers.ScalingBonusDamage += 0.15f;
                    Main.NewText(g + "15%", 229, 214, 127);
                }
                else if (h.Length() <= 400f)
                {
                    modifiers.ScalingBonusDamage += 0.1f;
                    Main.NewText(g + "10%", 229, 214, 127);
                }
                else if (h.Length() <= 600f)
                {
                    modifiers.ScalingBonusDamage += 0.05f;
                    Main.NewText(g + "5%", 229, 214, 127);
                }
            }
            if (Cursed_Bayonett == true && modifiers.DamageType == DamageClass.Ranged)
            {
                Vector2 h = Player.Center - target.Center;
                string g = h.ToString();
                if (h.Length() <= 200f)
                {
                    modifiers.ScalingBonusDamage += 0.25f;
                    Main.NewText(g + "25%", 229, 214, 127);
                }
                else if (h.Length() <= 400f)
                {
                    modifiers.ScalingBonusDamage += 0.2f;
                    Main.NewText(g + "20%", 229, 214, 127);
                }
                else if (h.Length() <= 600f)
                {
                    modifiers.ScalingBonusDamage += 0.15f;
                    Main.NewText(g + "15%", 229, 214, 127);
                }
                else if (h.Length() <= 800f)
                {
                    modifiers.ScalingBonusDamage += 0.1f;
                    Main.NewText(g + "10%", 229, 214, 127);
                }
                else if (h.Length() <= 1000f)
                {
                    modifiers.ScalingBonusDamage += 0.05f;
                    Main.NewText(g + "5%", 229, 214, 127);
                }
            }
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
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Player.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }
        public override void PostUpdateMiscEffects()
        {
            if (Mech_Equipped == true)
            {
                float maxDetectRadius = 400f;
                float projSpeed = 19f;
                NPC closestNPC = FindClosestNPC(maxDetectRadius);
                if (closestNPC == null)
                {
                    Mech_Timer = 0;
                    return;
                }
                Mech_Timer++;
                if (Mech_Timer >= 60)
                {
                    int x = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Player.Center, (closestNPC.Center - Player.Center).SafeNormalize(Vector2.Zero) * projSpeed, ModContent.ProjectileType<Lazar>(), 60, 6f, Main.myPlayer);
                    Main.projectile[x].DamageType = DamageClass.Generic;
                    Mech_Timer = 0;
                }
            }
            if (Mana_Syphoner == true)
            {
                for (int i = 0; i < 24; i++)
                {
                    Vector2 dust_v = Player.Center - new Vector2(300, 0).RotatedBy(i * Math.PI * 2 / 24f) - Player.Center;
                    var dest = dust_v.SafeNormalize(Vector2.Zero);
                    Dust h = Dust.NewDustPerfect(Player.Center + new Vector2(300, 0).RotatedBy(i * Math.PI * 2 / 24f), DustID.PurpleCrystalShard, dest * 20f);
                    h.noGravity = true;
                    h.noLight = true;
                }

            }
            if (Iceicle_Necklace_Timer > 0)
            {
                Iceicle_Necklace_Timer--;
            }
            if(Burning_Grasp_Timer > 0)
            {
                Burning_Grasp_Timer--;
            }
            if (life_steal_timer > 0)
            {
                life_steal_timer--;
            }
            if(Equite_Cooldown > 0)
            {
                Equite_Cooldown--;
            }
            if (Equite_Equipped == false)
            {
                Equite_Amount = 0;
            }
            if (Squid_Sroll == false)
            {
                Squid_Scroll_Amount = 0;
            }

            if (Aerodynamics == true)
            {
                Player.wingTimeMax += 50;
                Player.jumpSpeedBoost += 2f;
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

            if (Equite_Equipped == true && Equite_Emblem_Equipped == true)
            {
                Player.maxMinions += 2;
            }

            if(Equite_Amount < Max_Equite_Amount && Equite_Equipped == true)
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
                Equite_Amount++;
                int i = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Player.Center.X, Player.Center.Y, 0, 0, ModContent.ProjectileType<Equite_Leaf>(), 60, 5f, Player.whoAmI);
                Main.projectile[i].alpha = Equite_Amount;
                Equite_Regen = 140;
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
            Equite_Emblem_Equipped = false;
            Equite_Charm_Equipped = false;
            Equite_Knuckles_Equipped = false;
            Equite_Quiver_Equipped = false;
            Aeritite_Shield_Equipped = false;
            NPC_Iced = false;
            Quiver_Equipped = false;
            Meteor_Whetstone = false;
            Squid_Heart = false;
            Ice_Whiphead = false;
            Basalt_Whiphead = false;
            Defiled_Whiphead = false;
            Ancient_Whiphead = false;
            Soul_Drinker = false;
            Lectric = false;
            Hellion_Spite = false;
            Phantom_Blade = false;
            Rage = false;
            Magic_Pouch = false;
            Cryo_Gauntlet = false;
            Sentinel_Battery = false;
            Aerodynamics = false;
            Corrupted_Mask_Equipped = false;
            Mana_Syphoner = false;
            Bayonett = false;
            Cursed_Bayonett = false;
            Iceicle_Necklace = false;
            Plasma_Splash = false;
            Burning_Grasp = false;
            Enchanted_Femur = false;
            Meteor_Core = false;
            Ammo_Pouch = false;
            Ink_Cartridge = false;
            Squid_Sroll = false;
            Extra_Barrel = false;

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