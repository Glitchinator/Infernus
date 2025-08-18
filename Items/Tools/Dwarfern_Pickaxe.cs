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
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public static readonly Color RocknStone = new(194, 194, 163);
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Melee;
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.value = 50000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = false;
            Item.axe = 12;
            Item.pick = 59;
            Item.useTurn = true;
            Item.shoot = ProjectileID.None;
            Item.useAmmo = AmmoID.None;
            Item.shootSpeed = 0f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 4)
            .AddRecipeGroup(RecipeGroupID.Wood, 16)
            .AddRecipeGroup(RecipeGroupID.IronBar, 8)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.DemoniteBar, 4)
            .AddRecipeGroup(RecipeGroupID.Wood, 16)
            .AddRecipeGroup(RecipeGroupID.IronBar, 8)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override void UpdateInventory(Player player)
        {
            if (Main.hardMode == true)
            {
                Item.damage = 32;
                Item.axe = 22;
                Item.pick = 119;
                return;
            }
            SetDefaults();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.axe = 0;
                Item.pick = 0;
                Item.noUseGraphic = true;
                Item.autoReuse = false;
                Item.useAmmo = AmmoID.FallenStar;
                Item.shoot = ProjectileID.StickyBomb;
                Item.shootSpeed = 5f;
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), RocknStone, Main.rand.Next(new[] { "Rock and Stone!", "ROCK... AND... STONE!", "Rock and Stone forever!", "That's it lads! Rock and Stone!", "None can stand before us!", "We fight for Rock and Stone!", "If you don't Rock and Stone, you ain't comin' home!", "Did I hear a Rock and Stone?", "For Karl!", "Leave No Dwarf Behind!", "Rock and Stone, Brother!", "Rock and Stone in the Heart!", "Come on guys! Rock and Stone!", "Like that! Rock and Stone!", "Rock and Stone to the Bone!" }), true);
            }
            else
            {
                Item.noUseGraphic = false;
                SetDefaults();
            }
            return base.CanUseItem(player);
        }
    }
}
