using Infernus.Placeable;
using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    public class Cursed_Wanderer : ModNPC
    {
        private Player player;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Wanderer");
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 3500;
            NPC.damage = 36;
            NPC.defense = 24;
            NPC.knockBackResist = 0.0f;
            NPC.width = 38;
            NPC.height = 56;
            NPC.aiStyle = 22;
            NPC.noGravity = true;
            NPC.noTileCollide =true;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 7000;
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
            if (timer == 180)
            {
                timer = 0;
            }


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
                Vector2 position = player.Center + new Vector2(450f, 0f);
                NPC.Center = position;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
        }
        private void Teleport_Right()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = player.Center + new Vector2(-450f, 0f);
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
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 24; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GlowingMushroom, 2.5f * hitDirection, -2.5f, 0, default, 1.2f);
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.GoldenKey, 3, 1, 3));
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