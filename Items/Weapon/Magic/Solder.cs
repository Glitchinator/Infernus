using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class Solder : ModItem
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
            Item.noMelee = true;
            Item.width = 30;
            Item.height = 30;
            Item.useAnimation = 30;
            Item.useTime = 15;
            Item.reuseDelay = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 4, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<Projectiles.Aeritite_Magic_Bomb>();
            Item.shootSpeed = 12.4f;
            Item.autoReuse = true;
            Item.mana = 10;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            position = Main.MouseWorld - new Vector2(Main.rand.NextFloat(201) * player.direction, -600f);
            position.Y -= 100;
            Vector2 heading = target - position;
            heading.Normalize();
            heading *= velocity.Length();
            heading.Y += Main.rand.Next(-30, 31) * 0.02f;
            Projectile.NewProjectile(source, position, heading, type, 18, knockback, player.whoAmI, 0f, 0);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Gaming>(), 9)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
