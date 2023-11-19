using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    public class eleScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 24, 50, 0);
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.player[Main.myPlayer].ZoneNormalSpace || Main.player[Main.myPlayer].ZoneBeach)
            {
                player.statDefense += 10;
                player.GetCritChance(DamageClass.Generic) += 16;
                player.GetDamage(DamageClass.Generic) += .15f;
                player.accMerman = true;
                player.lifeRegen += 6;
            }

        }
    }
}