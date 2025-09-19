using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using ReLogic.Content;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Policy;
namespace Infernus.Projectiles
{

    public class Ancient_Fishing_Spear_Proj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.timeLeft = 240;

            DrawOffsetX = -10;
            DrawOriginOffsetY = -24;
            //DrawOriginOffsetX = 31;
        }
        bool retracting = false;
        bool retracted = false;
        int Speed = 18;
        int rand = 0;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 20f)
            {
                Projectile.ai[0] = 20f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.6f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            Player player = Main.player[Projectile.owner];

            if (Main.myPlayer == Projectile.owner)
            {
                var inertia = 12f;
                Vector2 direction = player.Center - Projectile.Center;
                float dist_check = Magnitude(direction);

                if (player.dead || !player.active)
                {
                    return;
                }
                if (retracted == true)
                {
                    direction.Normalize();
                    direction *= Speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                if (dist_check <= 40f)
                {
                    if (retracted == true)
                    {
                        if (player.ZoneJungle == true && player.ZoneOverworldHeight == true)
                        {
                            int rand = Main.rand.Next(3);
                            if (rand == 0)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.DoubleCod, 1, false);
                            }
                            else
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Bass, 1, false);
                            }
                        }
                        else if (player.ZoneJungle == true && (player.ZoneDirtLayerHeight == true || player.ZoneRockLayerHeight == true))
                        {
                            int rand = Main.rand.Next(3);
                            if (rand == 0)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.VariegatedLardfish, 1, false);
                            }
                            else
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Bass, 1, false);
                            }
                        }
                        else if (player.ZoneCorrupt == true)
                        {
                            int rand = Main.rand.Next(3);
                            if (rand == 0)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Ebonkoi, 1, false);
                            }
                            else
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Bass, 1, false);
                            }

                        }
                        else if (player.ZoneCrimson == true)
                        {
                            int rand = Main.rand.Next(4);
                            if (rand == 0)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.CrimsonTigerfish, 1, false);
                            }
                            else if (rand == 1)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Hemopiranha, 1, false);
                            }
                            else
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Bass, 1, false);
                            }

                        }
                        else if (player.ZoneHallow == true && (player.ZoneDirtLayerHeight == true || player.ZoneRockLayerHeight == true))
                        {
                            int rand = Main.rand.Next(4);
                            if (rand == 0)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.ChaosFish, 1, false);
                            }
                            else if (rand == 1)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Prismite, 1, false);
                            }
                            else
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Bass, 1, false);
                            }

                        }
                        else if (player.ZoneDirtLayerHeight == true || player.ZoneRockLayerHeight == true)
                        {
                            int rand = Main.rand.Next(4);
                            if (rand == 0)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.ArmoredCavefish, 1, false);
                            }
                            else if (rand == 1)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.SpecularFish, 1, false);
                            }
                            else
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Bass, 1, false);
                            }

                        }
                        else if (player.ZoneSkyHeight == true)
                        {
                            int rand = Main.rand.Next(3);
                            if (rand == 0)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Damselfish, 1, false);
                            }
                            else
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Bass, 1, false);
                            }

                        }
                        else if (player.ZoneSnow == true)
                        {
                            int rand = Main.rand.Next(3);
                            if (rand == 0)
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.FrostMinnow, 1, false);
                            }
                            else
                            {
                                Item.NewItem(Projectile.GetSource_NaturalSpawn(), new Vector2(Projectile.position.X, Projectile.position.Y), ItemID.Bass, 1, false);
                            }
                        }
                        else
                        {
                            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), InfernusPlayer.GainXP_Resource, "There are no vanilla potion fish to be caught in this biome, try a different biome", true);
                        }

                        Projectile.Kill();
                    }
                }
            }

            if (Projectile.wet == true)
            {
                Projectile.tileCollide = false;
                retracted = true;
            }
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Rope);
            }
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 playerArmPosition = Main.GetPlayerArmPosition(Projectile);

            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("Infernus/Projectiles/Light_Chain");

            Rectangle? chainSourceRectangle = null;
            float chainHeightAdjustment = 0f;

            Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
            Vector2 chainDrawPosition = Projectile.Center;
            Vector2 vectorFromProjectileToPlayerArms = playerArmPosition.MoveTowards(chainDrawPosition, 4f) - chainDrawPosition;
            Vector2 unitVectorFromProjectileToPlayerArms = vectorFromProjectileToPlayerArms.SafeNormalize(Vector2.Zero);
            float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height()) + chainHeightAdjustment;
            if (chainSegmentLength == 0)
            {
                chainSegmentLength = 10;
            }
            float chainRotation = unitVectorFromProjectileToPlayerArms.ToRotation() + MathHelper.PiOver2;
            int chainCount = 0;
            float chainLengthRemainingToDraw = vectorFromProjectileToPlayerArms.Length() + chainSegmentLength / 2f;

            while (chainLengthRemainingToDraw > 0f)
            {
                var chainTextureToDraw = chainTexture;
                Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, lightColor, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);
                chainDrawPosition += unitVectorFromProjectileToPlayerArms * chainSegmentLength;
                chainCount++;
                chainLengthRemainingToDraw -= chainSegmentLength;
            }
            return true;
        }
    }
}