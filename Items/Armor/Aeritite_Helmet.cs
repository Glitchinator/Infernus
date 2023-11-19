using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Aeritite_Helmet : ModItem
    {
        int timer;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 6000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += .05f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Aeritite_Chestplate>() && legs.type == ModContent.ItemType<Aeritite_Leggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Aeritite energy surrounds you" + "\n reducing next hit damage by 33%" + "\n and increases all jump power" + "\n 30 Second recharge";
            if (player.HasBuff(ModContent.BuffType<Aeritite_Timer>()))
            {
                return;
            }
            player.endurance += .33f;
            player.jumpSpeedBoost += 4;
            Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Aeritite_Equipped = true;
            timer++;
            if(timer >= 30)
            {
                for (int k = 0; k < 7; k++)
                {
                    Vector2 speed_Dust = Main.rand.NextVector2Unit();
                    Dust wand = Dust.NewDustPerfect(player.Center + speed_Dust * 14, DustID.BlueCrystalShard, speed_Dust * 2, 0, default, Scale: 1.8f);
                    wand.noGravity = true;
                }
                timer = 0;
            }

        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Materials.Gaming>(), 20)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}