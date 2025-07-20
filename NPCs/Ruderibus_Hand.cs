using Infernus.Projectiles;
using Infernus.Projectiles.Raiko.Boss;
using Infernus.Projectiles.Temporal_Glow_Squid.Boss;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.CameraModifiers;
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

        public static int Life_Type()
        {
            return Main.npc[ModContent.NPCType<Ruderibus>()].whoAmI;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }
        int Timer;
        int Ice_Shard_Area = 120;
        private Vector2 destination;
        float inertia = 5;
        float speed = 16f;
        bool dashing = false;
        private Vector2 mub;

        private Vector2 Player_Position;
        bool should_explode = false;

        bool speed_up = false;

        bool circle_player = false;

        int rand_dist = 24;
        int randi;
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
            NPC.knockBackResist = 0.3f;
            NPC.netAlways = true;
            NPC.aiStyle = -1;
            //NPC.realLife = ParentIndex;
        }
        public override void AI()
        {
            Vector2 dest = NPC.Center - destination;
            float diddy = Magnitude(dest);
            if(diddy  >= 80f && dashing == false)
            {;
                NPC.damage = 0;
            }
            else
            {
                NPC.damage = 40;
            }

            NPC.realLife = Main.npc[ParentIndex].whoAmI;
            Timer = InfernusWorld.Ruderibus_Timer;
            NPC.netUpdate = true;
            NPC.TargetClosest();
            //NPC.dontTakeDamage = true;
            Player player = Main.player[NPC.target];

            Vector2 pos = player.Center - NPC.Center;
            var f = pos.ToRotation() + MathHelper.PiOver2;
            NPC.rotation = NPC.rotation.AngleTowards(f, 0.3f);

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
                NPC.knockBackResist = 0.0f;
            }
            if(Timer == 260)
            {
                if (Which_Fist == 0)
                {
                    Whoops_Ahh_Pimp_Down_Ahhhh_Pimp_IN_DISTRESS();
                }
            }
            if(Timer == 300)
            {
                if (Which_Fist == 0)
                {
                    Whoops_Ahh_Pimp_Down_Ahhhh_Pimp_IN_DISTRESS();
                }
            }
            if (Timer == 340)
            {
                dashing = true;

                if (Which_Fist == 0)
                {
                    //Ice_Shard_Area = 0;
                    //NPC.color = Color.Green;
                    Dash();
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                if (Which_Fist == 1)
                {
                    Whoops_Ahh_Pimp_Down_Ahhhh_Pimp_IN_DISTRESS();
                }
                //Ice_Shard_Area = 120;
            }
            if (Timer == 380)
            {
                if (Which_Fist == 1)
                {
                    Whoops_Ahh_Pimp_Down_Ahhhh_Pimp_IN_DISTRESS();
                }
            }
            if (Timer == 420)
            {
                if (Which_Fist == 1)
                {
                    //Ice_Shard_Area = 0;
                    //NPC.color = Color.Blue;
                    Dash();
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                if (Which_Fist == 2)
                {
                    Whoops_Ahh_Pimp_Down_Ahhhh_Pimp_IN_DISTRESS();
                }
                //Ice_Shard_Area = 120;
            }
            if (Timer == 460)
            {
                if (Which_Fist == 2)
                {
                    Whoops_Ahh_Pimp_Down_Ahhhh_Pimp_IN_DISTRESS();
                }
            }
            if (Timer == 500)
            {
                if (Which_Fist == 2)
                {
                    //Ice_Shard_Area = 0;
                    //NPC.color = Color.Red;
                    Dash();
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                if (Which_Fist == 3)
                {
                    Whoops_Ahh_Pimp_Down_Ahhhh_Pimp_IN_DISTRESS();
                }
                //Ice_Shard_Area = 120;
            }
            if (Timer == 540)
            {
                if (Which_Fist == 3)
                {
                    Whoops_Ahh_Pimp_Down_Ahhhh_Pimp_IN_DISTRESS();
                }
            }
            if (Timer == 580)
            {
                if (Which_Fist == 3)
                {
                    //Ice_Shard_Area = 0;
                    //NPC.color = Color.Yellow;
                    Dash();
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                //Ice_Shard_Area = 120;
            }
            if(Timer == 660)
            {
                //Ice_Shard_Area = 120;
                circle_player = false;
                dashing = false;
                NPC.knockBackResist = 0.3f;
            }

            if(Timer == 960)
            {
                Ice_Shard_Area = 24;
            }

            if(Timer == 1040)
            {
                Horizontal_Ice();
            }
            if (Timer == 1070)
            {
                Horizontal_Ice();
            }
            if (Timer == 1100)
            {
                Horizontal_Ice();
            }
            if (Timer == 1140)
            {
                if (Which_Fist == 0)
                {
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                if (Which_Fist == 1)
                {
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                if (Which_Fist == 2)
                {
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                if (Which_Fist == 3)
                {
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                //Ice_Shard_Area = 120;
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
                //NPC.color = Color.Yellow;
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
                randi = Main.rand.Next(1);
                if (magnitude <= 40f)
                {
                    Whoops_Ahh_Pimp_Down_Ahhhh_Pimp_IN_DISTRESS();
                    //dashing = false;
                    //Ice_Shard_Area = 120;
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
                    NPC.velocity = Vector2.Zero;
                    //dashing = false;
                    inertia = 5;
                    PunchCameraModifier modifier = new PunchCameraModifier(NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 20, 1000f, FullName);
                    Main.instance.CameraModifiers.Add(modifier);
                }
            }
            if (Timer == 1600)
            {
                Player_Position = Vector2.Zero;
                should_explode = false;
                circle_player = false;
                dashing = false;
                inertia = 5;
                if (Which_Fist == 0)
                {
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                if (Which_Fist == 1)
                {
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                if (Which_Fist == 2)
                {
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
                if (Which_Fist == 3)
                {
                    rand_dist = Main.rand.Next(60, 120);
                    Ice_Shard_Area = rand_dist;
                }
            }
            if(Timer == 1900)
            {
                speed_up = true;
            }
            if(Timer == 2040)
            {
                //ruderibus done layer ice mines
                speed_up = false;
            }
            if (Timer == 2300)
            {
                speed_up = true;
            }
            if (Timer == 2350 || Timer == 2400 || Timer == 2450 || Timer == 2500 || Timer == 2550 || Timer == 2600 || Timer == 2650)
            {
                InkBolt();
            }
            if (Timer == 2700)
            {
                speed_up = false;
            }
            if(Timer == 2701)
            {
                InfernusWorld.Ruderibus_Timer = 0;
            }

            /*
            if(Timer == 2300)
            {
                dashing = true;
            }
            */
            /*
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
            */

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
        private void InkBolt()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                player = Main.player[NPC.target];
                Vector2 velocity = player.Center - NPC.Bottom;
                float magnitude = Magnitude(velocity);
                if (magnitude > 0)
                {
                    velocity *= 15f / magnitude;
                }
                else
                {
                    velocity = new Vector2(0f, 4.9f);
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, velocity, ModContent.ProjectileType<Ice_Bolt>(), 16, NPC.whoAmI);
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
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
                    if (magnitude >= 700f)
                    {
                        magnitude = 700;
                    }
                    NPC.velocity.X = (float)(Math.Cos(rotation) * 12 * (magnitude / 200)) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(rotation) * 12 * (magnitude / 200)) * -1;
                }
            }
        }

        private void Horizontal_Ice()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, new Vector2(10f,0f), ModContent.ProjectileType<Ice_Bolt>(), 15, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, new Vector2(-10f, 0f), ModContent.ProjectileType<Ice_Bolt>(), 15, NPC.whoAmI);
            }
        }
        private void Whoops_Ahh_Pimp_Down_Ahhhh_Pimp_IN_DISTRESS()
        {
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int k = 0; k < 14; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                    Dust wand = Dust.NewDustPerfect(NPC.Center + speed * 22, DustID.HallowedPlants, speed * 2, 0, default, Scale: 2.8f);
                    wand.noGravity = true;
                }
                for (int k = 0; k < 10; k++)
                {
                    Vector2 speed = Main.rand.NextVector2Unit();
                    Dust wand = Dust.NewDustPerfect(NPC.Center + speed * 22, DustID.ApprenticeStorm, speed * 2, 0, default, Scale: 2.8f);
                    wand.noGravity = true;
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
        bool dashed = false;
        int reset_timer;
        float lerp = 0f;
        private void Form_Ice()
        {
            Player player = Main.player[NPC.target];

            float rad = (float)PositionIndex / 4 * MathHelper.TwoPi;

            /*
            if (dashing == true)
            {
                dashed = true;
            }
            if (dashing == false && dashed == true)
            {
                reset_timer++;
            }
            if (reset_timer == 40)
            {
                dashed = false;
                reset_timer = 0;
            }
            if (dashed == false)
            {
                if (speed_up == true)
                {
                    lerp += 0.2f;
                    RotationTimer += 1.6f + lerp;
                }
                else
                {
                    RotationTimer += 0.8f;
                    lerp = 0f;
                }
            }
            */
            
            if (speed_up == true)
            {
                RotationTimer += 2f;
            }
            else
            {
                RotationTimer += 0.8f;
            }
            
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
            mub = toDestinationNormalized;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
        }
    }
}