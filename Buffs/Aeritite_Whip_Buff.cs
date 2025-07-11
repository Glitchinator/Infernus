using Terraria;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Aeritite_Whip_Buff : ModBuff
    {

        public override void Update(Player player, ref int buffIndex)
        {
            player.whipRangeMultiplier += 0.3f;
        }
    }
}
