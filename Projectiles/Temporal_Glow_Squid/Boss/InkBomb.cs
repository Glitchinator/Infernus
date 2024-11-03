using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles.Temporal_Glow_Squid.Boss
{

    public class InkBomb : ModProjectile
    {
        bool Touching_Tile = false;
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 500;
        }
        public override void AI()
        {
            if (Touching_Tile == true)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
            else
            {
                Projectile.rotation += (float)Projectile.direction * 15;
                if (Main.rand.NextBool(2))
                {
                    Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Wraith, Main.rand.Next(-2, 3), Main.rand.Next(-2, 3));
                }
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Wraith, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                Main.dust[dust].noGravity = true;
            }


            AdventureMode_Changes();

            Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
            if (Projectile.velocity.Y > 18f)
            {
                Projectile.velocity.Y = 18f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Touching_Tile = true;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Wraith, Projectile.velocity.X, Projectile.velocity.Y);

            Projectile.velocity.X = Projectile.velocity.X * .98f;
            Projectile.velocity.Y = Projectile.velocity.Y * .98f;
            return false;
        }

        private void AdventureMode_Changes()
        {
            if (Projectile.timeLeft == 250 && InfernusSystem.Level_systemON == true)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath12, Projectile.position);

                Projectile.NewProjectile(Terraria.Entity.GetSource_NaturalSpawn(), Projectile.Right.X, Projectile.Right.Y / 2f, 0, 10, ModContent.ProjectileType<InkBolt>(), 10, 0, 0);
            }
        }
    }
}