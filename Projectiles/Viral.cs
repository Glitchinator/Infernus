using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Viral : ModProjectile
    {
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            for (int k = 0; k < 20; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Demonite, 2.5f, -2.5f, 0, default, 1.2f);
            }
            target.AddBuff(ModContent.BuffType<Rot_Ball_Demo>(), 240);
        }
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.aiStyle = 15;
        }
        public override bool PreDrawExtras()
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[0] == 1f)
            {
                Texture2D projectileTexture = TextureAssets.Projectile[Projectile.type].Value;
                Vector2 drawPosition = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                Vector2 drawOrigin = new Vector2(projectileTexture.Width, projectileTexture.Height) / 2f;
                Color drawColor = Projectile.GetAlpha(lightColor);
                drawColor.A = 127;
                drawColor *= 0.5f;
                int launchTimer = (int)Projectile.ai[1];
                if (launchTimer > 5)
                    launchTimer = 5;

                SpriteEffects spriteEffects = SpriteEffects.None;
                if (Projectile.spriteDirection == -1)
                    spriteEffects = SpriteEffects.FlipHorizontally;

                for (float transparancy = 1f; transparancy >= 0f; transparancy -= 0.125f)
                {
                    float opacity = 1f - transparancy;
                    Vector2 drawAdjustment = Projectile.velocity * -launchTimer * transparancy;
                    Main.EntitySpriteDraw(projectileTexture, drawPosition + drawAdjustment, null, drawColor * opacity, Projectile.rotation, drawOrigin, Projectile.scale * 1.15f * MathHelper.Lerp(0.5f, 1f, opacity), spriteEffects, 0);
                }
            }
            Vector2 playerArmPosition = Main.GetPlayerArmPosition(Projectile);

            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("Infernus/Projectiles/Viral_Chain");

            Rectangle? chainSourceRectangle = null;
            float chainHeightAdjustment = 0f;

            Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
            Vector2 chainDrawPosition = Projectile.Center;
            Vector2 vectorFromProjectileToPlayerArms = playerArmPosition.MoveTowards(chainDrawPosition, 4f) - chainDrawPosition;
            Vector2 unitVectorFromProjectileToPlayerArms = vectorFromProjectileToPlayerArms.SafeNormalize(Vector2.Zero);
            float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height()) + chainHeightAdjustment;
            if (chainSegmentLength == 0)
            {
                chainSegmentLength = 10;
            }
            float chainRotation = unitVectorFromProjectileToPlayerArms.ToRotation() + MathHelper.PiOver2;
            int chainCount = 0;
            float chainLengthRemainingToDraw = vectorFromProjectileToPlayerArms.Length() + chainSegmentLength / 2f;

            while (chainLengthRemainingToDraw > 0f)
            {
                var chainTextureToDraw = chainTexture;
                Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, lightColor, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);
                chainDrawPosition += unitVectorFromProjectileToPlayerArms * chainSegmentLength;
                chainCount++;
                chainLengthRemainingToDraw -= chainSegmentLength;
            }

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects spriteEffects2 = SpriteEffects.None;

            if (Projectile.spriteDirection == -1)
            {
                spriteEffects2 = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin2 = new(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin2 + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(101, 67, 122, 0) * (.60f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin2, Projectile.scale, spriteEffects2, 0);
            }

            return true;
        }
    }
}