using Infernus.Buffs.Whip_Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class WhipDrillx : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.IsAWhip[Type] = true;
            Projectile.netImportant = true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
            target.AddBuff(ModContent.BuffType<drillwhipbuff>(), 300);
            Projectile.damage = (int)(Projectile.damage * 0.85f);

            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), target.Center, Vector2.Zero, ModContent.ProjectileType<whip_drill_exlos>(), (int)(Projectile.damage * 1.1f), 4f, Projectile.owner);
        }

        public override void SetDefaults()
        {
            Projectile.DefaultToWhip();
        }

        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new();
            Projectile.FillWhipControlPoints(Projectile, list);

            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 pos = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                Rectangle frame = new(0, 0, 26, 26);
                Vector2 origin = new(13, 8);
                float scale = 1;

                if (i == list.Count - 2)
                {
                    frame.Y = 74;
                    frame.Height = 18;

                    Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
                    float t = Timer / timeToFlyOut;
                    scale = MathHelper.Lerp(0.55f, 1.55f, Utils.GetLerpValue(0.15f, 0.75f, t, true) * Utils.GetLerpValue(0.95f, 0.75f, t, true));

                    if (Main.rand.NextBool(1))
                    {
                        int whip = Dust.NewDust(pos, Projectile.width, Projectile.height, DustID.SolarFlare, Projectile.velocity.X, Projectile.velocity.Y);
                        Main.dust[whip].noGravity = true;
                    }
                }
                else if (i > 15)
                {
                    frame.Y = 74;
                    frame.Height = 16;
                }
                else if (i > 10)
                {
                    frame.Y = 58;
                    frame.Height = 16;
                }
                else if (i > 5)
                {
                    frame.Y = 42;
                    frame.Height = 16;
                }
                else if (i > 0)
                {
                    frame.Y = 26;
                    frame.Height = 16;
                }

                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

                pos += diff;
            }
            return false;
        }
    }
}
