using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{
    public class Boulder_Rain : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 120)
            {
                
            }
            else
            {
                Projectile.velocity.X = Projectile.velocity.X * 0.9f;
                Projectile.velocity.Y = Projectile.velocity.Y * 0.9f;
            }
            Projectile.rotation += (float)Projectile.direction * 7;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.BrokenArmor, 800);
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
        /*
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 9; k++)
            {
                Vector2 speed_Dust = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed_Dust * 18, DustID.Stone, speed_Dust, 0, default, Scale: 2f);
                wand.noGravity = true;
            }
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, -2.5f, 0, ModContent.ProjectileType<Ice_Bolt_Homing>(), Projectile.damage,0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 2.5f, 3f, ModContent.ProjectileType<Ice_Bolt_Homing>(), Projectile.damage, 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 2.5f, -3f, ModContent.ProjectileType<Ice_Bolt_Homing>(), Projectile.damage, 0, Projectile.owner);
        }
        */
    }
}