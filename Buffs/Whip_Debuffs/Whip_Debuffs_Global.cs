using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs.Whip_Debuffs
{
    public class Whip_Debuffs_Global : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool markedByaerWhip;
        public bool markedBypalWhip;
        public bool markedByrockyWhip;
        public bool markedByiceWhip;
        public bool markedBygemWhip;
        public bool markedByeleWhip;
        public bool markedBydrillWhip;


        public override void ResetEffects(NPC npc)
        {
            markedByaerWhip = false;
            markedBypalWhip = false;
            markedByrockyWhip = false;
            markedByiceWhip = false;
            markedBygemWhip = false;
            markedByeleWhip = false;
            markedBydrillWhip = false;
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (markedByaerWhip && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                damage += 4;
            }
            if (markedBypalWhip && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                damage += 28;
                knockback += 2f;
            }
            if (markedByrockyWhip && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                damage += 10;
            }
            if (markedByiceWhip && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                damage += 7;
            }
            if (markedBygemWhip && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                damage += 5;
            }
            if (markedByeleWhip && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                if (Main.rand.NextBool(3))
                {
                    for (int k = 0; k < 6; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(npc.Center + speed2 * 16, DustID.Electric, speed2 * 2, Scale: 1f);
                        wand.noGravity = true;
                    }
                    Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), npc.Right.X, npc.Right.Y / 1.25f, 0, 10, ModContent.ProjectileType<Lightning_Summon_Shot>(), 110, 0, projectile.owner);
                }
                damage += 12;
            }
            if (markedBydrillWhip && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                damage += 14;
            }
        }
    }
}
