using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Melee
{
    public class Cursed_Spear : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Spears[Item.type] = true;
        }
        public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Melee;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 22;
			Item.useAnimation = 22;
            Item.crit = 9;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
            Item.shootSpeed = 3.7f;
            Item.shoot = ModContent.ProjectileType<Wanderer_Spear>();
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Cursed_Plasma>(), 22)
            .AddIngredient(ItemID.Spear, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
