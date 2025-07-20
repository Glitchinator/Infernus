using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{
    public class plant : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.timeLeft = 140;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Projectile.rotation += (float)Projectile.direction * 2;

            Projectile.velocity.X = Projectile.velocity.X * .98f;
            Projectile.velocity.Y = Projectile.velocity.Y * .98f;
        }
        public override void OnKill(int timeLeft)
        {
            var damage = (int)(Projectile.damage * 0.6f);
            var proj = ModContent.ProjectileType<Flour_Homing>();
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(-7, 0), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(7, 0), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(0, 7), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(0, -7), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(-7, -7), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(7, -7), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(-7, 7), proj, damage, 3f, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, new Vector2(7, 7), proj, damage, 3f, Main.myPlayer);
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