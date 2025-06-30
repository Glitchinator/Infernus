using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Coral_Minion_Shot : ModProjectile
    {
        public override string Texture => "Infernus/Items/Weapon/Melee/Hatchet";
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Summon;
            Projectile.friendly = true;
            Projectile.height = 8;
            Projectile.width = 8;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Water, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.014f;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Wet, 180);
        }
    }
}