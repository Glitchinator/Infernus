using Infernus.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Infernus.Level
{
    internal class Level_UI : UIState
    {
        private UIText text;
        private UIElement area;
        private UIImage barFrame;
        private UIImage buff;
        private UIImage buff2;
        private Color gradientA;
        private Color gradientB;
        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 800, 1f);
            area.Top.Set(30, 0f);
            area.Width.Set(182, 0f);
            area.Height.Set(60, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("Infernus/Level/Level_Image"));
            barFrame.Left.Set(22, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(136, 0f);
            barFrame.Height.Set(30, 0f);

            buff = new UIImage(ModContent.Request<Texture2D>("Infernus/Level/Stress_Buff_1"));
            buff.Left.Set(16, 0f);
            buff.Top.Set(10, 0f);
            buff.Width.Set(10, 0f);
            buff.Height.Set(10, 0f);

            buff2 = new UIImage(ModContent.Request<Texture2D>("Infernus/Level/Stress_Buff_2"));
            buff2.Left.Set(154, 0f);
            buff2.Top.Set(10, 0f);
            buff2.Width.Set(10, 0f);
            buff2.Height.Set(10, 0f);

            text = new UIText("", 0.8f);
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(40, 0f);
            text.Left.Set(18, 0f);

            gradientA = new Color(48, 9, 9);
            gradientB = new Color(193, 92, 24);

            area.Append(text);
            area.Append(barFrame);
            area.Append(buff);
            area.Append(buff2);
            Append(area);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (InfernusSystem.Level_systemON == false)
            {
                return;
            }
            base.Draw(spriteBatch);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {

            var modPlayer = Main.LocalPlayer.GetModPlayer<InfernusPlayer>();
            float quotient = (float)modPlayer.Stress_Current / modPlayer.Stress_Max2;
            quotient = Utils.Clamp(quotient, 0f, 1f);
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 9;
            hitbox.Width -= 18;
            hitbox.Y += 5;
            hitbox.Height -= 10;

            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                float percent = (float)i / (right - left);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
            }
            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (InfernusSystem.Level_systemON == false)
            {
                return;
            }

            var modPlayer = Main.LocalPlayer.GetModPlayer<InfernusPlayer>();

            if (ModContent.GetInstance<InfernusConfig>().Enable_StressUI_Text == true)
            {
                text.SetText($"Stress: {modPlayer.Stress_Current} / {modPlayer.Stress_Max2}");
            }
            else
            {
                text.SetText($"");
            }


            if (modPlayer.Stress_Buff_1 == true)
            {
                buff.Color = Color.White;
            }
            else
            {
                buff.Color = Color.Transparent;
            }
            if (modPlayer.Stress_Buff_2 == true)
            {
                buff2.Color = Color.White;
            }
            else
            {
                buff2.Color = Color.Transparent;
            }
            base.Update(gameTime);
        }
    }
}
