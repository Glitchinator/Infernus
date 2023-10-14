using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.Items.Pets
{
    public class CrabPet : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Penguin);
            AIType = ProjectileID.Penguin;
            Main.projFrames[Projectile.type] = 8;
            Main.projPet[Projectile.type] = true;
            Projectile.width = 80;
            Projectile.height = 28;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            CheckActive(player);
        }
        private void CheckActive(Player player)
        {
            if (!player.dead && player.HasBuff(ModContent.BuffType<CrabBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}
