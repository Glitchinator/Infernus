using Infernus.NPCs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus
{
    public class InfernusProjectiles : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (InfernusSystem.Level_systemON == true && InfernusNPC.Is_Spawned == true && projectile.hostile == true)
            {
                projectile.damage = (int)(projectile.damage * 1.4f);
            }
        }
        /*
         * just set timeleft during projectile spawn in flarerolver
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.aiStyle == 33)
            {
                projectile.timeLeft = 1030;
            }
        }
        */
        public override void AI(Projectile projectile)
        {
            if(Calypsical.death_aninatiom == true)
            {
                if(projectile.hostile == true)
                {
                    projectile.Kill();
                }
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(projectile.type == ProjectileID.Flare || projectile.type == ProjectileID.BlueFlare)
            {
                projectile.damage = (int)(projectile.damage * 0.70f);
            }
        }
    }
}
