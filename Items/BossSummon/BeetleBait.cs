using Infernus.Items.Materials;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.BossSummon
{
    public class BeetleBait : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 22;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = 0;
            Item.rare = ItemRarityID.Yellow;
            Item.consumable = true;
            Item.maxStack = 20;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Shark").Type) && (player.ZoneBeach);
        }
        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.ForceRoar, player.position);
            if (Main.netMode != NetmodeID.MultiplayerClient)

            {
                NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Shark").Type);
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SharkFin, 1)
            .AddIngredient(ItemID.SandBlock, 16)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}