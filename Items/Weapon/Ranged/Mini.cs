using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Ranged
{
    public class Mini : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        int shot = 0;

        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 22;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 6, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 15f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Hot>(), 32)
            .AddIngredient(ItemID.Minishark, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override void UseAnimation(Player player)
        {
            for (int k = 0; k < 11; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                Dust Sword = Dust.NewDustPerfect(player.Center + speed * 32, DustID.SolarFlare, speed * 3, Scale: 1f);
                Sword.noGravity = true;
            }
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .33f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shot++;
            if (shot == 10)
            {
                type = ModContent.ProjectileType<Molten_Chaingun>();
                shot = 0;
            }
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

            return false;
        }
    }
}