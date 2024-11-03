using Infernus.Placeable;
using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    [AutoloadBossHead]
    public class Chorus_Plant : ModNPC
    {
        private Player player;

        public int ArmIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasArms => ArmIndex > -1;

        public int PositionIndex2
        {
            get => (int)NPC.ai[1] - 1;
            set => NPC.ai[1] = value + 1;
        }

        public bool HasPosition1 => PositionIndex2 > -1;
        public static int BodyType2()
        {
            return ModContent.NPCType<Chorus_Plant_Arm>();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 20000;
            NPC.damage = 78;
            NPC.defense = 44;
            NPC.knockBackResist = 0.0f;
            NPC.width = 40;
            NPC.height = 68;
            NPC.aiStyle = -1;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 10000;
            NPC.boss = true;
            Music = MusicLoader.GetMusicSlot("Infernus/Music/Floral_Disruption");
        }
        int timer;

        public static int Arms_Left = 4;
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest(true);


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
            if (timer == 60)
            {
                Shoot();
            }

            if (timer == 100)
            {
                Shoot();
            }
            if (timer == 140)
            {
                Shoot();
            }
            if (timer == 180)
            {
                Shoot();
            }
            if (timer == 200)
            {
                Shoot();
            }
            if (timer == 220)
            {
                Spore_Shootgun();
            }
            if (timer == 260)
            {
                Spore_Shootgun();
                timer = 0;
            }

            if (Arms_Left == 0)
            {
                NPC.dontTakeDamage = false;
                return;
            }
            NPC.dontTakeDamage = true;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Arms_Left = 4;
            NPC.lifeMax = 20000;
            NPC.life = 20000;
            Spawn_IceShards();
        }
        private void Spawn_IceShards()
        {
            for (int i = 0; i < 4; i++)
            {
                int NPC_ = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Chorus_Plant_Arm>(), NPC.whoAmI);
                NPC Ice = Main.npc[NPC_];

                if (Ice.ModNPC is Chorus_Plant_Arm minion)
                {
                    minion.ParentIndex = NPC.whoAmI;
                    minion.PositionIndex = i;

                }

                if (Main.netMode == NetmodeID.Server && NPC_ < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: NPC_);
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 2;

            int frameSpeed = 8;
            NPC.frameCounter += 0.7f;
            NPC.frameCounter += NPC.velocity.Length() / 13f;
            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneJungle && spawnInfo.Player.ZoneRockLayerHeight && NPC.downedPlantBoss && InfernusNPC.Plant_Spawned == false)
            {
                return .1f;
            }
            return 0f;
        }
        private void Spore_Shootgun()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 6; i++)
                {
                    Vector2 velocity = player.Center - NPC.Center;
                    float magnitude = Magnitude(velocity);
                    if (magnitude > 0)
                    {
                        velocity *= 6f / magnitude;
                    }
                    else
                    {
                        velocity = new Vector2(0f, 6f);
                    }
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(34));
                    newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, newVelocity, ModContent.ProjectileType<Petal_Bullet>(), 24, NPC.whoAmI);
                    SoundEngine.PlaySound(SoundID.Item17, NPC.position);
                }
            }
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
                    velocity *= 9f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 9f);
                }

                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Petal_Bullet>(), 28, NPC.whoAmI);
                SoundEngine.PlaySound(SoundID.Item39, NPC.position);
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Confetti_Pink, 2.5f, -2.5f, 0, default, 1.2f);
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.LifeFruit, 2, 1, 4));
            npcLoot.Add(ItemDropRule.Common(ItemID.TurtleShell, 2, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.PlanteraBossBag, 1, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.ChlorophyteOre, 1, 24, 78));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.BossSummon.PlanteraBossSummon>(), 2, 1, 1));
        }
        public override void OnKill()
        {
            InfernusSystem.downedFlower = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("A wild flower that began consuming chlorophyte and growing into a stronger behemoth.")
            });
        }
    }
}