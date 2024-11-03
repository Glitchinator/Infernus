using Infernus.Buffs;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class Equite_Leaf : ModProjectile
    {
        public bool foundTarget = false;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 0;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = false;
            Projectile.penetrate = 2;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 15;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            InfernusPlayer.Equite_Amount -= 1;
        }
        public override void OnSpawn(IEntitySource source)
        {
            foundTarget = false;
            InfernusPlayer.Equite_target = null;
            foundTarget = false;
        }
        int timer;
        public override void AI()
        {
            if (Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Equite_Equipped == false)
            {
                Projectile.Kill();
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            Player player = Main.player[Projectile.owner];

            Vector2 withplayer = player.Center;
            withplayer.Y -= 14f;
            float notamongusX = (3 + Projectile.minionPos * 3) * -player.direction;
            withplayer.X += notamongusX;
            Vector2 vectorToplayer = withplayer - Projectile.Center;
            float distanceToplayer = vectorToplayer.Length();
            if (Main.myPlayer == player.whoAmI && distanceToplayer > 2000f)
            {
                Projectile.position = withplayer;
                Projectile.velocity *= 0.1f;
            }

            float distanceFromTarget = 250f;
            Vector2 targetCenter = Projectile.position;

            if (InfernusPlayer.Equite_target != null)
            {
                foundTarget = true;
                targetCenter = InfernusPlayer.Equite_target.Center;
            }
            Projectile.friendly = foundTarget;

            if (timer == 120)
            {
                InfernusPlayer.Equite_target = null;
                foundTarget = false;
                timer = 0;
            }

            float speed = 22f;
            float inertia = 8f;

            if (foundTarget)
            {
                if (distanceFromTarget > 40f)
                {
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                timer++;
            }
            else
            {
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
        }
    }
}