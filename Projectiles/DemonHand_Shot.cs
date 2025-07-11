using Infernus.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class DemonHand_Shot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionShot[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.UnholyTridentFriendly);
            AIType = ProjectileID.UnholyTridentFriendly;

            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Summon;
        }
    }
}