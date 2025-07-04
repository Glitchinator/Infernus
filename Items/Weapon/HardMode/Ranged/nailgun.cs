using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Ranged
{
    public class nailgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 170;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 60;
            Item.height = 28;
            Item.useAnimation = 6;
            Item.useTime = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 19, 75, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item108;
            Item.shoot = ModContent.ProjectileType<Vivid_Glory_Round>();
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shootSpeed = 22f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, 0);
        }
        public override void UseAnimation(Player player)
        {
            for (int k = 0; k < 7; k++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                Dust Sword = Dust.NewDustPerfect(player.Center + speed * 32, DustID.Vortex, speed * 3, Scale: 1f);
                Sword.noGravity = true;
            }
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.LunarBar, 16)
            .AddIngredient(ItemID.IllegalGunParts, 2)
            .AddIngredient(ItemID.FragmentVortex, 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}