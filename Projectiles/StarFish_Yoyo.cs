using Infernus.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class StarFish_Yoyo : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 200f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 10f;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.height = 22;
            Projectile.width = 22;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.netImportant = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 19;
        }
        public override void AI()
        {
            float maxDetectRadius = 240f;
            float projSpeed = 19f;
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
            {
                Projectile.ai[2] = 0f;
                return;
            }
            Projectile.ai[2] += 1f;
            if (Projectile.ai[2] >= 35f)
            {
                int x = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Projectile.Center, (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed, ModContent.ProjectileType<Coral_Minion_Shot>(), (int)(Projectile.damage * 1.25f), 1f, Main.myPlayer, 0f, Projectile.owner);
                Main.projectile[x].DamageType = DamageClass.Melee;
                Projectile.ai[2] = 0f;
            }
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