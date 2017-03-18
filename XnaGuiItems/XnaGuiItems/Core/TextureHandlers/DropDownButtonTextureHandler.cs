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
    using Structs;
    using System;

    /// <summary>
    /// The class that handles the textures for a <see cref="Items.DropDown"/> buttons.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class DropDownButtonTextureHandler : ButtonTextureHandler
    {
        internal DropDownButtonTextureHandler()
        {
            state = ButtonStyle.Default;
        }

        internal void SetBackFromLabels(Pair[] labels, Size size, SpriteFont font, SpriteBatch sb)
        {
            internCall = true;
            if (!userSet[0]) Background = Drawing.FromLabels(labels, font, size, sb);
            if (!userSet[2]) Hover = Drawing.FromLabels(labels, font, size, sb);
            if (!userSet[3]) Click = Drawing.FromLabels(labels, font, size, sb);
            internCall = false;
        }

        internal override void ApplyBorders()
        {
            internCall = true;
            if (!userSet[2]) Hover = Hover.ApplyBorderButton(ButtonStyle.Hover, false);
            if (!userSet[3]) Click = Click.ApplyBorderButton(ButtonStyle.Click, false);
            internCall = false;
        }

        /// <summary>
        /// Refreshes the <see cref="TextureHandler.DrawTexture"/>.
        /// </summary>
        /// <param name="sb"> The spritebatch used for the underlying rendering. </param>
        public override void Refresh(SpriteBatch sb)
        {
            DrawTexture.Dispose();
            DrawTexture.Start(sb, new Size(Background.Width, Background.Height + Hover.Height + Click.Height));
            DrawTexture.DrawAt(0, Background, Background.Bounds);
            DrawTexture.DrawAt(2, Hover, new Rectangle(0, Background.Height, Hover.Width, Hover.Height));
            DrawTexture.DrawAt(3, Click, new Rectangle(0, Background.Height + Hover.Height, Click.Width, Click.Height));
            DrawTexture.End();
        }
    }
}