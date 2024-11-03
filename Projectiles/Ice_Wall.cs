using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace Infernus.Projectiles
{

    public class Ice_Wall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 560;
            DrawOffsetX = -3;
            DrawOriginOffsetY = -3;
        }
        public override void AI()
        {
            if (Projectile.timeLeft <= 220)
            {
                Projectile.hostile = false;
                Projectile.alpha += 1;
            }
        }
    }
}