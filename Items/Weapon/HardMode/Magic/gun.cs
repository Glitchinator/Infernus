using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Magic
{
    public class gun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DRAK-25 Plasma Carbine");
            Tooltip.SetDefault("God Damn it! There is a pebble in my boot! ");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 46;
            Item.DamageType = DamageClass.Magic;
            Item.width = 60;
            Item.height = 26;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 210000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = new SoundStyle("Infernus/Sounds/Plasma")
            {
                MaxInstances = 3
            };
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Plasma_Ball>();
            Item.autoReuse = true;
            Item.shootSpeed = 18f;
            Item.mana = 6;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
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