using Terraria;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Lucky_Flame : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.luck += 0.15f;
        }
    }
}
