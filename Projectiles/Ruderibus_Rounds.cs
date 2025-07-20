using Microsoft.Xna.Framework;
using System.Media;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Ruderibus_Rounds : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.netImportant = true;
            Projectile.timeLeft = 400;
            Projectile.penetrate = 2;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 7;
            }
            if (Projectile.timeLeft <= 40)
            {
                Projectile.velocity.X = Projectile.velocity.X * .75f;
                Projectile.velocity.Y = Projectile.velocity.Y * .75f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 40;
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Ruderibus_Round_exlos>(), (int)(Projectile.damage * .25f), 4f, Projectile.owner);
        }
    }
}