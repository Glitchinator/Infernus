using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Infernus.Projectiles
{

    public class Magic_Ink_Typhoon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.width = 82;
            Projectile.height = 82;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 500;
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
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.Wraith, speed * 3, Scale: 2f);
                Sword.noGravity = true;
            }
        }
        public override void AI()
        {
            Projectile.velocity.X = Projectile.velocity.X * .97f;
            Projectile.velocity.Y = Projectile.velocity.Y * .97f;

            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation += 0.2f * (float)Projectile.direction;

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].active == true)
                {
                    Vector2 h = Projectile.Center - Main.player[i].Center;
                    if (h.Length() <= 80f)
                    {
                        Main.player[i].AddBuff(ModContent.BuffType<Ink_Typhoon_Buff>(), 900);
                    }
                }
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(242, 240, 235, 0) * (.30f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
    }
}