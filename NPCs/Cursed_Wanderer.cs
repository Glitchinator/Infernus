using Infernus.Items.Materials;
using Infernus.Items.Mounts;
using Infernus.Items.Weapon.Melee;
using Infernus.Items.Weapon.Ranged;
using Infernus.Items.Weapon.Summon;
using Infernus.Placeable;
using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    [AutoloadBossHead]
    public class Cursed_Wanderer : ModNPC
    {
        private Player player;
        public override void SetDefaults()
        {
            NPC.lifeMax = 3500;
            NPC.damage = 36;
            NPC.defense = 3;
            NPC.knockBackResist = 0.4f;
            NPC.width = 38;
            NPC.height = 56;
            NPC.aiStyle = 22;
            NPC.noGravity = true;
            NPC.noTileCollide =true;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 7000;
            NPC.boss = true;
            Music = MusicLoader.GetMusicSlot("Infernus/Music/The_Wanderer");
        }
        int timer;
        public override void AI()
        {

            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;


            if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.velocity.Y = NPC.velocity.Y + .3f;
                if (NPC.timeLeft > 20)
                {
                    NPC.timeLeft = 20;
                    return;
                }
            }
            timer++;
            if(timer == 0)
            {
                if (player.direction == 1)
                {
                    Teleport_Right();
                }
                if (player.direction == -1)
                {
                    Teleport_Left();
                }
            }

            if (timer == 80)
            {
                Shoot();
            }
            if (timer == 85)
            {
                Shoot();
            }
            if (timer == 90)
            {
                Shoot();
            }
            if(timer == 130)
            {
                if (player.direction == 1)
                {
                    Teleport_Right();
                }
                if (player.direction == -1)
                {
                    Teleport_Left();
                }
            }
            if (timer == 140)
            {
                Shoot();
            }
            if (timer == 145)
            {
                Shoot();
            }
            if (timer == 150)
            {
                Shoot();
            }
            if(timer >= 219)
            {
                NPC.damage = 0;
                NPC.velocity.Y = 0;
                NPC.velocity.X = 0;
            }
            if (timer == 220)
            {
                if (player.direction == 1)
                {
                    Teleport_Right_Close();
                }
                if (player.direction == -1)
                {
                    Teleport_Left_Close();
                }
            }
            if (timer == 330)
            {
                timer = 0;
            }


        }
        public override void OnSpawn(IEntitySource source)
        {
            NPC.lifeMax = 3500;
            NPC.life = 3500;
        }
        public override void OnKill()
        {
            InfernusSystem.downedWanderer = true;
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneDungeon && NPC.downedBoss3 && InfernusNPC.Cursed_Spawned == false)
            {
                return .1f;
            }
            return 0f;
        }
        private void Shoot()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 4f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 9f);
                }

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Cursed_Wanderer_Shot>(), 16, NPC.whoAmI);
                SoundEngine.PlaySound(SoundID.Item20, NPC.position);
            }
        }
        private void Teleport_Left()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(250f, 0f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        private void Teleport_Right()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(-250f, 0f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        private void Teleport_Left_Close()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(30f, 0f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        private void Teleport_Right_Close()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(-30f, 0f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;

            Rectangle frame;


            frame = texture.Frame();

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new(NPC.width / 2 - frameOrigin.X, NPC.height - frame.Height);
            Vector2 drawPos = NPC.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = NPC.activeTime / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
            {
                time = 2f - time;
            }

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(1, 87, 155, 50), NPC.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(1, 87, 155, 77), NPC.rotation, frameOrigin, 1f, SpriteEffects.None, 0);
            }
            return true;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 24; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GlowingMushroom, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.GoldenKey, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cursed_Plasma>(), 1, 12, 36));

            int[] Which_One = [ModContent.ItemType<Ghost_Pistol>()];

            npcLoot.Add(ItemDropRule.OneFromOptions(1, Which_One));
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("A soul, lost to the depths of the dungeon. Forever trapped inside a brick tomb.")
            });
        }
    }
}