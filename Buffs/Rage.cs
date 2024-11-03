using Terraria;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Rage : ModBuff
    {

        public override void Update(Player player, ref int buffIndex)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Rage = true;

            if(player.buffTime[buffIndex] == 0)
            {
                player.AddBuff(ModContent.BuffType<Rage_Cooldown>(), 1800);
            }
        }
    }
}
