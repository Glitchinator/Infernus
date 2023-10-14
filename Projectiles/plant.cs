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
            DisplayName.SetDefault("Pistol Shot");
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
        int timer;
        public override void AI()
        {
            Projectile.rotation += (float)Projectile.direction * 2;
            timer++;
            if (timer == 50)
            {
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, -7, 0, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, 7, 0, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, 0, 7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, 0, -7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, -7, -7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, 7, -7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, -7, 7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, 7, 7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
                timer = 0;
                Projectile.netUpdate = true;
            }
            Projectile.velocity.X = Projectile.velocity.X * .98f;
            Projectile.velocity.Y = Projectile.velocity.Y * .98f;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, -7, 0, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, 7, 0, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, 0, 7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, 0, -7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, -7, -7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, 7, -7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, -7, 7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.position.X + 40, Projectile.position.Y + 40, 7, 7, ModContent.ProjectileType<Flour_Homing>(), Projectile.damage / 2, 1, Main.myPlayer, 0f, 0f);
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