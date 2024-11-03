using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Shark_Typhoon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 240;
            Projectile.height = 240;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2400;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;

            DrawOffsetX = -40;
            DrawOriginOffsetY = -40;
        }
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation += (float)Projectile.direction * 50;

            Projectile.velocity.X = Projectile.velocity.X * .98f;
            Projectile.velocity.Y = Projectile.velocity.Y * .98f;

            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.WaterCandle, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 2.3f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.IcyMerman, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 2.3f);
            }


            Projectile.velocity *= 1f;
            if (++Projectile.frameCounter >= 14)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }

            if (Projectile.timeLeft <= 220)
            {
                Projectile.alpha += 1;
                Projectile.hostile = false;
                return;
            }
            if (Projectile.alpha <= 0)
            {
                Projectile.hostile = true;
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 3;
            }
        }
    }
}