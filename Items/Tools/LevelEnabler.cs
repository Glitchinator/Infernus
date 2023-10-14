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
            DisplayName.SetDefault("Heresy");
            Tooltip.SetDefault("Your journey awaits, with fortune and fame..."
                 + "\n Enables Stress for the Player"
                 + "\n 40% Increase Health and 40% Increased Damage of Bosses"
                + "\n Adds custom AI to these bosses (Temporal Glow Squid, Raiko, Ruderibus, Corrupted Husk, Serphious, Calypsical)"
                + "\n Stress is an amount of hits the player can take before they die"
                + "\n With each hit(s) you take, you gain a buff until you die"
                + "\n Stress only activates when a boss is alive"
                + "\n Future updates like custom vanilla AI and drops will come later");
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
            if (InfernusNPC.Is_Spawned == true)
            {
                return false;
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