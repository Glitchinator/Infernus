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

namespace Infernus.Projectiles.Temporal_Glow_Squid
{

    public class Squid_Scroll_Pickup : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.alpha = 0;
        }
        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Wraith);
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Wraith);
            }
        }
        public override void AI()
        {
            Projectile.velocity.X = Projectile.velocity.X * .95f;
            Projectile.velocity.Y = Projectile.velocity.Y * .95f;

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].active == true)
                {
                    Vector2 h = Projectile.Center - Main.player[i].Center;
                    if (h.Length() <= 60f && Main.player[i].GetModPlayer<InfernusPlayer>().Squid_Sroll == true)
                    {
                        Main.player[i].GetModPlayer<InfernusPlayer>().Squid_Scroll_Amount++;
                        Projectile.Kill();
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
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(242, 240, 235, 0) * (.30f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
    }
}