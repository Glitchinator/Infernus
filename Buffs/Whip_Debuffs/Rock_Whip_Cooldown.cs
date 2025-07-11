using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs.Whip_Debuffs
{
    public class Rock_Whip_Cooldown : ModBuff
    {
        public override string Texture => "Infernus/Buffs/Whip_Debuffs/Whip_Debuff_Icon";
        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsATagBuff[Type] = true;
        }
    }
}
