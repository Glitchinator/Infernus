using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs.Whip_Debuffs
{
    public class elewhipbuff : ModBuff
    {
        public override string Texture => "Infernus/Buffs/Whip_Debuffs/Whip_Debuff_Icon";
        public static readonly int TagDamage = 7;
        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsATagBuff[Type] = true;
        }
    }
}
