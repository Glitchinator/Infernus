using Infernus.Buffs.Whip_Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{
    public class whipe_ele_shot : ModProjectile
    {
        int Richochet;
        public override string Texture => "Infernus/Items/Weapon/Melee/Hatchet";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 1;
            Projectile.width = 1;
            Projectile.hostile = false;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 100;
            Projectile.netImportant = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 20;
            Projectile.DamageType = DamageClass.Summon;
        }
        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 1; i++)
                {
                    Vector2 projectilePosition = Projectile.position;
                    projectilePosition -= Projectile.velocity * (i * 0.25f);
                    Projectile.alpha = 255;
                    int dust = Dust.NewDust(projectilePosition, 1, 1, DustID.Electric, 0f, 0f, 0, default, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.018f;
                    Main.dust[dust].velocity *= 0.2f;
                }
            }
            float maxDetectRadius = 300f;
            float projSpeed = 14f;

            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
            Projectile.rotation = Projectile.velocity.ToRotation();

            Projectile.velocity.Y += Projectile.ai[0];
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Richochet = Main.rand.Next(new int[] { 140, -140, 170, -170, 230 ,-230 });
            Projectile.damage = (int)(Projectile.damage * 0.7f);
            //Main.rand.Next(-230, 230); I can definately somehow make a better ricochet
            Projectile.position.X = target.position.X - Richochet;
            Projectile.position.Y = target.position.Y - Richochet;

            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
            target.AddBuff(ModContent.BuffType<elewhipbuff>(), 300);
        }
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }
    }
}