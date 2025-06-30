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

            DustType = DustID.Meteorite;
            MinPick = 110;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            //TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            //TileObjectData.newTile.StyleLineSkip = 2;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(191, 142, 111), Language.GetText("Meteor Alter"));
        }
        public override bool CanExplode(int i, int j)
        {
            return true;
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
                    SoundEngine.PlaySound(SoundID.NPCHit52, new Vector2(i, j));
                    for (int k = 0; k < 13; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(new Vector2(i,j), DustID. SolarFlare, speed2 * 2, Scale: 1f);
                        wand.noGravity = true;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient)

                    {
                        NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Raiko").Type);
                    }
                }
            }
            else if (Main.dayTime == true)
            {
                Main.NewText("The sun shines brighter than the flame, come back at night.", 229, 214, 127);
            }
            return true;
        }
    }
}