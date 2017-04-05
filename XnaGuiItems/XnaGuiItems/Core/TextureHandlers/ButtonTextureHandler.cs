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
    /// The class that handles the textures for a <see cref="Items.Button"/>.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ButtonTextureHandler : LabelTextureHandler
    {
        /// <summary>
        /// The texture that needs to be drawn in the current state.
        /// </summary>
        public int DrawId
        {
            get
            {
                return state == ButtonStyle.Default ? 0 : (state == ButtonStyle.Hover ? 2 : 3);
            }
        }

        /// <summary>
        /// The button hover texture for a <see cref="Items.Button"/>.
        /// </summary>
        public Texture2D Hover
        {
            get { return hover; }
            set
            {
                if (!internCall) userset_hover = true;
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
                if (!internCall) userset_click = true;
                click = value;
            }
        }

        internal ButtonStyle state;

        /// <summary>
        /// Gets or sets a value indicating if the user or XnaGuiItems has set the hover texture.
        /// </summary>
        protected bool userset_hover;
        /// <summary>
        /// Gets or sets a value indicating if the user or XnaGuiItems has set the click texture.
        /// </summary>
        protected bool userset_click;

        private Texture2D hover, click;

        internal ButtonTextureHandler()
        {
            hover = null;
            click = null;
        }

        internal override void SetBackFromClr(Color clr, Size size, GraphicsDevice device)
        {
            base.SetBackFromClr(clr, size, device);

            internCall = true;
            if (!userset_hover) Hover = Drawing.FromColor(clr, size, device);
            if (!userset_click) Click = Drawing.FromColor(clr, size, device);
            internCall = false;
        }

        internal virtual void ApplyBorders()
        {
            internCall = true;
            if (!userset_background) Background = Background.ApplyBorderButton(ButtonStyle.Default, true);
            if (!userset_hover) Hover = Hover.ApplyBorderButton(ButtonStyle.Hover, true);
            if (!userset_click) Click = Click.ApplyBorderButton(ButtonStyle.Click, true);
            internCall = false;
        }

        /// <summary>
        /// Refreshes the <see cref="TextureHandler.DrawTexture"/>.
        /// </summary>
        /// <param name="sb"> The spritebatch used for the underlying rendering. </param>
        public override void Refresh(SpriteBatch sb)
        {
            ApplyBorders();

            DrawTexture.Dispose();
            DrawTexture.Start(sb, new Size(Math.Max(Background.Width, Foreground.Width), Background.Height + Foreground.Height + Hover.Height + Click.Height));
            DrawTexture.DrawAt(0, Background, Background.Bounds);
            DrawTexture.DrawAt(1, Foreground, new Rectangle(0, Background.Height, Foreground.Width, Foreground.Height));
            DrawTexture.DrawAt(2, Hover, new Rectangle(0, Background.Height + Foreground.Height, Hover.Width, Hover.Height));
            DrawTexture.DrawAt(3, Click, new Rectangle(0, Background.Height + Foreground.Height + Hover.Height, Click.Width, Click.Height));
            DrawTexture.End();
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