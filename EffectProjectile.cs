using Infernus.Buffs;
using Infernus.NPCs;
using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus
{
    public class EffectProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        bool Arrow = false;
        bool Bullet = false;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.type == ProjectileID.WoodenArrowFriendly || projectile.type == ProjectileID.FlamingArrow || projectile.type == ProjectileID.UnholyArrow || projectile.type == ProjectileID.JestersArrow || projectile.type == ProjectileID.HellfireArrow || projectile.type == ProjectileID.HolyArrow || projectile.type == ProjectileID.CursedArrow || projectile.type == ProjectileID.FrostburnArrow || projectile.type == ProjectileID.ChlorophyteArrow || projectile.type == ProjectileID.IchorArrow || projectile.type == ProjectileID.VenomArrow || projectile.type == ProjectileID.BoneArrow || projectile.type == ProjectileID.MoonlordArrow || projectile.type == ProjectileID.MoonlordArrowTrail || projectile.type == ProjectileID.ShimmerArrow || projectile.type == ModContent.ProjectileType<Aeritite_Arrow>() || projectile.type == ModContent.ProjectileType<Equite_Arrow>() || projectile.type == ModContent.ProjectileType<Basalt_Arrow>() || projectile.type == ProjectileID.BloodArrow || projectile.type == ProjectileID.FrostArrow || projectile.type == ProjectileID.PulseBolt || projectile.type == ProjectileID.BeeArrow || projectile.type == ProjectileID.FairyQueenRangedItemShot || projectile.type == ProjectileID.Hellwing || projectile.type == ProjectileID.ShadowFlameArrow || projectile.type == ProjectileID.DD2BetsyArrow || projectile.type == ProjectileID.Phantasm || projectile.type == ModContent.ProjectileType<Missle_Bow>() || projectile.type == ModContent.ProjectileType<Lightning_Arrow>() || projectile.type == ModContent.ProjectileType<Raiko_Bow_Arrow>())
            {
                Arrow = true;
            }
            if (projectile.type == ProjectileID.Bullet || projectile.type == ProjectileID.MeteorShot || projectile.type == ProjectileID.SilverBullet || projectile.type == ProjectileID.CrystalBullet || projectile.type == ProjectileID.CursedBullet || projectile.type == ProjectileID.ChlorophyteBullet || projectile.type == ProjectileID.BulletHighVelocity || projectile.type == ProjectileID.IchorBullet || projectile.type == ProjectileID.VenomBullet || projectile.type == ProjectileID.PartyBullet || projectile.type == ProjectileID.NanoBullet || projectile.type == ProjectileID.ExplosiveBullet || projectile.type == ProjectileID.GoldenBullet || projectile.type == ProjectileID.MoonlordBullet || projectile.type == ModContent.ProjectileType<Ruderibus_Rounds>() || projectile.type == ModContent.ProjectileType<Rocky_Rounds>() || projectile.type == ModContent.ProjectileType<Rocky_Rounds_Proj>() || projectile.type == ModContent.ProjectileType<SandySlug>() || projectile.type == ModContent.ProjectileType<Lazar>() || projectile.type == ModContent.ProjectileType<Bullet_Rocket_Prime>() || projectile.type == ModContent.ProjectileType<Pump_Fire_Slug>() || projectile.type == ModContent.ProjectileType<Bullet_Rocket>())
            {
                Bullet = true;
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(Bullet == true)
            {
                if (target.HasBuff(ModContent.BuffType<Cryo_Necklace_Debuff>()))
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, Vector2.Zero, ModContent.ProjectileType<Iceicle_Necklace_exlos>(), projectile.damage, 4f, projectile.owner);
                    int buff_id = target.FindBuffIndex(ModContent.BuffType<Cryo_Necklace_Debuff>());
                    target.DelBuff(buff_id);
                }
                Player player = Main.player[projectile.owner];
                if (player.GetModPlayer<InfernusPlayer>().Tainted_Clip == true)
                {
                    target.AddBuff(ModContent.BuffType<Tainted_Clip_Debuff>(), 179);
                }
            }
            if (Arrow == true)
            {
                Player player = Main.player[projectile.owner];
                if (player.GetModPlayer<InfernusPlayer>().NPC_Iced == true)
                {
                    target.AddBuff(BuffID.Frostburn, 120);
                }

                if (target.HasBuff(ModContent.BuffType<Burning_Grasp_Debuff>()))
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, Vector2.Zero, ModContent.ProjectileType<Hellfire_exlos>(), projectile.damage, 4f, projectile.owner);
                    int buff_id = target.FindBuffIndex(ModContent.BuffType<Burning_Grasp_Debuff>());
                    target.DelBuff(buff_id);
                }
            }
        }
    }
}
