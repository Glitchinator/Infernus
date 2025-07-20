using Infernus.Invas;
using Infernus.Items.Materials;
using Infernus.Items.Weapon.HardMode.Accessories;
using Infernus.NPCs;
using Infernus.Projectiles;
using Infernus.Projectiles.Temporal_Glow_Squid.Boss;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Infernus
{
    public class EffectNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public List<Vector2> Equite_Knuckles_Stacking = [];

        public Vector2 Haunted_Status = Vector2.Zero;
        public static NPC Haunted_NPC;
        public static int damage;

        public override void ResetEffects(NPC npc)
        {
            int count = Equite_Knuckles_Stacking.Count;
            for (int z = 0; z < count; z++)
            {
                Vector2 Equite_Knuckles_Buff = Equite_Knuckles_Stacking[z];
                if (Equite_Knuckles_Buff.Y <= 0)
                {
                    Equite_Knuckles_Stacking.Remove(Equite_Knuckles_Buff);
                    count--;
                    z--;
                    continue;
                }
                else
                {
                    Equite_Knuckles_Stacking[z] = new Vector2(Equite_Knuckles_Buff.X, Equite_Knuckles_Buff.Y - 1);
                }
            }
            // very similiar to how equite leafs work, first checks if the player has a target and if that target is active
            if (Haunted_NPC != null)
            {
                if (Haunted_NPC.active == true)
                {
                    // jsut to make sure there can only be one corrupted enemy at a time
                    if (Haunted_NPC == npc)
                    {
                        // now that we jsut hit an enemy, spawn the mask projectile and start counting timer down
                        if (Haunted_Status.Y == 900)
                        {
                            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), npc.Center, Vector2.Zero, ModContent.ProjectileType<Corrupted_Mask>(), 0, 0f);
                        }
                        if (Haunted_Status.Y > 0)
                        {
                            Haunted_Status.Y--;
                        }
                        string i = Haunted_Status.ToString();
                        damage = (int)Haunted_Status.X;
                        Main.NewText(i, 229, 214, 127);
                    }
                }
                else
                {
                    // if at any point the npc dies, reset
                    Haunted_NPC = null;
                    Haunted_Status = Vector2.Zero;
                }
            }
            else
            {
                // if there is no target, make sure there aint none by reseting so no false positives
                Haunted_NPC = null;
            }

        }
        public override void OnKill(NPC npc)
        {
            if (Haunted_NPC == npc)
            {
                Haunted_NPC = null;
                Haunted_Status = Vector2.Zero;
            }

        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (Haunted_NPC == npc)
            {
                Haunted_Status.X += damageDone;
            }
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (Haunted_NPC == npc)
            {
                Haunted_Status.X += damageDone;
            }
        }
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            modifiers.Defense -= Equite_Knuckles_Stacking.Count;

            if (Haunted_NPC == npc)
            {
                modifiers.SourceDamage -= 0.3f;
            }
        }
    }
}
