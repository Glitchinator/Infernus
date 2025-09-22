using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Magic
{
    public class gun : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Magic;
            Item.width = 60;
            Item.height = 26;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 210000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = new SoundStyle("Infernus/Sounds/Plasma")
            {
                MaxInstances = 5
            };
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Plasma_Ball>();
            Item.autoReuse = true;
            Item.shootSpeed = 22f;
            Item.mana = 6;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

            return false;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SpaceGun, 1)
            .AddIngredient(ItemID.ChlorophyteBar, 16)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}