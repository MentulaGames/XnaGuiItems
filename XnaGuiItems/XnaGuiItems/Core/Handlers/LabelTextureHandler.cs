namespace Mentula.GuiItems.Core.Handlers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public sealed class LabelTextureHandler : TextureHandler
    {
        internal void SetBackFromClr(Color clr, Size size, GraphicsDevice device, BorderStyle style)
        {
            SetBackFromClr(clr, size, device);
            if ((userSet & 1) == 0) Background = Background.ApplyBorderLabel(style);
        }
    }
}