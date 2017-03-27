﻿#if MONO
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
    using Stitching;

    /// <summary>
    /// A class that handles texture settings.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class TextureHandler : IDisposable
    {
        /// <summary> The <see cref="Texture2D"/> used for drawing. </summary>
        public StitchedTexture2D DrawTexture { get; protected set; }

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
                if (!internCall) userSet[0] = true;
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
                if (!internCall) userSet[1] = true;
                fore = value;
            }
        }

        internal bool internCall;

        /// <summary>
        /// A <see cref="ByteFlags"/> that contains if the user has set specified textures.
        /// 1 if for the background,
        /// 2 if for the foreground.
        /// </summary>
        protected ByteFlags userSet;
        private Texture2D back, fore;

        internal TextureHandler()
        {
            internCall = false;
            userSet = new ByteFlags();
            back = null;
            fore = null;
            DrawTexture = new StitchedTexture2D();
        }

        /// <summary>
        /// Disposes the unmanaged resources.
        /// </summary>
        ~TextureHandler()
        {
            Dispose(false);
        }

        internal virtual void SetBackFromClr(Color clr, Size size, GraphicsDevice device)
        {
            internCall = true;
            if (!userSet[0]) Background = Drawing.FromColor(clr, size, device);
            internCall = false;
        }

        internal virtual void SetForeFromClr(Color clr, Size size, GraphicsDevice device)
        {
            internCall = true;
            if (!userSet[1]) Foreground = Drawing.FromColor(clr, size, device);
            internCall = false;
        }

        internal virtual void SetForeFromClr(Color clr, Size size, Rect destination, GraphicsDevice device)
        {
            internCall = true;
            if (!userSet[1]) Foreground = Drawing.FromColor(clr, size, destination, device);
            internCall = false;
        }

        internal virtual void SetText(string text, SpriteFont font, Color color, Size size, bool multiLine, int lineStart, SpriteBatch sb)
        {
            internCall = true;
            if (!userSet[1]) Foreground = Drawing.FromText(text, font, color, size, multiLine, lineStart, sb);
            internCall = false;
        }

        /// <summary>
        /// Refreshes the <see cref="DrawTexture"/>.
        /// </summary>
        /// <param name="sb"> The spritebatch used for the underlying rendering. </param>
        public virtual void Refresh(SpriteBatch sb)
        {
            DrawTexture.Dispose();
            DrawTexture.Start(sb, new Size(Math.Max(Background.Width, Foreground.Width), Background.Height + Foreground.Height));
            DrawTexture.DrawAt(0, Background, Background.Bounds);
            DrawTexture.DrawAt(1, Foreground, new Rectangle(0, Background.Height, Foreground.Width, Foreground.Height));
            DrawTexture.End();
        }

        internal bool BackgroundSet() => userSet[0];

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