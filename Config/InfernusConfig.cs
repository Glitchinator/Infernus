using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Infernus.Config
{
    public class InfernusConfig : ModConfig
    {
        // ConfigScope.ClientSide should be used for client side, usually visual or audio tweaks.
        // ConfigScope.ServerSide should be used for basically everything else, including disabling items or changing NPC behaviors
        public override ConfigScope Mode => ConfigScope.ServerSide;

        //[DefaultValue(true)]
        //[ReloadRequired]
        public bool Skip_Dialogue;

        public bool Disable_Elf;

        public bool Enable_StressUI_Text;

        [ReloadRequired]
        public bool No_Invasion;
    }
}
