using Infernus.Items.Materials;
using Infernus.Placeable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
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
                    Point p = new Point(i, j);
                    Vector2 g = p.ToWorldCoordinates();
                    player.ConsumeItem(ModContent.ItemType<Scorched_Sinew>());
                    for (int k = 0; k < 26; k++)
                    {
                        Vector2 speed2 = Main.rand.NextVector2Unit();
                        Dust wand = Dust.NewDustPerfect(g, DustID. SolarFlare, speed2 * 16, Scale: 1.3f);
                        wand.noGravity = true;
                    }
                    SoundEngine.PlaySound(SoundID.NPCHit52, g);
                    if (Main.netMode != NetmodeID.MultiplayerClient)

                    {
                        NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Raiko").Type);
                    }
                }
                else
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Main.NewText("Bring an offering to summon the meteor", 239, 106, 15);
                    }
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("Bring an offering to summon the meteor"), new(239, 106, 15), player.whoAmI);
                    }
                }
            }
            else if (Main.dayTime == true)
            {
                Player player = Main.LocalPlayer;
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText("The sun shines brighter than the flame, come back at night.", 239, 106, 15);
                }
                if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("The sun shines brighter than the flame, come back at night."), new(239, 106, 15), player.whoAmI);
                }
            }
            return true;
        }
    }
}