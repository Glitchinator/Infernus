using Terraria;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Corrupt_Cooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
}
