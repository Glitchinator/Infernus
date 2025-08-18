using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Ink_Typhoon_Buff : ModBuff
    {
        public override string Texture => "Infernus/Buffs/Whip_Debuffs/Whip_Debuff_Icon";

        /*
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        */
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Magic) += 0.12f;
            player.GetCritChance(DamageClass.Magic) += 8;
            player.statManaMax2 += 80;
            player.manaRegen += 10;
            player.manaCost -= 0.1f;
        }
    }
}
