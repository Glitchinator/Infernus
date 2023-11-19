using Terraria;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Aeritite_Sword_Buff : ModBuff
    {

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Melee) += .5f; // Adds faster attack speed (negative slows the attack speed)
        }
    }
}
