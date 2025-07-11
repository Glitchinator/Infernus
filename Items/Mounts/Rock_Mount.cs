using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Mounts
{
    public class Rock_Mount : ModMount
    {
        public override void SetStaticDefaults()
        {
            MountData.jumpHeight = 20;
            MountData.acceleration = 0.16f;
            MountData.jumpSpeed = 7f;
            MountData.blockExtraJumps = false;
            MountData.constantJump = false;
            MountData.heightBoost = 24;
            MountData.fallDamage = 0f;
            MountData.runSpeed = 4f;
            MountData.dashSpeed = 8f;
            MountData.totalFrames = 3;
            MountData.playerYOffsets = Enumerable.Repeat(24, MountData.totalFrames).ToArray();
            MountData.xOffset = 0;
            MountData.yOffset = 18;
            MountData.playerHeadOffset = 12;
            MountData.bodyFrame = 3;
            MountData.standingFrameCount = 1;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;
            MountData.runningFrameCount = 3;
            MountData.runningFrameDelay = 12;
            MountData.inAirFrameCount = 1;
            MountData.inAirFrameDelay = 12;
            MountData.idleFrameCount = 1;
            MountData.idleFrameDelay = 12;
            MountData.idleFrameLoop = false;
            MountData.swimFrameCount = MountData.inAirFrameCount;
            MountData.swimFrameDelay = MountData.inAirFrameDelay;
            MountData.swimFrameStart = MountData.inAirFrameStart;

            MountData.textureWidth = MountData.backTexture.Width();
            MountData.textureHeight = MountData.backTexture.Height();
        }
        public override void UpdateEffects(Player player)
        {
            if (Math.Abs(player.velocity.X) > 7f)
            {
                Rectangle rect = player.getRect();

                Dust.NewDust(new Vector2(rect.X, rect.Bottom), 1, 1, DustID.Smoke);
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
