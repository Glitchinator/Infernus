using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.BossSummon
{
    public class Mechsummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Damaged Exotic Prism");
            Tooltip.SetDefault("Summons Calypsical"
                + "\nMust have killed Moon Lord");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = 0;
            Item.rare = ItemRarityID.Red;
            Item.consumable = false;
            Item.maxStack = 1;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Calypsical").Type) && (NPC.downedMoonlord);
        }
        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.ForceRoar, player.position);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Calypsical").Type);
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.LunarBar, 16)
            .AddIngredient(ItemID.Ruby, 10)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
    }
}