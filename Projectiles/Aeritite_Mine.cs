using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Aeritite_Mine : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 4;
        }
        int bounces = 3;
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.penetrate = 3;
            Projectile.netImportant = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 200;
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(47, 147, 194, 0) * (.74f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }
            return true;
        }
        public override void AI()
        {
            Projectile.rotation += 0.2f * (float)Projectile.direction;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 26f)
            {
                Projectile.ai[0] = 26f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.45f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            if (Main.rand.NextBool(12))
            {
                for (int k = 0; k < 4; k++)
                {
                    Vector2 speed2 = Main.rand.NextVector2Unit();
                    Dust wand = Dust.NewDustPerfect(Projectile.Center + speed2 * 16, DustID.Electric, speed2 * 2, Scale: 1f);
                    wand.noGravity = true;
                }
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
                    Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 22, DustID.BlueCrystalShard, speed * 2, 0, default, Scale: 2.8f);
                    wand.noGravity = true;
                }
                Projectile.Kill();
            }
            else
            {
                //Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity.Y *= 0.75f;
                Projectile.velocity.X *= .85f;
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
            bounces--;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.ScalingArmorPenetration += .1f;
        }
    }
}