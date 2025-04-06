using Infernus.Items.Materials;
using Infernus.Placeable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Utilities;

namespace Infernus.Tiles
{
    
    public class Raiko_Summon_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleLineSkip = 2;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(191, 142, 111), Language.GetText("Raiko Summon Place Thingy"));
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<Scorched_Sinew>();
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
        }
        public override bool RightClick(int i, int j)
        {
            if (Main.dayTime == false && !NPC.AnyNPCs(Mod.Find<ModNPC>("Raiko").Type))
            {
                Player player = Main.LocalPlayer;
                if(player.HasItemInAnyInventory(ModContent.ItemType<Scorched_Sinew>()))
                {
                    player.ConsumeItem(ModContent.ItemType<Scorched_Sinew>());
                    SoundEngine.PlaySound(SoundID.ForceRoar, player.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)

                    {
                        NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Raiko").Type);
                    }
                }
                else
                {
                    Main.NewText("Come back at night and bring some Scorched Sinew", 229, 214, 127);
                }
            }
            return true;
        }
    }
}