using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles.Temporal_Glow_Squid.Drops
{

    public class Ink_EmergencyTyphoon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 120;
            Projectile.height = 320;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 540;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 16;
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

            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 15;
            }
        }
    }
}