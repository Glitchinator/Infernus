using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Meteor_Flare : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            AIType = ProjectileID.Bullet;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            Projectile.timeLeft = 350;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }

            Projectile.velocity.X = Projectile.velocity.X * .98f;
            Projectile.velocity.Y = Projectile.velocity.Y * .98f;
            Projectile.rotation += (float)Projectile.direction * 7;

            if (Projectile.timeLeft == 0)
            {
                NPC.NewNPC(Projectile.GetSource_FromAI(), (int)Projectile.Center.X, (int)Projectile.Center.Y, NPCID.BurningSphere, Projectile.whoAmI);
                for (int k = 0; k < 12; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
            if (Projectile.timeLeft == 83)
            {
                NPC.NewNPC(Projectile.GetSource_FromAI(), (int)Projectile.Center.X, (int)Projectile.Center.Y, NPCID.BurningSphere, Projectile.whoAmI);
                for (int k = 0; k < 12; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
            if (Projectile.timeLeft == 166)
            {
                NPC.NewNPC(Projectile.GetSource_FromAI(), (int)Projectile.Center.X, (int)Projectile.Center.Y, NPCID.BurningSphere, Projectile.whoAmI);
                for (int k = 0; k < 12; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
            if (Projectile.timeLeft == 196)
            {
                NPC.NewNPC(Projectile.GetSource_FromAI(), (int)Projectile.Center.X, (int)Projectile.Center.Y, NPCID.BurningSphere, Projectile.whoAmI);
                for (int k = 0; k < 12; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
            if (Projectile.timeLeft == 265)
            {
                NPC.NewNPC(Projectile.GetSource_FromAI(), (int)Projectile.Center.X, (int)Projectile.Center.Y, NPCID.BurningSphere, Projectile.whoAmI);
                for (int k = 0; k < 12; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
        }
    }
}