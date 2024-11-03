using Terraria;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class PotionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Calamari Rage");
            // Description.SetDefault("\"Boost to Defense, Regen, Damage and Crit\"");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += .7f;
            player.GetCritChance(DamageClass.Generic) += 4;
        }
    }
}
