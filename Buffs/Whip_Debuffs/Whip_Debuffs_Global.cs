using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs.Whip_Debuffs
{
    public class Whip_Debuffs_Global : GlobalNPC
    {
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.npcProj || projectile.trap || !projectile.IsMinionOrSentryRelated)
                return;


            var projTagMultiplier = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
            if (npc.HasBuff<aerwhipbuff>())
            {
                modifiers.FlatBonusDamage += aerwhipbuff.TagDamage * projTagMultiplier;
            }
            if (npc.HasBuff<drillwhipbuff>())
            {
                modifiers.FlatBonusDamage += drillwhipbuff.TagDamage * projTagMultiplier;
            }
            if (npc.HasBuff<elewhipbuff>())
            {
                modifiers.FlatBonusDamage += elewhipbuff.TagDamage * projTagMultiplier;
            }
            if (npc.HasBuff<gemwhipbuff>())
            {
                modifiers.FlatBonusDamage += gemwhipbuff.TagDamage * projTagMultiplier;
                if (npc.HasBuff<Gem_Whip_Cooldown>() == false)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), npc.Center, Main.rand.NextVector2Unit() * 4, ModContent.ProjectileType<Whip_Gem_proj>(), projectile.damage, 2f,projectile.owner);
                    }
                    npc.AddBuff(ModContent.BuffType<Gem_Whip_Cooldown>(), 80);
                }
            }
            if (npc.HasBuff<Ancient_Whiphead>())
            {
                modifiers.FlatBonusDamage += Ancient_Whiphead.TagDamage * projTagMultiplier;
            }
            if (npc.HasBuff<Basalt_Whiphead>())
            {
                modifiers.FlatBonusDamage += Basalt_Whiphead.TagDamage * projTagMultiplier;
            }
            if (npc.HasBuff<icewhipbuff>())
            {
                modifiers.FlatBonusDamage += icewhipbuff.TagDamage * projTagMultiplier;
                if (npc.HasBuff<Ice_Whip_Cooldown>() == false)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(npc.Center + speed2 * 16, DustID.HallowedPlants, speed2 * 2, Scale: 1.4f);
                        wand.noGravity = true;
                    }
                    for (int k = 0; k < 3; k++)
                    {
                        int h = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), npc.Center, Main.rand.NextVector2Unit() * 4, ModContent.ProjectileType<Ice_Slash_Summon>(), (int)(projectile.damage * 0.5f), 0, projectile.owner);
                        Main.projectile[h].DamageType = DamageClass.Summon;
                        Main.projectile[h].ai[0] = 15;
                    }
                    npc.AddBuff(ModContent.BuffType<Ice_Whip_Cooldown>(), 80);
                }
            }
            if (npc.HasBuff<rockywhipbuff>())
            {
                modifiers.FlatBonusDamage += rockywhipbuff.TagDamage * projTagMultiplier;
                if (npc.HasBuff<Rock_Whip_Cooldown>() == false)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(npc.Center + speed2 * 16, DustID.Stone, speed2 * 2, Scale: 1.4f);
                        wand.noGravity = true;
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        int h = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), npc.Center, Main.rand.NextVector2Unit() * 4, ModContent.ProjectileType<BoulderMini_Summon>(), (int)(projectile.damage * 0.5f), 0, projectile.owner);
                        Main.projectile[h].DamageType = DamageClass.Summon;
                        //Main.projectile[h].ai[0] = 15;
                    }
                    npc.AddBuff(ModContent.BuffType<Rock_Whip_Cooldown>(), 80);
                }
            }
            if (npc.HasBuff<Whipbuff>())
            {
                modifiers.FlatBonusDamage += Whipbuff.TagDamage * projTagMultiplier;
                modifiers.Knockback += Whipbuff.TagKnockBack * projTagMultiplier;
            }
            if (npc.HasBuff<Equite_Tag_Debuff>())
            {
                modifiers.FlatBonusDamage += Equite_Tag_Debuff.TagDamage * projTagMultiplier;
                modifiers.Knockback += Equite_Tag_Debuff.TagKnockBack * projTagMultiplier;
            }
            if (npc.HasBuff<Radiant_Squid_Buff>())
            {
                if (Main.rand.Next(10) < 1)
                {
                    modifiers.SetCrit();
                }
            }
        }
    }
}
