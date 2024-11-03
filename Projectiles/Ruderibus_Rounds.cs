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
        public bool hit3 = false;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.netImportant = true;
            Projectile.timeLeft = 400;
            Projectile.penetrate = 2;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.timeLeft <= 120)
            {
                Projectile.velocity.X = Projectile.velocity.X * .75f;
                Projectile.velocity.Y = Projectile.velocity.Y * .75f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 120;
            hit3 = true;
        }
        public override void OnKill(int timeLeft)
        {
            if(hit3 == true)
            {
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                for (int k = 0; k < 7; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Unit();
                    Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 12, DustID.HallowedPlants, speed * 2, 0, default, Scale: 1.3f);
                    wand.noGravity = true;
                }
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.X, ModContent.ProjectileType<Aeritite_Mine_Explosion>(), (int)(Projectile.damage * .3f), 0, Projectile.owner);

            }
        }
    }
}