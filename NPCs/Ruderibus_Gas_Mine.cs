using Infernus.Projectiles;
using Infernus.Projectiles.Temporal_Glow_Squid.Boss;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Infernus.NPCs
{
    public class Ruderibus_Gas_Mine : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }
        int timer;
        int explode_timer;
        public override void SetDefaults()
        {
            NPC.lifeMax = 320;
            NPC.damage = 16;
            NPC.defense = 6;
            NPC.knockBackResist = 0.0f;
            NPC.width = 62;
            NPC.height = 62;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit42;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.value = 0;
        }
        public override void AI()
        {
            timer++;
            explode_timer++;
            if (timer == 30)
            {
                Gas_Med();
            }
            if (timer == 100)
            {
                Gas_Large();
                timer = 0;
            }
            if(explode_timer == 1900)
            {
                NPC.life = 0;
                SoundEngine.PlaySound(SoundID.NPCDeath44, NPC.position);
                if (Main.netMode == NetmodeID.Server)
                {
                    return;
                }
                for (int k = 0; k < 16; k++)
                    {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ApprenticeStorm, 4f, -2.5f, 0, default, 1f);
                }
                for (int k = 0; k < 16; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.HallowedPlants, 4f, -2.5f, 0, default, 1f);
                }
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 16; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ApprenticeStorm, 4f, -2.5f, 0, default, 1f);
                }
                for (int k = 0; k < 16; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.HallowedPlants, 4f, -2.5f, 0, default, 1f);
                }
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            NPC.lifeMax = 320;
            NPC.life = 320;
        }
        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 3;

            int frameSpeed = 6;
            NPC.frameCounter += 0.7f;
            NPC.frameCounter += NPC.velocity.Length() / 13f;
            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }
        private void Gas_Med()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0,0), ModContent.ProjectileType<Ice_Gas_Med>(), 16, NPC.whoAmI);
            }
        }
        private void Gas_Large()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, 0), ModContent.ProjectileType<Ice_Gas_Large>(), 16, NPC.whoAmI);
            }
        }
    }
}