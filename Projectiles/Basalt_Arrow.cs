using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Basalt_Arrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.height = 12;
            Projectile.width = 12;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 36;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, -1, ModContent.ProjectileType<Basalt_Proj>(), (int)(Projectile.damage * 0.4f), 2f, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 1, ModContent.ProjectileType<Basalt_Proj>(), (int)(Projectile.damage * 0.4f), 2f, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, -1, 0, ModContent.ProjectileType<Basalt_Proj>(), (int)(Projectile.damage * 0.4f), 2f, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 1, 0, ModContent.ProjectileType<Basalt_Proj>(), (int)(Projectile.damage * 0.4f), 2f, Projectile.owner);
        }
    }
}