using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Gladius_Exlos : ModProjectile
    {
        public override string Texture => "Infernus/Items/Weapon/Melee/Hatchet";
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 90;
            Projectile.height = 90;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 1;
            Projectile.extraUpdates = 1;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 32;
        }
        public override void OnSpawn(IEntitySource source)
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.TerraBlade,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(Projectile.Hitbox) },
                Projectile.owner);
            for (int k = 0; k < 6; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Circular(1f, 2f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed2 * 32, DustID.TerraBlade, speed2 * 3, Scale: 2f);
                Sword.noGravity = true;
            }
            for (int k = 0; k < 7; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Circular(1f, 2f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed2 * 32, DustID.PoisonStaff, speed2 * 3, Scale: 2f);
                Sword.noGravity = true;
            }
        }
    }
}