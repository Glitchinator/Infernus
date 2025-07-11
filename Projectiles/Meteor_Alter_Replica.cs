using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Meteor_Alter_Replica : ModProjectile
    {

        public sealed override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.height = 30;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.sentry = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            player.UpdateMaxTurrets();
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 17f)
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X + Main.rand.Next(-300, 300), Projectile.Center.Y - 400, Main.rand.Next(-9, 8), Main.rand.Next(-11, -5), ModContent.ProjectileType<Boulder2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.ai[0] = 0f;
            }
        }
    }
}