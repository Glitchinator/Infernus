using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Melee
{
    public class lumsword : ModItem
    {
        public override string Texture => "Infernus/Projectiles/Luminite_Sword_Proj";
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 280;
            Item.DamageType = DamageClass.Melee;
            Item.width = 78;
            Item.height = 78;
            Item.useTime = 20;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 8f;
            Item.value = Item.buyPrice(0, 26, 50, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item19;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Luminite_Sword_Proj>();
            Item.shootSpeed = 26f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Weapon.HardMode.Melee.Scythe>(), 1)
            .AddIngredient(ItemID.LunarBar, 18)
            .AddTile(TileID.MythrilAnvil)
            .Register();
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
    }
}