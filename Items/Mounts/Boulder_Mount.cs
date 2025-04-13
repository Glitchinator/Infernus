using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items
{
    public class Boulder_Mount : ModMount
    {
        public override void SetStaticDefaults()
        {
            MountData.jumpHeight = 18;
            MountData.acceleration = 0.15f;
            MountData.jumpSpeed = 6f;
            MountData.blockExtraJumps = false;
            MountData.constantJump = true;
            MountData.heightBoost = 20;
            MountData.fallDamage = 0.5f;
            MountData.runSpeed = 7f;
            MountData.dashSpeed = 5f;

            MountData.totalFrames = 4;
            MountData.playerYOffsets = Enumerable.Repeat(1, MountData.totalFrames).ToArray();
            MountData.xOffset = 13;
            MountData.yOffset = -12;
            MountData.playerHeadOffset = 22;
            MountData.bodyFrame = 3;
            MountData.standingFrameCount = 4;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;
            MountData.runningFrameCount = 4;
            MountData.runningFrameDelay = 12;
            MountData.inAirFrameCount = 1;
            MountData.inAirFrameDelay = 12;
            MountData.idleFrameCount = 4;
            MountData.idleFrameDelay = 12;
            MountData.idleFrameLoop = true;
            MountData.swimFrameCount = MountData.inAirFrameCount;
            MountData.swimFrameDelay = MountData.inAirFrameDelay;
            MountData.swimFrameStart = MountData.inAirFrameStart;

            MountData.textureWidth = MountData.backTexture.Width();
            MountData.textureHeight = MountData.backTexture.Height();
        }
        /*
        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            // Draw is called for each mount texture we provide, so we check drawType to avoid duplicate draws.
            if (drawType == 0)
            {

                rotation += (float)drawPlayer.direction * 4;
                playerDrawData.Add(new DrawData(texture, drawPosition, frame, drawColor, rotation, drawOrigin, drawScale, spriteEffects ^ SpriteEffects.FlipHorizontally, 0));
            }

            // by returning true, the regular drawing will still happen.
            return true;
        }
        */
        public override void UpdateEffects(Player player)
        {
            if (Math.Abs(player.velocity.X) > 4f)
            {
                Rectangle rect = player.getRect();

                Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, DustID.Smoke);
            }
        }
        public override void SetMount(Player player, ref bool skipDust)
        {
            for (int i = 0; i < 16; i++)
            {
                Dust.NewDustPerfect(player.Center, DustID.Stone);
            }

            skipDust = true;
        }
    }
}
