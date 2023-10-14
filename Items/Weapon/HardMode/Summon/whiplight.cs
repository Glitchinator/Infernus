using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Summon
{
    public class whiplight : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Volt Conductor");
            Tooltip.SetDefault("Your minions will attack struck foes"
                            + "\n Minions have a chance to rain lightning on hit"
                            + "\n + 12 summon tag damage");
        }

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<Projectiles.Whipele>(), 95, 3, 7);
            Item.value = Item.buyPrice(0, 27, 50, 0);
            Item.useTime = 10;

            Item.shootSpeed = 9;
            Item.rare = ItemRarityID.Yellow;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}