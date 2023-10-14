using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class Dwarfern_Pickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dwarfven Pickaxe");
            Tooltip.SetDefault("Gets more powerful in hardmode" +
                 "\n Right click to salute your fellow dwarfs" +
                 "\n 'Rock and Stone, it never gets old'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public static readonly Color RocknStone = new(194, 194, 163);
        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.Melee;
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.value = 50000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.axe = 12;
            Item.pick = 45;
            Item.useTurn = true;
        }
        public override void UpdateInventory(Player player)
        {
            if (Main.hardMode == true)
            {
                Item.damage = 52;
                Item.axe = 22;
                Item.pick = 110;
            }
            else
            {
                SetDefaults();
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useAnimation = 130;
                Item.useTime = 130;
                Item.axe = 0;
                Item.pick = 0;
                Item.autoReuse = false;
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), RocknStone, Main.rand.Next(new[] { "Rock and Stone!", "ROCK... AND... STONE!", "Rock and Stone forever!", "That's it lads! Rock and Stone!", "None can stand before us!", "We fight for Rock and Stone!", "If you don't Rock and Stone, you ain't comin' home!", "Did I hear a Rock and Stone?", "For Karl!", "Leave No Dwarf Behind!", "Rock and Stone, Brother!", "Rock and Stone in the Heart!", "Come on guys! Rock and Stone!", "Like that! Rock and Stone!", "Rock and Stone to the Bone!" }), true);

                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(player.name + " salutes his fellow dwarfs", RocknStone);
                }
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    Main.NewText(player.name + " salutes his fellow dwarfs", RocknStone);
                }
            }
            else
            {
                SetDefaults();
            }
            return base.CanUseItem(player);
        }
    }
}
