using Infernus.Projectiles.Raiko.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Meteor_Huge : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            DrawOffsetX = -10;
            DrawOriginOffsetY = -10;
            Projectile.timeLeft = 60;
        }
        public override void AI()
        {
            Projectile.rotation += 0.6f * (float)Projectile.direction;
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.SolarFlare, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.InfernoFork, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);

            // Spawn a bunch of smoke dusts.
            for (int a = 0; a < 30; a++)
            {
                var smoke = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                smoke.velocity *= 1.4f;
                smoke.color = Color.LightPink;
            }

            // Spawn a bunch of fire dusts.
            for (int j = 0; j < 20; j++)
            {
                var fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Meteorite, 0f, 0f, 100, default, 3.5f);
                fireDust.noGravity = true;
                fireDust.velocity *= 7f;
                fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0f, 0f, 100, default, 1.5f);
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
            PunchCameraModifier modifier = new PunchCameraModifier(Projectile.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, FullName);
            Main.instance.CameraModifiers.Add(modifier);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                var i = ModContent.ProjectileType<Meteor_Heat>();
                for (int k = 0; k < 24; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(1f,1f);
                    Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 22, DustID.SolarFlare, speed * 2, 0, default, Scale: 2.8f);
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center + speed * 22, speed * 4, i, Projectile.damage, 0, Projectile.owner);
                    wand.noGravity = true;
                }
                /*
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, -5, i, Projectile.damage, 0, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 5, i, Projectile.damage, 0, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, -5, 0, i, Projectile.damage, 0, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 5, 0, i, Projectile.damage, 0, Projectile.owner);

                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 5, -5, i, Projectile.damage, 0, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, -5, 5, i, Projectile.damage, 0, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, -5, -5, i, Projectile.damage, 0, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 5, 5, i, Projectile.damage, 0, Projectile.owner);
                */
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(237, 116, 9, 0) * (.80f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
    }
}