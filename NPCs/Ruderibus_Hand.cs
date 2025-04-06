using Infernus.Projectiles;
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
            NPC.DoesntDespawnToInactivity();
            NPC.DiscourageDespawn(60);

            if (Despawn())
            {
                return;
            }
            /*
            string i = Which_Fist.ToString();
            if (Timer == 120)
            {
                Main.NewText(i, 229, 214, 127);
            }
            if (Timer <= 399)
            {
                inertia = 6;
                speed = 14f;
                Ice_Shard_Area = 120;
            }
            if (Timer == 400)
            {
                Ice_Shard_Area = 10;
            }
            if (Timer == 440)
            {
                SoundEngine.PlaySound(SoundID.Item35, NPC.position);
            }
            if (Timer == 500)
            {
                SoundEngine.PlaySound(SoundID.Item35, NPC.position);
            }
            if (Timer == 560)
            {
                SoundEngine.PlaySound(SoundID.Item35, NPC.position);
            }
            if (Timer >= 620 && InfernusWorld.Ruderibus_Switch == true)
            {
                //Ice_Wall();
               // SoundEngine.PlaySound(SoundID.Item105, NPC.position);
                //Ice_Shard_Area = 36;
                inertia = 2;
                speed = 8f;
            }
            if (Timer == 560)
            {
                Ice_Shard_Area = 10;
            }
            */
            if(Timer == 500 || Timer == 100)
            {
                Ice_Shard_Area = 1;
            }
            if(Timer <= 700 && Timer >= 500)
            {
                Ice_Wall();
            }
            if(Timer == 301)
            {
                Ice_Shard_Area = 120;
            }

            if (Timer == 700)
            {
                if (Which_Fist == 0)
                {
                    Ice_Shard_Area = 0;
                    NPC.color = Color.Green;
                    Dash();
                }
                else
                {
                    Ice_Shard_Area = 120;
                }
            }
            if (Timer == 800)
            {
                if (Which_Fist == 1)
                {
                    Ice_Shard_Area = 0;
                    NPC.color = Color.Blue;
                    Dash();
                }
                else
                {
                    Ice_Shard_Area = 120;
                }
            }
            if (Timer == 900)
            {
                if (Which_Fist == 2)
                {
                    Ice_Shard_Area = 0;
                    NPC.color = Color.Red;
                    Dash();
                }
                else
                {
                    Ice_Shard_Area = 120;
                }
            }
            if (Timer == 1000)
            {
                if (Which_Fist == 3)
                {
                    Ice_Shard_Area = 0;
                    NPC.color = Color.Yellow;
                    Dash();
                }
                else
                {
                    Ice_Shard_Area = 120;
                }
            }
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
            if (Timer >= 700)
            {
                NPC.velocity.Y = NPC.velocity.Y * 0.98f;
                NPC.velocity.X = NPC.velocity.X * 0.98f;
                return;
            }
            Form_Ice();
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
                    if (magnitude > 0f)
                    {
                        NPC.velocity.X = (float)(Math.Cos(rotation) * 14 * (magnitude / 250)) * -1;
                        NPC.velocity.Y = (float)(Math.Sin(rotation) * 14 * (magnitude / 250)) * -1;
                    }
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
            if (Timer <= 700 && Timer >= 500)
            {
                lerp += 0.1f;
                 RotationTimer += 1.6f + lerp;
            }
            else
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

            if (InfernusWorld.Ruderibus_Switch == true)
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