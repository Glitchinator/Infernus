using Infernus.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Weapon.HardMode.Summon
{
    public class eternal : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 46;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 8;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 22, 50, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item96;
            Item.shoot = ModContent.ProjectileType<Projectiles.Eternalflame>();
            Item.buffType = ModContent.BuffType<flameBuff>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            for (int i = 1; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Ectoplasm, 8)
            .AddIngredient(ItemID.SpookyWood, 24)
            .AddIngredient(ItemID.SoulofFright, 8)
            .AddIngredient(ModContent.ItemType<Weapon.Summon.Minion>(), 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}