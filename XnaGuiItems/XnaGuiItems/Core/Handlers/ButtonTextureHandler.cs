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

        new internal void SetBackFromClr(Color clr, Size size, GraphicsDevice device)
        {
            base.SetBackFromClr(clr, size, device);
            internCall = true;
            if ((userSet & 4) == 0) Hover = Drawing.FromColor(clr, size, device);
            if ((userSet & 8) == 0) Click = Drawing.FromColor(clr, size, device);
            internCall = false;
        }

        internal void ApplyBorders()
        {
            internCall = true;
            if ((userSet & 1) == 0) Background = Background.ApplyBorderButton(ButtonStyle.Default);
            if ((userSet & 4) == 0) Hover = Hover.ApplyBorderButton(ButtonStyle.Hover);
            if ((userSet & 8) == 0) Click = Click.ApplyBorderButton(ButtonStyle.Click);
            internCall = false;
        }

        /// <summary>
        /// Releases the unmanaged and managed resources used by the <see cref="ButtonTextureHandler"/>.
        /// </summary>
        /// <param name="disposing"> Whether the managed resources should be disposed. </param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                if (hover != null) hover.Dispose();
                if (click != null) click.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}