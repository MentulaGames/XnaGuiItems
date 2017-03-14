#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Core.Handlers
{
#if MONO
    using Mono.Microsoft.Xna.Framework.Graphics;
#else
    using Xna.Microsoft.Xna.Framework.Graphics;
#endif
    using Structs;

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
    }
}