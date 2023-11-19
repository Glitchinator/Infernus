using Infernus.NPCs;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus
{
    public class InfernusItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if(item.type == ItemID.GolemFist)
            {
                item.damage = 110;
            }
        }
    }
}
