using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Sandstorm_Whip : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Summon;
            Projectile.width = 94;
            Projectile.height = 98;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 16;
            Projectile.timeLeft = 90;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            for (int k = 0; k < 7; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Sandnado, Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
            }
            Projectile.rotation += (float)Projectile.direction * 7;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.85f);
        }
    }
}