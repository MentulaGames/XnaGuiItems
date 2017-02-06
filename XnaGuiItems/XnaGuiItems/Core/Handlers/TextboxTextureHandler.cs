﻿namespace Mentula.GuiItems.Core.Handlers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The class that handles the textures of a <see cref="Items.TextBox"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="TextureHandler.userSet"/> flag for the focused texture is 4.
    /// </remarks>
    public sealed class TextboxTextureHandler : TextureHandler
    {
        /// <summary>
        /// The textbox focused texture for a <see cref="Items.TextBox"/>. 
        /// </summary>
        public Texture2D Focused
        {
            get { return focus; }
            set
            {
                if (!internCall) userSet |= 4;
                focus = value;
            }
        }

        private Texture2D focus;

        internal TextboxTextureHandler()
        {
            focus = null;
        }

        internal void Swap()
        {
            internCall = true;
            Foreground = Focused;
            internCall = false;
        }

        internal void SetBack(Texture2D saved)
        {
            internCall = true;
            Foreground = saved;
            internCall = false;
        }

        internal void SetFocusText(string text, SpriteFont font, Color color, Size size, bool multiLine, int lineStart, GraphicsDevice device)
        {
            SetText(text, font, color, size, multiLine, lineStart, device);

            internCall = true;
            if ((userSet & 4) == 0) Focused = Drawing.FromText(text + '|', font, color, size, multiLine, lineStart, device);
            internCall = false;
        }

        internal void SetBackFromClr(Color clr, Size size, GraphicsDevice device, BorderStyle style)
        {
            SetBackFromClr(clr, size, device);
            internCall = true;
            if ((userSet & 1) == 0) Background = Background.ApplyBorderLabel(style);
            internCall = false;
        }

        /// <summary>
        /// Releases the unmanaged and managed resources used by the <see cref="TextboxTextureHandler"/>.
        /// </summary>
        /// <param name="disposing"> Whether the managed resources should be disposed. </param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                if (focus != null) focus.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}