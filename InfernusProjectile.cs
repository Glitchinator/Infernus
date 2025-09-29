using Infernus.Buffs;
using Infernus.NPCs;
using Infernus.Projectiles;
using Microsoft.Xna.Framework;
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
            /*
            if (InfernusSystem.Level_systemON == true && InfernusNPC.Is_Spawned == true && projectile.hostile == true)
            {
                projectile.damage = (int)(projectile.damage * 1.4f);
            }
            */

            if (projectile.type == 496)
            {
                projectile.velocity *= 2f;
            }

            if (source is EntitySource_Parent parent && parent.Entity is NPC npc && InfernusSystem.Level_systemON == true && InfernusNPC.Is_Spawned == true && projectile.hostile == true)
            {
                projectile.damage = (int)(projectile.damage * 1.4f);
            }
            //string dam = projectile.damage.ToString();
            //Main.NewText(dam, 229, 214, 127);
        }
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == 496)
            {
                projectile.stopsDealingDamageAfterPenetrateHits = true;
            }
        }
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
