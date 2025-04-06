using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class LeadPipe_Proj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.height = 6;
            Projectile.width = 6;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 4;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Main.rand.NextBool(3))
            {
                int i = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Iron, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
                Main.dust[i].noGravity = true;
            }

            Projectile.velocity.Y = Projectile.velocity.Y + 0.3f;
            if (Projectile.velocity.Y > 18f)
            {
                Projectile.velocity.Y = 18f;
            }
        }
    }
}