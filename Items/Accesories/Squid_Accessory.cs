using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Accesories
{
    public class Squid_Accessory : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Squid Heart");
            Tooltip.SetDefault("Increases your max number of minions by 1"
                + "\n 8% increased summon damage" + "\n 7% reduced mana cost");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 38;
            Item.accessory = true;
            Item.value = 75000;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += .08f;
            player.maxMinions++;
            player.manaCost -= .07f;
        }
    }
}
