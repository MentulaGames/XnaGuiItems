namespace Mentula.GuiItems.Core.Handlers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The class that handles the textures for a <see cref="Items.Button"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="TextureHandler.userSet"/> flag for hover is 4 and click is 8.
    /// </remarks>
    public sealed class ButtonTextureHandler : TextureHandler
    {
        /// <summary>
        /// The button hover texture for a <see cref="Items.Button"/>.
        /// </summary>
        public Texture2D Hover
        {
            get { return hover; }
            set
            {
                if (!internCall) userSet |= 4;
                hover = value;
            }
        }

        /// <summary>
        /// The button click texture for a <see cref="Items.Button"/>.
        /// </summary>
        public Texture2D Click
        {
            get { return click; }
            set
            {
                if (!internCall) userSet |= 8;
                click = value;
            }
        }

        internal ButtonTextureHandler()
        {
            hover = null;
            click = null;
        }

        private Texture2D hover, click;

        new internal void SetFromClr(Color clr, Size size, GraphicsDevice device)
        {
            base.SetFromClr(clr, size, device);

            internCall = true;
            if ((userSet & 1) == 0) Background.ApplyBorderButton(ButtonStyle.Default);
            if ((userSet & 4) == 0) Hover.ApplyBorderButton(ButtonStyle.Hover);
            if ((userSet & 8) == 0) Click.ApplyBorderButton(ButtonStyle.Click);
            internCall = false;
        }
    }
}