using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs.Whip_Debuffs
{
    public class Ancient_Whiphead : ModBuff
    {
        public override string Texture => "Infernus/Buffs/Whip_Debuffs/Whip_Debuff_Icon";
        public static readonly int TagDamage = 2;
        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsATagBuff[Type] = true;
        }
    }
}
