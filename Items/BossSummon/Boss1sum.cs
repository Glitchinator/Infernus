using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.BossSummon
{
    public class Boss1sum : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noUseGraphic = true;
            Item.value = 0;
            Item.rare = ItemRarityID.Blue;
            //Item.consumable = true;
            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<Raiko_Loc>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 vec = new(InfernusWorld.Raiko_Alter_X, InfernusWorld.Raiko_Alter_Y);

            Projectile.NewProjectileDirect(source, position, (vec - position).SafeNormalize(Vector2.Zero) * 2f, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Scorched_Sinew>(), 25)
            .AddTile(TileID.Furnaces)
            .Register();
        }
    }
}