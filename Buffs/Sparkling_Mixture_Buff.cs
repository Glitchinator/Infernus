using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Buffs
{
    public class Sparkling_Mixture_Buff : ModBuff
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
            player.GetDamage(DamageClass.Melee) += 0.15f;
            player.moveSpeed += 0.25f;
            player.lifeRegen += 4;
            for (int i = 0; i < 2; i++)
            {
                //int type = Main.rand.Next(new int[] {DustID.GlowingMushroom});
                Vector2 speed = Main.rand.NextVector2Circular(2f, 1f);
                Dust Sword = Dust.NewDustPerfect(player.Center + speed * 32, DustID.GlowingMushroom, speed * 3, Scale: 2f);
                Sword.noGravity = true;
                Sword.scale = Main.rand.Next(70, 110) * 0.012f;
            }
        }
}
}
