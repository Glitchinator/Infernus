using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Wall_Horde : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wall_Horde");
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 88;
            Projectile.height = 500;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1600;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;

            DrawOffsetX = -25;
            DrawOriginOffsetY = -30;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            Projectile.velocity.X = Projectile.velocity.X * .98f;
            Projectile.velocity.Y = Projectile.velocity.Y * .98f;

            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Wraith, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Wraith, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);

            Projectile.velocity *= 1f;

            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 15;
            }
            if (Projectile.timeLeft == 100)
            {
                NPC.NewNPC(Projectile.GetSource_FromAI(), (int)Projectile.Center.X, (int)Projectile.Center.Y, NPCID.DungeonSpirit, Projectile.whoAmI);
                for (int k = 0; k < 12; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
            if (Projectile.timeLeft == 400)
            {
                NPC.NewNPC(Projectile.GetSource_FromAI(), (int)Projectile.Center.X, (int)Projectile.Center.Y, NPCID.DungeonSpirit, Projectile.whoAmI);
                for (int k = 0; k < 12; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
            if (Projectile.timeLeft == 700)
            {
                NPC.NewNPC(Projectile.GetSource_FromAI(), (int)Projectile.Center.X, (int)Projectile.Center.Y, NPCID.DungeonSpirit, Projectile.whoAmI);
                for (int k = 0; k < 12; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
            if (Projectile.timeLeft == 1000)
            {
                NPC.NewNPC(Projectile.GetSource_FromAI(), (int)Projectile.Center.X, (int)Projectile.Center.Y, NPCID.DungeonSpirit, Projectile.whoAmI);
                for (int k = 0; k < 12; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
            if (Projectile.timeLeft == 1300)
            {
                NPC.NewNPC(Projectile.GetSource_FromAI(), (int)Projectile.Center.X, (int)Projectile.Center.Y, NPCID.DungeonSpirit, Projectile.whoAmI);
                for (int k = 0; k < 12; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.BrokenArmor, 180, quiet: false);
        }
    }
}