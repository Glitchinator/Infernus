using Infernus.Invas;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Thick_Boulder : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.netImportant = true;
            Projectile.penetrate = 3;
        }
        public override void AI()
        {
            Projectile.rotation += 0.2f * (float)Projectile.direction;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 15f)
            {
                Projectile.ai[0] = 15f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.6f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Stone, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity *= 0.75f;
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 16; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 1f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.Stone, speed * 3, Scale: 2f);
                Sword.noGravity = true;
            }
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText("A storm rolls overhead from the west. The ground trembles with vibrations deep below.", 207, 196, 162);
            }
            if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("A storm rolls overhead from the west. The ground trembles with vibrations deep below."), new(207, 196, 162), -1);
            }
            BoulderInvasion.StartBoulderInvasion();
        }
    }
}