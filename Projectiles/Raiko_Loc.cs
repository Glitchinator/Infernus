using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Raiko_Loc: ModProjectile
    {
        public override string Texture => "Infernus/Projectiles/Meteor_Flail";
        public override void SetDefaults()
        {
            Projectile.friendly = false;
            Projectile.height = 1;
            Projectile.width = 1;
            Projectile.hostile = false;
            Projectile.extraUpdates = 60;
            Projectile.timeLeft = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projectilePosition = Projectile.position;
                    projectilePosition -= Projectile.velocity * (i * 0.25f);
                    Projectile.alpha = 255;
                    int dust = Dust.NewDust(projectilePosition, 1, 1, DustID.SolarFlare, 0f, 0f, 0, default, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.016f;
                    Main.dust[dust].velocity *= 0.2f;
                }
            }
        }
    }
}