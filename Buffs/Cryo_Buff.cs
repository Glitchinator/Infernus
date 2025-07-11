using Terraria;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Cryo_Buff : ModBuff
    {

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.14f;
        }
    }
}
