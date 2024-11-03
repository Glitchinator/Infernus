using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles.Temporal_Glow_Squid.Drops
{

    public class Ink_Flamethrower : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.height = 12;
            Projectile.width = 12;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.timeLeft = 22;
            Projectile.penetrate = 6;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Wraith, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 1.5f);
            Main.dust[dust].noGravity = true;
        }
    }
}