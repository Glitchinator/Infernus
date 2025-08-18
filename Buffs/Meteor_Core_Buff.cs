using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Meteor_Core_Buff : ModBuff
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
            player.statDefense += 5;
            player.GetAttackSpeed(DamageClass.Melee) += .08f;
            player.GetDamage(DamageClass.Melee) += 0.12f;
        }
    }
}
