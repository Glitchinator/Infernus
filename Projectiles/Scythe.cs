using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Scythe : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 300;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.netImportant = true;
            Projectile.ownerHitCheck = true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int k = 0; k < 16; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 20, DustID.Blood, speed2 * 2, Scale: 1.7f);
                wand.noGravity = true;
            }
            Projectile.damage = (int)(Projectile.damage * 0.75f);
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
            if (Main.rand.NextBool(7))
            {
                int h = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Aeritite_Mine_Explosion>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                Main.projectile[h].DamageType = DamageClass.Melee;
                // Spawn a bunch of smoke dusts.
                for (int i = 0; i < 30; i++)
                {
                    var smoke = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                    smoke.velocity *= 1.4f;
                    smoke.color = Color.LightPink;
                }

                // Spawn a bunch of fire dusts.
                for (int j = 0; j < 20; j++)
                {
                    var fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemSapphire, 0f, 0f, 100, default, 3.5f);
                    fireDust.noGravity = true;
                    fireDust.velocity *= 7f;
                    fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.FrostStaff, 0f, 0f, 100, default, 1.5f);
                    fireDust.velocity *= 3f;
                }

                // Spawn a bunch of smoke gores.
                for (int k = 0; k < 2; k++)
                {
                    float speedMulti = 0.4f;
                    if (k == 1)
                    {
                        speedMulti = 0.8f;
                    }

                    var smokeGore = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                    smokeGore.velocity *= speedMulti;
                    smokeGore.velocity += Vector2.One;
                    smokeGore = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                    smokeGore.velocity *= speedMulti;
                    smokeGore.velocity.X -= 1f;
                    smokeGore.velocity.Y += 1f;
                    smokeGore = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                    smokeGore.velocity *= speedMulti;
                    smokeGore.velocity.X += 1f;
                    smokeGore.velocity.Y -= 1f;
                    smokeGore = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                    smokeGore.velocity *= speedMulti;
                    smokeGore.velocity -= Vector2.One;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            retracted = true;
            return false;
        }
        //int timer;
        //bool retracting = false;
        bool retracted = false;
        int Speed = 24;
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 10;
            height = 10;
            fallThrough = true;

            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation += 0.2f * (float)Projectile.direction;

            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.014f;



            if (Main.myPlayer == Projectile.owner)
            {
                var inertia = 8f;
                Vector2 direction = player.Center - Projectile.Center;
                float dist_check = Magnitude(direction);

                if (player.dead || !player.active)
                {
                    return;
                }
                if (retracted == true)
                {
                    Projectile.tileCollide = false;
                    direction.Normalize();
                    direction *= Speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;

                }
                if (dist_check <= 40f)
                {
                    if (retracted == true)
                    {
                        Projectile.Kill();
                    }
                }
                if (retracted == false && dist_check >= 440f)
                {
                    retracted = true;
                    Speed = 34;
                    //retracted = true;
                }
            }
        }
        private static float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Smoke);
            }
        }
    }
}