using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Aeritite_Mine : ModProjectile
    {
        int bounces = 9;
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.penetrate = 3;
            Projectile.netImportant = true;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            Projectile.rotation += 0.2f * (float)Projectile.direction;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 22f)
            {
                Projectile.ai[0] = 22f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.6f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bounces--;
            if (bounces <= 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Aeritite_Mine_Explosion>(), (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
                for (int k = 0; k < 10; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Unit();
                    Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.BlueCrystalShard, speed * 2, 0, default, Scale: 2.8f);
                    wand.noGravity = true;
                }
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity.Y *= 0.60f;
                Projectile.velocity.X *= .95f;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Aeritite_Mine_Explosion>(), (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
            for (int k = 0; k < 10; k++)
            {
                Vector2 speed = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.BlueCrystalShard, speed * 2, 0, default, Scale: 2.8f);
                wand.noGravity = true;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.ScalingArmorPenetration += .1f;
        }
    }
}