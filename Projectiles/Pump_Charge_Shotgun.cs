using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Pump_Charge_Shotgun : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 56;
            Projectile.height = 20;
            Projectile.netImportant = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 76;
        }
        public override void AI()
        {
            Projectile.rotation += 0.15f * Projectile.direction;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 26f)
            {
                Projectile.ai[0] = 26f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.44f;
            }
            if (Main.rand.NextBool(10))
            {
                for(int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-9, 8), Main.rand.Next(-9, 8), ModContent.ProjectileType<Pump_Fire_Slug>(), Projectile.damage * 2, 0, Projectile.owner);
                }
            }
            if(Projectile.damage < 220)
            {
                Projectile.damage = 220;
            }
            if (Projectile.damage > 3080)
            {
                Projectile.damage = 3080;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 11; k++)
            {
                Vector2 speed = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(Projectile.Center + speed * 46, DustID.SolarFlare, speed * 2, Scale: 1.8f);
                wand.noGravity = true;
            }
            int y = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<A_Ray_exlos>(), Projectile.damage, 0, Projectile.owner);
            Main.projectile[y].DamageType = DamageClass.Ranged;
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(235, 103, 21, 0) * (.30f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
    }
}