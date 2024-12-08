using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class CrimStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Magic;
            Item.width = 42;
            Item.height = 42;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 1, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<CrimShot>();
            Item.shootSpeed = 13f;
            Item.mana = 9;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld;
            for (int k = 0; k < 10; k++)
            {
                Vector2 speed = Main.rand.NextVector2Unit();
                Dust wand = Dust.NewDustPerfect(position + speed * 32, DustID.VampireHeal, speed * 2, 0, default, Scale: 2f);
                wand.noGravity = true;
            }
            Projectile.NewProjectileDirect(source, position, new Vector2(0, -4), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, new Vector2(0, 4), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, new Vector2(4, 0), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, new Vector2(-4, 0), type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 8)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}