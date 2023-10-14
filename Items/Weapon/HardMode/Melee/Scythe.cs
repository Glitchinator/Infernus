using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Melee
{
    public class Scythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scythe of Dark");
            Tooltip.SetDefault("Throw out a Scythe that fades quickly but releases homing souls on hit");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 46;
            Item.DamageType = DamageClass.Melee;
            Item.width = 52;
            Item.height = 44;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(0, 16, 50, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item71;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Scythe>();
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 42f;
            Item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Weapon.Melee.Hatchet>(), 1)
            .AddIngredient(ItemID.SoulofNight, 14)
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