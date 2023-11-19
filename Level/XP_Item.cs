using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Level
{
    public class XP_Item : ModItem
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
            if (InfernusSystem.Level_systemON == false)
            {
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), InfernusPlayer.GainXP_Resource, "No XP", true);
                return false;
            }
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), InfernusPlayer.GainXP_Resource, "+1 XP", true);
            return false;
        }
    }
}
