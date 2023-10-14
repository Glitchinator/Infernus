using Infernus.Placeable;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Melee
{
    public class Trueprimed : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prime Light's Edge");
            Tooltip.SetDefault("spawns 3 shards when fully thrown,Shoots white daggers at enemies, has a chance to spawn daggers on hit, inflicts cursed flame");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.width = 38;
            Item.height = 34;
            Item.useTime = 28;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(0, 36, 50, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item19;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.LightTruePrimed>();
            Item.channel = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<TrueLight>(), 1)
            .AddIngredient(ItemID.BrokenHeroSword, 1)
            .AddIngredient(ModContent.ItemType<Rock>(), 26)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
