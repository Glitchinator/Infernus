using Terraria;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class PotionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Calamari Rage");
            Description.SetDefault("\"Boost to Defense, Regen, Damage and Crit\"");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 5;
            player.lifeRegen += 1;
            player.GetDamage(DamageClass.Generic) += .1f;
            player.GetCritChance(DamageClass.Generic) += 4;
        }
    }
}
