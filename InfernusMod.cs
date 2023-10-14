using Terraria.ModLoader;

namespace Infernus
{
    public class InfernusMod : Mod
    {
        private ModProperties Properties;

        public InfernusMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }
        internal class ModProperties
        {
            public bool Autoload { get; set; }
            public bool AutoloadGores { get; set; }
            public bool AutoloadSounds { get; set; }
        }
    }
}