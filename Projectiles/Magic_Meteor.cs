﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Magic_Meteor : ModProjectile
    {
        int timer = 110;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 82;
            Projectile.height = 82;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 4000;
            Projectile.extraUpdates = 1;
            Projectile.alpha = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 18;
        }
        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int k = 0; k < 18; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 1f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.InfernoFork, speed * 3, Scale: 2f);
                Sword.noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);

            // Resize the projectile again so the explosion dust and gore spawn from the middle.
            // Rocket I: 22, Rocket III: 80, Mini Nuke Rocket: 50
            Projectile.Resize(50, 50);

            // Spawn a bunch of smoke dusts.
            for (int i = 0; i < 30; i++)
            {
                var smoke = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                smoke.velocity *= 1.4f;
            }

            // Spawn a bunch of fire dusts.
            for (int j = 0; j < 20; j++)
            {
                var fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
                fireDust.noGravity = true;
                fireDust.velocity *= 7f;
                fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
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
        public override void AI()
        {
            Projectile.velocity.X = Projectile.velocity.X * .98f;
            Projectile.velocity.Y = Projectile.velocity.Y * .98f;

            if(Projectile.velocity.X <= 1 && Projectile.velocity.Y <= 1)
            {
                timer--;
                SoundEngine.PlaySound(SoundID.Item47, Projectile.position);
            }
            if (timer <= 5)
            {
                Projectile.tileCollide = false; // This is important or the explosion will be in the wrong place if the grenade explodes on slopes.
                Projectile.alpha = 255; // Make the grenade invisible.
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Magic_Meteor_Explosion>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
            }
            if (timer <= 0)
            {
                Projectile.Kill();
            }
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation += 0.2f * (float)Projectile.direction;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.velocity.X = Projectile.velocity.X * .80f;
            Projectile.velocity.Y = Projectile.velocity.Y * .80f;
            Projectile.damage = (int)(Projectile.damage * 0.95f);
            for (int k = 0; k < 18; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.InfernoFork, speed * 3, Scale: 2f);
                Sword.noGravity = true;
            }
            timer -= 4;
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(242, 240, 235, 0) * (.30f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
    }
}