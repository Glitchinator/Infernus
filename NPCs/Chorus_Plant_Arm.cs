using Infernus.Items.Weapon.HardMode.Summon;
using Infernus.Projectiles;
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
    public class Chorus_Plant_Arm: ModNPC
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
            return ModContent.NPCType<Chorus_Plant>();
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }
        int Timer;
        int Ice_Shard_Area = 50;
        private Vector2 destination;
        float inertia = 6;
        float speed = 12f;

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 38;
            NPC.damage = 60;
            NPC.defense = 32;
            NPC.lifeMax = 8000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.0f;
            NPC.netAlways = true;
            NPC.aiStyle = -1;
            NPC.npcSlots = 0;
        }
        public override void AI()
        {
            

            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            NPC.DoesntDespawnToInactivity();
            NPC.DiscourageDespawn(60);

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

            if (Despawn())
            {
                return;
            }
            Timer++;
            if(Timer == 90)
            {
                Shoot_Projectile_Random();
                Timer = 0;
            }
            Form_Ice();
        }
        public override void OnSpawn(IEntitySource source)
        {
            NPC.lifeMax = 8000;
            NPC.life = 8000;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("Infernus/NPCs/Chorus_Plant_Arm_Sprite");

            Rectangle? chainSourceRectangle = null;

            Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
            Vector2 chainDrawPosition = NPC.Bottom;
            Vector2 vectorFromProjectileToPlayerArms = Main.npc[ParentIndex].Bottom.MoveTowards(chainDrawPosition, 0f) - chainDrawPosition;
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
        private void Shoot_Projectile_Random()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                var type = Main.rand.Next(new int[] { ModContent.ProjectileType<Petal_Rocket>(), ModContent.ProjectileType<Petal_Ball>(), ModContent.ProjectileType<Petal_Bullet>() });
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

                if (Main.rand.Next(3) < 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, type, 24, NPC.whoAmI);
                }
                SoundEngine.PlaySound(SoundID.Item20, NPC.position);
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Confetti_Pink, 4f, -2.5f, 0, default, 1f);
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

            destination = Main.npc[ParentIndex].Center + offset;

            Vector2 toDestination = destination - NPC.Center;
            Vector2 toDestinationNormalized = toDestination.SafeNormalize(Vector2.Zero);

            Vector2 moveTo = toDestinationNormalized * speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
        }
        public override void OnKill()
        {
            Chorus_Plant.Arms_Left -= 1;
        }
    }
}