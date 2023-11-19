using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Flour : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 92;
            Projectile.height = 102;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            Projectile.rotation += (float)Projectile.direction * 2;
            Projectile.velocity.X = Projectile.velocity.X * 1.02f;
            Projectile.velocity.Y = Projectile.velocity.Y * 1.02f;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_KoboldExplosion, Projectile.position);

            for (int k = 0; k < 34; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 1f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, 72, speed * 3, Scale: 2f);
                Sword.noGravity = true;
            }
            for (int k = 0; k < 34; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(1f, .5f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, 72, speed * 3, Scale: 2f);
                Sword.noGravity = true;
            }
        }
    }
}