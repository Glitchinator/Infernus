using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Mounts
{
    public class ConstructionMount : ModMount
    {
        public override void SetStaticDefaults()
        {
            MountData.jumpHeight = 5;
            MountData.acceleration = 0.19f;
            MountData.jumpSpeed = 1f;
            MountData.blockExtraJumps = false;
            MountData.constantJump = false;
            MountData.heightBoost = 20;
            MountData.fallDamage = 0f;
            MountData.runSpeed = 8f;
            MountData.dashSpeed = 6f;
            MountData.totalFrames = 4;
            MountData.playerYOffsets = Enumerable.Repeat(20, MountData.totalFrames).ToArray();
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

            MountData.textureWidth = MountData.backTexture.Width() + 20;
            MountData.textureHeight = MountData.backTexture.Height();
        }
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
                Dust.NewDustPerfect(player.Center, DustID.Dirt);
            }

            skipDust = true;
        }
    }
}
