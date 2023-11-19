using Infernus.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
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
        int Ice_Shard_Area;
        private Vector2 destination;
        float inertia = 4;
        float speed = 16f;

        public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 44;
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
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            NPC.dontTakeDamage = true;

            if (Despawn())
            {
                return;
            }
            Timer++;
            if (InfernusSystem.Level_systemON == true)
            {
                if (Timer <= 519)
                {
                    inertia = 5;
                    speed = 18f;
                    Ice_Shard_Area = 120;
                }
                if (Timer == 360)
                {
                    Pre_Wall();
                    SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                }
                if (Timer == 420)
                {
                    Pre_Wall();
                    SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                }
                if (Timer == 480)
                {
                    Pre_Wall();
                    SoundEngine.PlaySound(SoundID.Item35, NPC.position);
                }
                if (Timer >= 520)
                {
                    Ice_Wall();
                    SoundEngine.PlaySound(SoundID.Item105, NPC.position);
                    Ice_Shard_Area = 30;
                    inertia = 5;
                    speed = 10f;
                }
                if (Timer == 720)
                {
                    Timer = 0;
                }
                Form_Ice();
                return;
            }
            if (Timer <= 619)
            {
                inertia = 4;
                speed = 16f;
                Ice_Shard_Area = 120;
            }
            if (Timer == 440)
            {
                Pre_Wall();
                SoundEngine.PlaySound(SoundID.Item35, NPC.position);
            }
            if (Timer == 500)
            {
                Pre_Wall();
                SoundEngine.PlaySound(SoundID.Item35, NPC.position);
            }
            if (Timer == 560)
            {
                Pre_Wall();
                SoundEngine.PlaySound(SoundID.Item35, NPC.position);
            }
            if (Timer >= 620)
            {
                Ice_Wall();
                SoundEngine.PlaySound(SoundID.Item105, NPC.position);
                Ice_Shard_Area = 42;
                inertia = 2;
                speed = 8f;
            }
            if (Timer == 820)
            {
                Timer = 0;
            }
            Form_Ice();
        }
        private void Pre_Wall()
        {
            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.SpectreStaff, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f);
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
                if (Main.rand.Next(3) < 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Ice_Wall>(), 13, NPC.whoAmI);
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

            RotationTimer += 0.8f;
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

            if (InfernusSystem.Level_systemON == true)
            {
                if (Timer >= 510)
                {
                    destination = player.Center + offset;
                }
                else
                {
                    destination = Main.npc[ParentIndex].Center + offset;
                }
            }
            else
            {
                if (Timer >= 610)
                {
                    destination = player.Center + offset;
                }
                else
                {
                    destination = Main.npc[ParentIndex].Center + offset;
                }
            }

            Vector2 toDestination = destination - NPC.Center;
            Vector2 toDestinationNormalized = toDestination.SafeNormalize(Vector2.Zero);

            Vector2 moveTo = toDestinationNormalized * speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
        }
    }
}