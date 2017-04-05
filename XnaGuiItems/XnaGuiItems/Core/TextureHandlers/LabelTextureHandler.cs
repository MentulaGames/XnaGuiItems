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

    /// <summary>
    /// The class that handles the textures for a <see cref="Items.Label"/>.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class LabelTextureHandler : TextureHandler
    {
        internal virtual void SetBackFromClr(Color clr, Size size, GraphicsDevice device, BorderStyle style)
        {
            SetBackFromClr(clr, size, device);

            internCall = true;
            if (!userset_background) Background = Background.ApplyBorderLabel(style);
            internCall = false;
        }
    }
}