using Infernus.NPCs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus
{
    public class InfernusItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if(item.type == ItemID.GolemFist)
            {
                item.damage = 110;
            }
            if (item.type == ItemID.TacticalShotgun)
            {
                item.useTime = 18;
                item.useAnimation = 18;
            }
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (item.type == ItemID.GolemFist)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(13));
                int i = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), position, newVelocity * 0.8f, type, damage, knockback);
                Main.projectile[i].usesLocalNPCImmunity = true;
                Main.projectile[i].usesIDStaticNPCImmunity = false;
                Main.projectile[i].localNPCHitCooldown = 12;
            }
        }
        public override void UpdateEquip(Item item, Player player)
        {
            if (item.type == ItemID.RubyRobe)
            {
                Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Ruby_Robe = true;
            }
            if (item.type == ItemID.EmeraldRobe)
            {
                Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Emerald_Robe = true;
            }
            if (item.type == ItemID.DiamondRobe)
            {
                Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Diamond_Robe = true;
            }
            if (item.type == ItemID.AmberRobe)
            {
                Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Amber_Robe = true;
            }
            if (item.type == ItemID.AmethystRobe)
            {
                Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Amethyst_Robe = true;
            }
            if (item.type == ItemID.TopazRobe)
            {
                Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Topaz_Robe = true;
            }
            if (item.type == ItemID.SapphireRobe)
            {
                Main.LocalPlayer.GetModPlayer<InfernusPlayer>().Sapphire_Robe = true;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.RubyRobe)
            {
                tooltips.Add(new(Mod, "Gem Robe Combo", "Ruby Staff costs 0 mana"));
            }
            if (item.type == ItemID.EmeraldRobe)
            {
                tooltips.Add(new(Mod, "Gem Robe Combo", "Emerald Staff costs 0 mana"));
            }
            if (item.type == ItemID.DiamondRobe)
            {
                tooltips.Add(new(Mod, "Gem Robe Combo", "Diamond Staff costs 0 mana"));
            }
            if (item.type == ItemID.AmberRobe)
            {
                tooltips.Add(new(Mod, "Gem Robe Combo", "Amber Staff costs 0 mana"));
            }
            if (item.type == ItemID.AmethystRobe)
            {
                tooltips.Add(new(Mod, "Gem Robe Combo", "Amethyst Staff costs 0 mana"));
            }
            if (item.type == ItemID.SapphireRobe)
            {
                tooltips.Add(new(Mod, "Gem Robe Combo", "Sapphire Staff costs 0 mana"));
            }
            if (item.type == ItemID.TopazRobe)
            {
                tooltips.Add(new(Mod, "Gem Robe Combo", "Topaz Staff costs 0 mana"));
            }
            if (item.type == ItemID.WetBomb)
            {
                tooltips.Add(new(Mod, "Angler", "Buy more from the demolitionist"));
            }
        }
    }
}
