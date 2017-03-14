#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Core.Handlers
{
#if MONO
    using Mono.Microsoft.Xna.Framework;
    using Mono.Microsoft.Xna.Framework.Graphics;
#else
    using Xna.Microsoft.Xna.Framework;
    using Xna.Microsoft.Xna.Framework.Graphics;
#endif
    using Structs;

    /// <summary>
    /// The class that handles the textures for a <see cref="Items.Button"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="TextureHandler.userSet"/> flag for hover is 4 and click is 8.
    /// </remarks>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ButtonTextureHandler : LabelTextureHandler
    {
        /// <summary>
        /// The texture that needs to be drawn in the current state.
        /// </summary>
        public Texture2D DrawTexture
        {
            get
            {
                return state == ButtonStyle.Default ? Background : (state == ButtonStyle.Hover ? Hover : Click);
            }
        }

        internal ButtonStyle state;

        /// <summary>
        /// The button hover texture for a <see cref="Items.Button"/>.
        /// </summary>
        public Texture2D Hover
        {
            get { return hover; }
            set
            {
                if (!internCall) userSet[2] = true;
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
                if (!internCall) userSet[3] = true;
                click = value;
            }
        }

        internal ButtonTextureHandler()
        {
            hover = null;
            click = null;
        }

        private Texture2D hover, click;

        internal override void SetBackFromClr(Color clr, Size size, GraphicsDevice device)
        {
            base.SetBackFromClr(clr, size, device);

            internCall = true;
            if (!userSet[2]) Hover = Drawing.FromColor(clr, size, device);
            if (!userSet[3]) Click = Drawing.FromColor(clr, size, device);
            internCall = false;
        }

        internal virtual void ApplyBorders()
        {
            internCall = true;
            if (!userSet[0]) Background = Background.ApplyBorderButton(ButtonStyle.Default, true);
            if (!userSet[2]) Hover = Hover.ApplyBorderButton(ButtonStyle.Hover, true);
            if (!userSet[3]) Click = Click.ApplyBorderButton(ButtonStyle.Click, true);
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