using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Ice_Scroll_Buff : ModBuff
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
            player.statDefense += 6;
            player.endurance += 0.06f;
        }
    }
}
