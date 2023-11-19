using Terraria;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Aeritite_Timer : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Aeritite_Equipped = false;
        }
    }
}
