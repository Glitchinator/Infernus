using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Squid_proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.MinionShot[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 78;
            Projectile.height = 116;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 16;
            Projectile.DamageType = DamageClass.Summon;
        }
        public override void AI()
        {

            Projectile.velocity.X = 0;
            Projectile.velocity.Y = 0;

            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Wraith, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Wraith, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);

            Projectile.velocity *= 1f;
            if (++Projectile.frameCounter >= 14)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }

            if (Projectile.timeLeft <= 25)
            {
                Projectile.alpha += 6;
                return;
            }

            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 15;
            }
        }
    }
}