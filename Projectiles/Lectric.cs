using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Lectric : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = false;
            Projectile.height = 8;
            Projectile.width = 8;
            Projectile.hostile = false;
            Projectile.timeLeft = 180;
            Projectile.netImportant = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.velocity.Y = Projectile.velocity.Y + 0.6f;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            if(Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            }
            if (Projectile.timeLeft <= 150)
            {
                Projectile.friendly = true;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.75f);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity.X *= .85f;
            return false;
        }
    }
}