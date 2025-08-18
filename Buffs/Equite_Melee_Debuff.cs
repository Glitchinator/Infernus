using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Equite_Melee_Debuff : ModBuff
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
            if (player.controlDownHold)
            {
                player.maxFallSpeed = 30;
                if (Math.Abs(player.velocity.Y) > 0f)
                {
                    player.velocity.Y += 0.25f;
                }
            }
        }
    }
}
