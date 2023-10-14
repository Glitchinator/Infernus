using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.Magic
{
    public class MeteorEater : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("StormGlobe");
            Tooltip.SetDefault("Throws a flask, which explodes into fireballs and rains meteors."
                + "\n [WEATHER ALERT]");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.width = 30;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 6, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item88;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Meteor_MagicFlask>();
            Item.shootSpeed = 13f;
            Item.mana = 12;
        }
    }
}