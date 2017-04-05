#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Core.TextureHandlers
{
#if MONO
    using Mono::Microsoft.Xna.Framework;
    using Mono::Microsoft.Xna.Framework.Graphics;
#else
    using Xna::Microsoft.Xna.Framework;
    using Xna::Microsoft.Xna.Framework.Graphics;
#endif
    using System;

    /// <summary>
    /// The class that handles the textures for a <see cref="Items.DropDown"/>.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class DropDownTextureHandler : TextureHandler
    {
        /// <summary>
        /// The textures for the underlying buttons.
        /// </summary>
        public DropDownButtonTextureHandler[] Buttons { get; set; }

        internal DropDownTextureHandler()
        {
            Buttons = new DropDownButtonTextureHandler[0];
        }

        internal void SetBackFromClr(Color back, Color header, Size size, Size headerSize, SpriteBatch sb, BorderStyle style)
        {
            internCall = true;
            if (!userset_background)
            {
                Rectangle backRect = new Rectangle(0, headerSize.Height, size.Width, size.Height);
                Rectangle headerRect = new Rectangle(0, 0, headerSize.Width, headerSize.Height);
                Background = Drawing.FromMultiColor(back, header, backRect, headerRect, sb.GraphicsDevice).ApplyBorderLabel(style);
            }
            internCall = false;
        }

        internal void SetButtons(Pair[][] labels, Size size, SpriteFont font, SpriteBatch sb)
        {
            Buttons = new DropDownButtonTextureHandler[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                Buttons[i] = new DropDownButtonTextureHandler();
                Buttons[i].SetBackFromLabels(labels[i], size, font, sb);
                Buttons[i].ApplyBorders();
            }
        }

        /// <summary>
        /// Refreshes the <see cref="TextureHandler.DrawTexture"/>.
        /// </summary>
        /// <param name="sb"> The spritebatch used for the underlying rendering. </param>
        public override void Refresh(SpriteBatch sb)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].Refresh(sb);
            }

            DrawTexture.Dispose();
            DrawTexture.Start(sb, new Size(Math.Max(Background.Width, Foreground.Width), Background.Height * (Buttons.Length + 1) + Foreground.Height));
            DrawTexture.DrawAt(0, Background, Background.Bounds);
            DrawTexture.DrawAt(1, Foreground, new Rectangle(0, Background.Height, Foreground.Width, Foreground.Height));

            Vector2 pos = new Vector2(0, Background.Height + Foreground.Height);
            for (int i = 0; i < Buttons.Length; i++)
            {
                DrawTexture.DrawRangeAt(8 << i, Buttons[i].DrawTexture, pos);
                pos.Y += Buttons[i].DrawTexture.Texture.Height;
            }
            DrawTexture.End();
        }
    }
}