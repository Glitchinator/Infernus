using Infernus.Projectiles;
using Infernus.Projectiles.Temporal_Glow_Squid.Boss;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    public class Ruderibus_Hand : ModNPC
    {
        private Player player;
        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasParent => ParentIndex > -1;

        public int PositionIndex
        {
            get => (int)NPC.ai[1] - 1;
            set => NPC.ai[1] = value + 1;
        }
        public int Which_Fist;

        public bool HasPosition => PositionIndex > -1;

        public ref float RotationTimer => ref NPC.ai[2];

        public static int BodyType()
        {
            return ModContent.NPCType<Ruderibus>();
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }
        int Timer;
        int Ice_Shard_Area = 120;
        private Vector2 destination;
        float inertia = 6;
        float speed = 16f;
        bool dashing = false;

        private Vector2 Player_Position;
        bool should_explode = false;

        bool speed_up = false;

        bool circle_player = false;

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 34;
            NPC.damage = 40;
            NPC.defense = 6;
            NPC.lifeMax = 600;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.0f;
            NPC.netAlways = true;
            NPC.aiStyle = -1;
        }
        public override void AI()
        {
            Timer = InfernusWorld.Ruderibus_Timer;
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            NPC.dontTakeDamage = true;
            NPC.despawnEncouraged = false;
            Player player = Main.player[NPC.target];

            if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                //NPC.velocity.Y = NPC.velocity.Y + .3f;
                if (NPC.timeLeft > 20)
                {
                    NPC.timeLeft = 20;
                    return;
                }
            }

            if (Despawn())
            {
                return;
            }

            if (Timer == 220)
            {
                Ice_Shard_Area = 24;
                circle_player = true;
            }
            if (Timer == 340)
            {
                dashing = true;

                if (Which_Fist == 0)
                {
                    //Ice_Shard_Area = 0;
                    NPC.color = Color.Green;
                    Dash();
                }
                Ice_Shard_Area = 120;
            }
            if (Timer == 420)
            {
                if (Which_Fist == 1)
                {
                    //Ice_Shard_Area = 0;
                    NPC.color = Color.Blue;
                    Dash();
                }
                Ice_Shard_Area = 120;
            }
            if (Timer == 500)
            {
                if (Which_Fist == 2)
                {
                    //Ice_Shard_Area = 0;
                    NPC.color = Color.Red;
                    Dash();
                }
                Ice_Shard_Area = 120;
            }
            if (Timer == 580)
            {
                if (Which_Fist == 3)
                {
                    //Ice_Shard_Area = 0;
                    NPC.color = Color.Yellow;
                    Dash();
                }
                Ice_Shard_Area = 120;
            }
            if(Timer == 660)
            {
                Ice_Shard_Area = 120;
                circle_player = false;
                dashing = false;
            }

            if(Timer == 960)
            {
                Ice_Shard_Area = 24;
            }

            if(Timer == 1040)
            {
                Horizontal_Ice();
            }
            if (Timer == 1060)
            {
                Horizontal_Ice();
            }
            if (Timer == 1080)
            {
                Horizontal_Ice();
            }
            if (Timer == 1100)
            {
                Horizontal_Ice();
            }
            if (Timer == 1140)
            {
                Ice_Shard_Area = 120;
            }

            if(Timer == 1420)
            {
                Ice_Shard_Area = 30;
                circle_player = true;
                inertia = 3;
            }
            if(Timer == 1500)
            {
                dashing = true;
            }
            if(Timer == 1550)
            {
                //Ice_Shard_Area = 0;
                NPC.color = Color.Yellow;
                Player_Position = player.Center;
                should_explode = true;
                Dash();
            }
            if (should_explode == true)
            {
                var inertia2 = 12f;

                Vector2 direction = NPC.Center - Player_Position;
                direction.Normalize();
                direction *= -16;
                NPC.velocity = (NPC.velocity * (inertia2 - 1) + direction) / inertia2;
                var distancevec2 = NPC.Center - Player_Position;
                float magnitude = Magnitude(distancevec2);
                if (magnitude <= 100f)
                {
                    //dashing = false;
                    Ice_Shard_Area = 120;
                    for (int i = 0; i < 30; i++)
                    {
                        var smoke = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                        smoke.velocity *= 1.4f;
                    }

                    // Spawn a bunch of fire dusts.
                    for (int j = 0; j < 20; j++)
                    {
                        var fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.HallowedPlants, 0f, 0f, 100, default, 3.5f);
                        fireDust.noGravity = true;
                        fireDust.velocity *= 7f;
                        fireDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Glass, 0f, 0f, 100, default, 1.5f);
                        fireDust.velocity *= 3f;
                    }

                    // Spawn a bunch of smoke gores.
                    for (int k = 0; k < 2; k++)
                    {
                        float speedMulti = 0.8f;
                        if (k == 1)
                        {
                            speedMulti = 1.6f;
                        }

                        var smokeGore = Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                        smokeGore.velocity *= speedMulti;
                        smokeGore.velocity += Vector2.One;
                        smokeGore = Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                        smokeGore.velocity *= speedMulti;
                        smokeGore.velocity.X -= 1.2f;
                        smokeGore.velocity.Y += 1.2f;
                        smokeGore = Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                        smokeGore.velocity *= speedMulti;
                        smokeGore.velocity.X += 1.2f;
                        smokeGore.velocity.Y -= 1.2f;
                        smokeGore = Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
                        smokeGore.velocity *= speedMulti;
                        smokeGore.velocity -= Vector2.One;
                    }
                    Player_Position = Vector2.Zero;
                    should_explode = false;
                    circle_player = false;
                    //dashing = false;
                    inertia = 6;
                }
            }
            if (Timer == 1600)
            {
                Player_Position = Vector2.Zero;
                should_explode = false;
                circle_player = false;
                dashing = false;
                inertia = 6;
            }
            if(Timer == 1900)
            {
                speed_up = true;
            }
            if(Timer == 2040)
            {
                //ruderibus done layer ice mines
            }
            
            if(Timer == 2300)
            {
                dashing = true;
            }
            if(Timer >= 2360 && Timer < 2540)
            {
                if (Which_Fist == 0)
                {
                    //Ice_Shard_Area = 0;
                    NPC.Center = player.Center + new Vector2(-300f, -500f);
                }
                Ice_Shard_Area = 120;
                if (Which_Fist == 1)
                {
                    //Ice_Shard_Area = 0;
                    NPC.Center = player.Center + new Vector2(300f, -500f);
                }
                Ice_Shard_Area = 120;
                if (Which_Fist == 2)
                {
                    //Ice_Shard_Area = 0;
                    NPC.Center = player.Center + new Vector2(-100f, -500f);
                }
                Ice_Shard_Area = 120;
                if (Which_Fist == 3)
                {
                    //Ice_Shard_Area = 0;
                    NPC.Center = player.Center + new Vector2(100f, -500f);
                }
                Ice_Shard_Area = 120;
            }
            if(Timer == 2540)
            {
                NPC.velocity.Y = 20;
                NPC.noTileCollide = false;
            }
            if(Timer == 2660)
            {
                dashing = false;
                NPC.noTileCollide = true;
            }
            if(Timer == 2790)
            {
                InfernusWorld.Ruderibus_Timer = 0;
            }
            
            /*
            if(Timer == 1060)
            {
                InfernusWorld.Ruderibus_Timer = 0;
                InfernusWorld.Ruderibus_Switch = false;
                Ice_Shard_Area = 120;
            }
            if (Timer == 698)
            {
                NPC.velocity = new Vector2(0, 0);
            }
            */
            if (dashing == true && !(Timer <= 1630 && Timer >= 1550))
            {
                NPC.velocity.Y = NPC.velocity.Y * 0.98f;
                NPC.velocity.X = NPC.velocity.X * 0.98f;
                return;
            }
            
            if (dashing == false)
            {
                Form_Ice();
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("Infernus/NPCs/Ruderibus_Arm");

            Rectangle? chainSourceRectangle = null;

            Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
            Vector2 chainDrawPosition = NPC.Center;
            Vector2 vectorFromProjectileToPlayerArms = Main.npc[ParentIndex].Center.MoveTowards(chainDrawPosition, 0f) - chainDrawPosition;
            Vector2 unitVectorFromProjectileToPlayerArms = vectorFromProjectileToPlayerArms.SafeNormalize(Vector2.Zero);
            float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height());
            if (chainSegmentLength == 0)
            {
                chainSegmentLength = 10;
            }
            float chainRotation = unitVectorFromProjectileToPlayerArms.ToRotation() + MathHelper.PiOver2;
            int chainCount = 0;
            float chainLengthRemainingToDraw = vectorFromProjectileToPlayerArms.Length() + chainSegmentLength;

            while (chainLengthRemainingToDraw > 0f)
            {
                var chainTextureToDraw = chainTexture;
                Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, drawColor, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);
                chainDrawPosition += unitVectorFromProjectileToPlayerArms * chainSegmentLength;
                chainCount++;
                chainLengthRemainingToDraw -= chainSegmentLength;
            }
            return true;
        }
        private void Dash()
        {
            dashing = true;
            player = Main.player[NPC.target];
            var distancevec2 = player.position - NPC.position;
            float magnitude = Magnitude(distancevec2);
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.X *= 2.25f;
                NPC.velocity.Y *= 2.25f;
                {
                    float rotation = (float)Math.Atan2(NPC.Center.Y - (player.position.Y + player.velocity.Y * 10), NPC.Center.X - (player.position.X + player.velocity.X * 10));
                    if (magnitude > 400f)
                    {
                        magnitude = 400f;
                    }
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 14 * (magnitude / 350)) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 14 * (magnitude / 350)) * -1;
                    string i = magnitude.ToString();
                    Main.NewText(i, 229, 214, 127);
                }
            }
        }

        private void Ice_Wall()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = NPC.Center;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 0f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 0f);
                }
                if (Main.rand.NextBool(24))
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Ice_Wall>(), 11, NPC.whoAmI);
                }
            }
        }
        private void Horizontal_Ice()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, new Vector2(10f,0f), ModContent.ProjectileType<InkBolt>(), 10, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, new Vector2(-10f, 0f), ModContent.ProjectileType<InkBolt>(), 10, NPC.whoAmI);
            }
        }
        private static float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        private bool Despawn()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && (!HasPosition || !HasParent || !Main.npc[ParentIndex].active || Main.npc[ParentIndex].type != BodyType()))
            {
                NPC.active = false;
                NPC.life = 0;
                NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
                for (int k = 0; k < 16; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ApprenticeStorm, 4f, -2.5f, 0, default, 1f);
                }
                for (int k = 0; k < 16; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.HallowedPlants, 4f, -2.5f, 0, default, 1f);
                }
                return true;
            }
            return false;
        }
        private void Form_Ice()
        {
            Player player = Main.player[NPC.target];

            float rad = (float)PositionIndex / 4 * MathHelper.TwoPi;

            float lerp = 0f;
            /*
            if (dashing == true || speed_up == true)
            {
                lerp += 0.1f;
                 RotationTimer += 1.6f + lerp;
            }
            else
            */
            {
                RotationTimer += 0.8f;
                lerp = 0f;
            }
            if (RotationTimer > 360)
            {
                RotationTimer = 0;
            }
            float continuousRotation = MathHelper.ToRadians(RotationTimer);
            rad += continuousRotation;
            if (rad > MathHelper.TwoPi)
            {
                rad -= MathHelper.TwoPi;
            }
            else if (rad < 0)
            {
                rad += MathHelper.TwoPi;
            }

            float distanceFromBody = Main.npc[ParentIndex].width + NPC.width + Ice_Shard_Area;

            Vector2 offset = Vector2.One.RotatedBy(rad) * distanceFromBody;

            if (circle_player == true)
            {
                destination = player.Center + offset;
            }
            else
            {
                destination = Main.npc[ParentIndex].Center + offset;
            }

            Vector2 toDestination = destination - NPC.Center;
            Vector2 toDestinationNormalized = toDestination.SafeNormalize(Vector2.Zero);
            Vector2 moveTo = toDestinationNormalized * speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
        }
    }
}