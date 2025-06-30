using Infernus.Buffs;
using Infernus.Buffs.Whip_Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Equite_Leaf : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 0;
            ProjectileID.Sets.TrailCacheLength[Type] = 4;
        }
        bool shooting = false;
        bool shot = false;
        bool hits = false;

        public sealed override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 48;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hits = true;
            Player player = Main.player[Projectile.owner];

            if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Knuckles_Equipped == true)
            {
                // do melee stuff on hit
                // reduce enemy defense
                target.defense--;
            }
            if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Quiver_Equipped == true)
            {
                // do ranged stuff on hit
                // extra slashes on hit
                float positionX = Projectile.Center.X + (Projectile.velocity.X * -1f);
                float positionY = Projectile.Center.Y + (Projectile.velocity.Y * -1f);
                float rotation = MathHelper.ToRadians(3);
                Vector2 velocity = Projectile.velocity;
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(rotation, -rotation, i));
                    int x = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), positionX, positionY, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<EqualSword>(), (int)(Projectile.damage * .25f), 0, Projectile.owner);
                    Main.projectile[x].DamageType = DamageClass.Ranged;
                }
            }
            if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Emblem_Equipped == true)
            {
                // do summoner stuff on hit
                // custom tag damage buff
                target.AddBuff(ModContent.BuffType<Equite_Tag_Debuff>(), 300);
            }
            if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Charm_Equipped == true)
            {
                // do magic stuff on hit
                // restore some mana
                player.statMana += 15;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.SandstormInABottle);
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
                Main.EntitySpriteDraw(texture, drawPos, null, new Color(180, 158, 83, 0) * (.50f - Projectile.alpha / 210f), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
            }

            return true;
        }
        public override void AI()
        {

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            // if equite armor is taken off
            if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Equipped == false)
            {
                Projectile.Kill();
            }
            // check to make sure that player has hit a target and the target is active then shoot out and attack that enemy
            if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Hit == true && Projectile.alpha == Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Amount)
            {
                if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Target != null)
                {
                    if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Target.active == true)
                    {
                        // shooting means the leaf goes after the enemy
                        shooting = true;
                    }
                }
                else
                {
                    // else just resets so no false positives. I think it works that way
                    Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Hit = false;
                    shooting = false;
                }
            }

            // now that the leaf is going after an enemy, remove one leaf from player and tell player it is attacking
            if (shooting == true && shot == false)
            {
                Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Hit = false;
                Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Amount--;
                shot = true;
            }

            // if shooting is true (going after enemy) and leaf hasn't hit the enemy yet. Home onto the enemy until it hits
            if (shooting == true && hits == false)
            {
                var inertia = 12f;
                if (Main.myPlayer == Projectile.owner)
                {
                    NPC closestNPC = Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Target;

                    Vector2 direction = closestNPC.Center - Projectile.Center;
                    direction.Normalize();
                    direction *= 16;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;

                    // also set it so it can hit the enemy and has a time left now
                    Projectile.friendly = true;
                    Projectile.timeLeft = 240;
                }
            }
            // check while flying, if the target is killed, then delete itself
            if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Target != null)
            {
                if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Target.active == false)
                {
                    if (shot == true)
                    {
                        Projectile.Kill();
                    }
                    //Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Hit = false;
                }
            }
            // if not going after an enemy then just follow the player
            if (shooting == false)
            {
                Projectile.timeLeft = 250;
                Player player = Main.player[Projectile.owner];

                Vector2 withplayer = player.Center;
                //withplayer.Y += 4f;
                float notamongusX = (11 + Projectile.minionPos * 3) * -player.direction;
                withplayer.X += notamongusX;
                Vector2 vectorToplayer = withplayer - Projectile.Center;
                float distanceToplayer = vectorToplayer.Length();
                if (Main.myPlayer == player.whoAmI && distanceToplayer > 2000f)
                {
                    Projectile.position = withplayer;
                    Projectile.velocity *= 0.1f;
                }

                float speed = 22f;
                float inertia = 8f;


                if (distanceToplayer > 600f)
                {
                    speed = 46f;
                    inertia = 3f;
                }
                else
                {
                    speed = 22f;
                    inertia = 8f;
                }
                if (distanceToplayer > 20f)
                {
                    vectorToplayer.Normalize();
                    vectorToplayer *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToplayer) / inertia;
                }
                else if (Projectile.velocity == Vector2.Zero)
                {
                    Projectile.velocity.X = -0.01f;
                    Projectile.velocity.Y = -0.01f;
                }
            }
            string h = Projectile.timeLeft.ToString();
            Main.NewText(h, 229, 214, 127);
        }
    }
}