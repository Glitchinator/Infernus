using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Projectiles
{

    public class Ruderibus_Round_exlos : ModProjectile
    {
        public override string Texture => "Infernus/Items/Weapon/Melee/Hatchet";
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 1;
            Projectile.extraUpdates = 1;
            Projectile.alpha = 255;
            Projectile.damage = 22;
            Projectile.knockBack = 4f;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 20;
        }
        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);
            for (int k = 0; k < 14; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Circular(2f, 4f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed2 * 8, DustID.HallowedPlants, speed2 * 3, Scale: 2f);
                Sword.noGravity = true;
            }
            for (int k = 0; k < 12; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Circular(2f, 4f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed2 *8, DustID.HallowedPlants, speed2 * 3, Scale: 2f);
                Sword.noGravity = true;
            }
            for (int k = 0; k < 6; k++)
            {
                Vector2 speed2 = Main.rand.NextVector2Circular(2f, 4f);
                Dust Sword = Dust.NewDustPerfect(Projectile.Center + speed2 * 8, DustID.Smoke, speed2 * 3, Scale: 2f);
                Sword.noGravity = true;
            }
            float speedMulti = 0.8f;

            var smokeGore = Gore.NewGoreDirect(Projectile.GetSource_NaturalSpawn(), Projectile.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
            smokeGore.velocity *= speedMulti;
            smokeGore.velocity += Vector2.One;
            smokeGore = Gore.NewGoreDirect(Projectile.GetSource_NaturalSpawn(), Projectile.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
            smokeGore.velocity *= speedMulti;
            smokeGore.velocity.X -= 1.2f;
            smokeGore.velocity.Y += 1.2f;
            smokeGore = Gore.NewGoreDirect(Projectile.GetSource_NaturalSpawn(), Projectile.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
            smokeGore.velocity *= speedMulti;
            smokeGore.velocity.X += 1.2f;
            smokeGore.velocity.Y -= 1.2f;
            smokeGore = Gore.NewGoreDirect(Projectile.GetSource_NaturalSpawn(), Projectile.Center, default, Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1));
            smokeGore.velocity *= speedMulti;
            smokeGore.velocity -= Vector2.One;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.75f);
        }
    }
}