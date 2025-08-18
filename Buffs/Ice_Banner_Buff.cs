using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Ice_Banner_Buff : ModBuff
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
            player.statDefense += 8;
            player.endurance += 0.08f;
            player.thorns += 0.8f;
        }
    }
}
