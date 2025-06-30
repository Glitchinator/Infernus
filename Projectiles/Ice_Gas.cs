using Terraria;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Ice_Gas : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 110;
            Projectile.height = 110;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 120;

            DrawOffsetX = -15;
            DrawOriginOffsetY = -15;
        }
        int timer;

        public override void AI()
        {
            timer++;
            if (timer <= 20)
            {
                Projectile.scale += 0.1f;
            }
            if(timer >= 21)
            {
                Projectile.scale -= 0.1f;
            }
            if (timer == 39)
            {
                timer = 0;   
            }

            if (Projectile.timeLeft >= 30)
            {
                Projectile.alpha -= 5;
            }
            else
            {
                Projectile.alpha += 3;
                Projectile.hostile = false;
                return;
            }
            if (Projectile.alpha <= 10)
            {
                Projectile.hostile = true;
            }
        }
    }
}