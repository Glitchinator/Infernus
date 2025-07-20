using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class Magic9Ball : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = 0;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Master;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.DemonAltar)
            .Register();
        }
        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<InfernusPlayer>().Stress_Buff_2 == true)
            {
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), InfernusPlayer.GainXP_Resource, " 1 Stress point = 12% Movespeed increase" + "\n 3 Stress points = 12% jumpspeed increase" + "\n 5 Stress points = 25% damage increase" + "\n 7 Stress points = 35 crit increase", true);
                return true;
            }
            if (player.GetModPlayer<InfernusPlayer>().Stress_Buff_1 == true)
            {
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), InfernusPlayer.GainXP_Resource, " 1 Stress point = 12% Movespeed increase" + "\n 3 Stress points = 12% jumpspeed increase" + "\n 5 Stress points = 12% damage increase" + "\n 7 Stress points = 20 crit increase", true);
            }
            if (player.GetModPlayer<InfernusPlayer>().Stress_Buff_1 == false && player.GetModPlayer<InfernusPlayer>().Stress_Buff_2 == false)
            {
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), InfernusPlayer.GainXP_Resource, " 1 Stress point = 6% Movespeed increase" + "\n 3 Stress points = 6% jumpspeed increase" + "\n 5 Stress points = 12% damage increase" + "\n 7 Stress points = 20 crit increase", true);
                return true;
            }
            return true;
        }
    }
}