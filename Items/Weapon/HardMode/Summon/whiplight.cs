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
        }

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<Projectiles.Whipele>(), 85, 3, 9, 24);
            Item.value = Item.buyPrice(0, 27, 50, 0);

            Item.rare = ItemRarityID.Yellow;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}