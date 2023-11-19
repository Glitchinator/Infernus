using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class LevelEnabler : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 42;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = 0;
            Item.rare = ItemRarityID.Master;
        }
        public override bool CanUseItem(Player player)
        {
            for(int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].boss == true && Main.npc[i].active == true)
                {
                    return false;
                }
            }
            return true;
        }
        public override bool? UseItem(Player player)
        {
            if (InfernusSystem.Level_systemON == true)
            {
                InfernusSystem.Level_systemON = false;
                Main.NewText("Heresy disabled, don't want the fun?", InfernusPlayer.GainXP_Resource);
                return true;
            }
            else if (InfernusSystem.Level_systemON == false)
            {
                InfernusSystem.Level_systemON = true;
                Main.NewText("Heresy activated, let it begin!", InfernusPlayer.GainXP_Resource);
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .Register();
        }
    }
}