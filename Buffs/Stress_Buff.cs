using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Stress_Buff : ModBuff
    {
        public override string Texture => "Infernus/Buffs/Whip_Debuffs/Whip_Debuff_Icon";

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 2f;

            if (Main.rand.NextBool(4))
            {
                for (int a = 0; a < 6; a++)
                {
                    var smoke = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Smoke, 0f, 0f, 100, default, 1.3f);
                    smoke.velocity *= 1.4f;
                    smoke.color = Color.LightPink;
                }
            }
        }
    }
}
