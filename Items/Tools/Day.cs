using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class Day : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteorite Lamp");
            Tooltip.SetDefault("Take control of the day... or night");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 18;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = 60000;
            Item.rare = ItemRarityID.Blue;
        }
        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, player.position);
            if (Main.dayTime == true)
            {
                Main.dayTime = false;
                return true;
            }
            else if (Main.dayTime == false)
            {
                Main.dayTime = true;
            }
            return true;
        }
    }
}