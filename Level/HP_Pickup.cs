using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Level
{
    public class HP_Pickup : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 1;
            Item.value = 0;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Yellow.ToVector3() * 0.55f * Main.essScale);
        }
        public override bool OnPickup(Player player)
        {
            player.GetModPlayer<InfernusPlayer>().Squid_Scroll_Amount += 1;
            return false;
        }
    }
}
