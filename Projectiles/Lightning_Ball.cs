using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Lightning_Ball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Ball");
            Main.projFrames[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.NebulaSphere);
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 300;
            Projectile.alpha = 0;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.X * 0.05f;

            Projectile.velocity.X = Projectile.velocity.X * 1.065f;
            Projectile.velocity.Y = Projectile.velocity.Y * 1.065f;


            if (++Projectile.frameCounter >= 22)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);
            for (int k = 0; k < 12; k++)
            {
                Vector2 speed = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 16, DustID.Electric, speed * 2, Scale: 1f);
                wand.noGravity = true;
            }
        }
    }
}