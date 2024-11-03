using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Boulder_Orbit : ModProjectile
    {
        public sealed override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 82;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.height = 82;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 11;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<Boulder_Buff>());
            }
            if (player.HasBuff(ModContent.BuffType<Boulder_Buff>()))
            {
                Projectile.timeLeft = 2;
            }

            Projectile.damage = 36;

            Projectile.Center = player.Center;

            Projectile.rotation += (float)Projectile.direction * 2;

            //starts effect at 50MPH so velocity is like 5 times less MPH?

            if (player.velocity.X >= 10)
            {
                if (Main.rand.NextBool(3))
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Boulder_Orbit_Proj>(), Projectile.damage, 0, Projectile.owner);
                }
            }
            if (player.velocity.X <= -10)
            {
                if (Main.rand.NextBool(3))
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Boulder_Orbit_Proj>(), Projectile.damage, 0, Projectile.owner);
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(target.type != NPCID.TargetDummy)
            {
                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-9, 8), Main.rand.Next(-9, 8), ModContent.ProjectileType<Boulder_Orbit_Proj>(), Projectile.damage * 2, 0, Projectile.owner);
                }
            }
        }
    }
}