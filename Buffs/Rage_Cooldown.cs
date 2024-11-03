using Terraria;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Rage_Cooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Hellion_Spite = false;
        }
    }
}
