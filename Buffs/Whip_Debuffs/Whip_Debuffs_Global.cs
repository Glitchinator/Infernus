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
                modifiers.FlatBonusDamage += drillwhipbuff.TagDamage * projTagMultiplier;
                if (Main.rand.NextBool(3))
                {
                    for (int k = 0; k < 6; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(npc.Center + speed2 * 16, DustID.Electric, speed2 * 2, Scale: 1f);
                        wand.noGravity = true;
                    }
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), npc.Right.X, npc.Right.Y / 1.25f, 0, 10, ModContent.ProjectileType<Lightning_Summon_Shot>(), 110, 0, projectile.owner);
                }
            }
            if (npc.HasBuff<gemwhipbuff>())
            {
                modifiers.FlatBonusDamage += gemwhipbuff.TagDamage * projTagMultiplier;
            }
            if (npc.HasBuff<icewhipbuff>())
            {
                modifiers.FlatBonusDamage += icewhipbuff.TagDamage * projTagMultiplier;
            }
            if (npc.HasBuff<rockywhipbuff>())
            {
                modifiers.FlatBonusDamage += rockywhipbuff.TagDamage * projTagMultiplier;
            }
            if (npc.HasBuff<Whipbuff>())
            {
                modifiers.FlatBonusDamage += Whipbuff.TagDamage * projTagMultiplier;
                modifiers.Knockback += Whipbuff.TagKnockBack * projTagMultiplier;
            }
        }
    }
}
