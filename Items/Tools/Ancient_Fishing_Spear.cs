using Infernus.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Tools
{
    public class Ancient_Fishing_Spear : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 120;
            Item.height = 120;
            Item.useTime = 34;
            Item.useAnimation = 34;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0f;
            Item.value = Item.buyPrice(0, 14, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ancient_Fishing_Spear_Proj>();
            Item.shootSpeed = 14f;
            Item.maxStack = 9999;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe(22)
            .AddIngredient(ItemID.CopperBar, 6)
            .AddIngredient(ModContent.ItemType<Cursed_Plasma>(), 10)
            .AddTile(TileID.AlchemyTable)
            .Register();
        }
    }
}
