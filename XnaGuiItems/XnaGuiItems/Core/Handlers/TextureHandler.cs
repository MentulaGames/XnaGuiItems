namespace Mentula.GuiItems.Core.Handlers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    /// <summary>
    /// A class that handles texture settings.
    /// </summary>
    public class TextureHandler : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the <see cref="TextureHandler"/> has been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }
        /// <summary>
        /// The background texture for a <see cref="GuiItem"/>.
        /// </summary>
        public Texture2D Background
        {
            get { return back; }
            set
            {
                if (!internCall) userSet |= 1;
                back = value;
            }
        }

        /// <summary>
        /// The foreground texture for a <see cref="GuiItem"/>.
        /// </summary>
        public Texture2D Foreground
        {
            get { return fore; }
            set
            {
                if (!internCall) userSet |= 2;
                fore = value;
            }
        }

        internal bool internCall;

        /// <summary>
        /// A bitflag byte that contains if the user has set specified textures.
        /// 1 if for the background,
        /// 2 if for the foreground.
        /// </summary>
        protected byte userSet;
        private Texture2D back, fore;

        internal TextureHandler()
        {
            internCall = false;
            userSet = 0;
            back = null;
            fore = null;
        }

        /// <summary>
        /// Disposes the unmanaged resources.
        /// </summary>
        ~TextureHandler()
        {
            Dispose(false);
        }

        internal void SetBackFromClr(Color clr, Size size, GraphicsDevice device)
        {
            internCall = true;
            if ((userSet & 1) == 0) Background = Drawing.FromColor(clr, size, device);
            internCall = false;
        }

        internal void SetForeFromClr(Color clr, Size size, GraphicsDevice device)
        {
            internCall = true;
            if ((userSet & 2) == 0) Foreground = Drawing.FromColor(clr, size, device);
            internCall = false;
        }

        internal void SetText(string text, SpriteFont font, Color color, Size size, bool multiLine, int lineStart, GraphicsDevice device)
        {
            internCall = true;
            if ((userSet & 2) == 0) Foreground = Drawing.FromText(text, font, color, size, multiLine, lineStart, device);
            internCall = false;
        }

        internal bool BackgroundSet() { return (userSet & 1) != 0; }
        internal bool ForegroundSet() { return (userSet & 2) != 0; }

        /// <summary>
        /// Releases the managed and unmanaged resources used by the <see cref="GuiItem"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged and managed resources used by the <see cref="TextureHandler"/>.
        /// </summary>
        /// <param name="disposing"> Whether the managed resources should be disposed. </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed & disposing)
            {
                if (back != null) back.Dispose();
                if (fore != null) fore.Dispose();
                IsDisposed = true;
            }
        }
    }
}