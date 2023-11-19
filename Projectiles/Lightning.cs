using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Lightning : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.FrostburnArrow);
            AIType = ProjectileID.FrostburnArrow;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.netImportant = true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.Frostburn2, 300);
            }

            {
                for (int i = 0; i < 2; i++)
                {
                    int a = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center.X, target.Center.Y, Main.rand.Next(-10, 11) * 1f, Main.rand.Next(-10, -5) * .25f, ProjectileID.ThunderSpearShot, (int)(damageDone * .25f), 0, Projectile.owner);
                    Main.projectile[a].tileCollide = true;
                    Main.projectile[a].penetrate = 1;
                }
            }
            Projectile.Kill();
        }
    }
}