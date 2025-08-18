using Infernus.Buffs;
using Infernus.NPCs;
using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus
{
    public class InfernusKeybind : ModSystem
    {
        public static ModKeybind StressKeybind { get; private set; }

        public override void Load()
        {
            // Registers a new keybind
            // We localize keybinds by adding a Mods.{ModName}.Keybind.{KeybindName} entry to our localization files. The actual text displayed to English users is in en-US.hjson
            StressKeybind = KeybindLoader.RegisterKeybind(Mod, "Stress", "C");
        }

        // Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
        public override void Unload()
        {
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            StressKeybind = null;
        }

    }
}
