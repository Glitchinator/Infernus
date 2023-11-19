using Infernus.Buffs;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class Boulder_Expert : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 34;
            Item.value = Item.buyPrice(0, 24, 50, 0);
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
            Item.accessory = true;
            Item.defense = 4;
            Item.buffType = ModContent.BuffType<Boulder_Buff>();
            Item.shoot = ModContent.ProjectileType<Projectiles.Boulder_Orbit>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(Item.buffType, 2);
            player.endurance += .1f;
        }
        public override void UpdateEquip(Player player)
        {
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Boulder_Equipped = true;
        }
    }
}