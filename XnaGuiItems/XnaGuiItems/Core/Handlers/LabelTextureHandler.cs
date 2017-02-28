namespace Mentula.GuiItems.Core.Handlers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The class that handles the textures for a <see cref="Items.Label"/>.
    /// </summary>
    public sealed class LabelTextureHandler : TextureHandler
    {
        internal void SetBackFromClr(Color clr, Size size, GraphicsDevice device, BorderStyle style)
        {
            SetBackFromClr(clr, size, device);

            internCall = true;
            if (!userSet[0]) Background = Background.ApplyBorderLabel(style);
            internCall = false;
        }
    }
}