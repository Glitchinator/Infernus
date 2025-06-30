using Infernus.NPCs;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus
{
    public class InfernusItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if(item.type == ItemID.GolemFist)
            {
                item.damage = 110;
            }
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (item.type == ItemID.GolemFist)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(13));
                int i = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), position, newVelocity * 0.8f, type, damage, knockback);
                Main.projectile[i].usesLocalNPCImmunity = true;
                Main.projectile[i].usesIDStaticNPCImmunity = false;
                Main.projectile[i].localNPCHitCooldown = 12;
            }
        }
    }
}
