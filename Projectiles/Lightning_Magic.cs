using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Lightning_Magic : ModProjectile
    {
        public override void SetDefaults()
        {
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.height = 30;
            Projectile.width = 30;
            Projectile.hostile = false;
            Projectile.timeLeft = 60;
            Projectile.netImportant = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.rotation += 0.5f * (float)Projectile.direction;

            Projectile.velocity.X = 0;
            Projectile.velocity.Y = 0;

            var proj = ModContent.ProjectileType<Lightning_Magic_Shot>();
            var damage = (int)(Projectile.damage * 0.7f);
            if(Projectile.timeLeft == 40)
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X - 45, Projectile.Center.Y + 45, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X + 45, Projectile.Center.Y + 45, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X - 45, Projectile.Center.Y - 45, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X + 45, Projectile.Center.Y - 45, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
            }
            if (Projectile.timeLeft == 20)
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X - 90, Projectile.Center.Y, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y - 90, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X + 90, Projectile.Center.Y, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y + 90, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
            }
        }
        public override void OnKill(int timeLeft)
        {
            var damage = (int)(Projectile.damage * 0.7f);
            var proj = ModContent.ProjectileType<Lightning_Magic_Shot>();
            for (int k = 0; k < 7; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 32, DustID.Electric, speed2 * 2, Scale: 1f);
                wand.noGravity = true;
            }
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X - 90, Projectile.Center.Y + 90, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X + 90, Projectile.Center.Y + 90, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X - 90, Projectile.Center.Y - 90, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X + 90, Projectile.Center.Y - 90, 0, 0, proj, damage, Projectile.knockBack, Projectile.owner);
        }
    }
}