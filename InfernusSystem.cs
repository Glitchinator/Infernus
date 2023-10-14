using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Infernus
{
    public class InfernusSystem : ModSystem
    {
        public static bool downedRaiko = false;
        public static bool downedRuderibus = false;
        public static bool downedTigerShark = false;
        public static bool downedSquid = false;
        public static bool downedBoulderInvasionPHM = false;
        public static bool downedBoulderInvasionHM = false;
        public static bool downedBoulderBoss = false;
        public static bool downedCalypsical = false;
        public static bool Level_systemON = false;

        public override void OnWorldLoad()
        {
            downedRaiko = false;
            downedRuderibus = false;
            downedTigerShark = false;
            downedSquid = false;
            downedBoulderInvasionPHM = false;
            downedBoulderInvasionHM = false;
            downedBoulderBoss = false;
            downedCalypsical = false;
            Level_systemON = false;
        }

        public override void OnWorldUnload()
        {
            downedRaiko = false;
            downedRuderibus = false;
            downedTigerShark = false;
            downedSquid = false;
            downedBoulderInvasionPHM = false;
            downedBoulderInvasionHM = false;
            downedBoulderBoss = false;
            downedCalypsical = false;
            Level_systemON = false;
        }
        public override void SaveWorldData(TagCompound tag)
        {
            if (downedRaiko)
            {
                tag["downedRaiko"] = true;
            }
            if (downedRuderibus)
            {
                tag["downedRuderibus"] = true;
            }
            if (downedTigerShark)
            {
                tag["downedTigerShark"] = true;
            }
            if (downedSquid)
            {
                tag["downedSquid"] = true;
            }
            if (downedBoulderInvasionHM)
            {
                tag["downedHM"] = true;
            }
            if (downedBoulderInvasionPHM)
            {
                tag["downedPHM"] = true;
            }
            if (downedCalypsical)
            {
                tag["downedMech"] = true;
            }
            if (downedBoulderBoss)
            {
                tag["downedboulder"] = true;
            }
            if (Level_systemON)
            {
                tag["LevelON"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedRaiko = tag.ContainsKey("downedRaiko");
            downedRuderibus = tag.ContainsKey("downedRuderibus");
            downedTigerShark = tag.ContainsKey("downedTigerShark");
            downedSquid = tag.ContainsKey("downedSquid");
            downedBoulderInvasionHM = tag.ContainsKey("downedHM");
            downedBoulderInvasionPHM = tag.ContainsKey("downedPHM");
            downedBoulderBoss = tag.ContainsKey("downedboulder");
            downedCalypsical = tag.ContainsKey("downedMech");
            Level_systemON = tag.ContainsKey("LevelON");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedRaiko;
            flags[1] = downedRuderibus;
            flags[2] = downedTigerShark;
            flags[3] = downedSquid;
            flags[4] = downedBoulderInvasionPHM;
            flags[5] = downedBoulderInvasionHM;
            flags[6] = downedCalypsical;
            flags[7] = Level_systemON;
            flags[8] = downedBoulderBoss;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedRaiko = flags[0];
            downedRuderibus = flags[1];
            downedTigerShark = flags[2];
            downedSquid = flags[3];
            downedBoulderInvasionPHM = flags[4];
            downedBoulderInvasionHM = flags[5];
            downedCalypsical = flags[6];
            Level_systemON = flags[7];
            downedBoulderBoss = flags[8];
        }
    }
}