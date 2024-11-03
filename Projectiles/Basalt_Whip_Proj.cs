using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Basalt_Whip_Proj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Summon;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.penetrate = 1;
            Projectile.netImportant = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            Projectile.velocity.Y = Projectile.velocity.Y + 0.9f;
            Projectile.rotation += 0.2f * (float)Projectile.direction;
            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, (int)Projectile.Center.X, (int)Projectile.Center.Y, DustID.Ash, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Main.dust[dust].noGravity = true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                Vector2 speed = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 8, DustID.Ash, speed * 2, 0, default, Scale: 1f);
                wand.noGravity = true;
            }
        }
    }
}