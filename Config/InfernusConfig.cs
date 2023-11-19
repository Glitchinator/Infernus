using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Infernus.Config
{
    public class InfernusConfig : ModConfig
    {
        // Sorry example mod. This is a straight rip-off of your code.

        // ConfigScope.ClientSide should be used for client side, usually visual or audio tweaks.
        // ConfigScope.ServerSide should be used for basically everything else, including disabling items or changing NPC behaviors
        public override ConfigScope Mode => ConfigScope.ServerSide;

        //[DefaultValue(true)]
        //[ReloadRequired]
        public bool Skip_Dialogue;

        [ReloadRequired]
        public bool No_Invasion;
    }
}
