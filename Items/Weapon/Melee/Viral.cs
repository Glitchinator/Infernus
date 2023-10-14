using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Melee
{
    public class Viral : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rot Ball");
            Tooltip.SetDefault("Throw out an evil flail that lodges spikes in enemies.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(0, 6, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.ViralCRIM>();
            Item.channel = true;
            Item.noUseGraphic = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 10)
            .AddIngredient(ItemID.TissueSample, 14)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.DemoniteBar, 10)
            .AddIngredient(ItemID.ShadowScale, 14)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 1; i++)
            {
                type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<Projectiles.Viral>(), ModContent.ProjectileType<Projectiles.ViralCRIM>() });
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), position, velocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}