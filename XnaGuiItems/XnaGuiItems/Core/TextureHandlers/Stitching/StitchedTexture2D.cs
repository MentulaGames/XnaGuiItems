﻿#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Core.TextureHandlers.Stitching
{
#if MONO
    using Mono::Microsoft.Xna.Framework;
    using Mono::Microsoft.Xna.Framework.Graphics;
#else
    using Xna::Microsoft.Xna.Framework;
    using Xna::Microsoft.Xna.Framework.Graphics;
#endif
    using System.Collections.Generic;
    using System;
    using DeJong.Utilities.Logging;

    /// <summary>
    /// A container for multiple <see cref="Texture2D"/> stitched into one <see cref="Texture2D"/>.
    /// </summary>
    /// <remarks>
    /// To use this simple use <see cref="Texture"/> for the texture and a rectangle with a specified id for the source in a <see cref="SpriteBatch.Draw(Texture2D, Rectangle, Rectangle?, Color)"/>.
    /// </remarks>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class StitchedTexture2D : Dictionary<int, Rectangle>, IDisposable
    {
        /// <summary> The texture that encapsulates the underlying <see cref="Texture2D"/>. </summary>
        public Texture2D Texture { get; private set; }

        private RenderTarget2D target;
        private SpriteBatch batch;

        internal void Start(SpriteBatch sb, Size size)
        {
            Log.Verbose(nameof(StitchedTexture2D), $"Started stitching texture of size: {size}");

            batch = sb;
            Texture = new Texture2D(batch.GraphicsDevice, size.Width, size.Height);
            target = new RenderTarget2D(sb.GraphicsDevice, size.Width, size.Height);

            batch.GraphicsDevice.SetRenderTarget(target);
            batch.GraphicsDevice.Clear(Color.Transparent);
            batch.Begin();
        }

        internal void DrawAt(int id, Texture2D texture, Rectangle destination)
        {
            Log.Debug(nameof(StitchedTexture2D), $"Added texture({id}) at {destination}");
            Add(id, destination);
            batch.Draw(texture, destination, Color.White);
        }

        internal void DrawRangeAt(int id, StitchedTexture2D texture, Vector2 pos)
        {
            Log.Debug(nameof(StitchedTexture2D), $"Added texture base({id}) at {pos}");

            Enumerator enumarator = texture.GetEnumerator();
            while (enumarator.MoveNext())
            {
                KeyValuePair<int, Rectangle> cur = enumarator.Current;
                Rect destination = new Rect(cur.Value);
                destination.Position += pos;

                Log.Debug(nameof(StitchedTexture2D), $"Added texture child({id | cur.Key}) at {destination}");
                Add(id | cur.Key, destination.ToXnaRectangle());
                batch.Draw(texture.Texture, destination.ToXnaRectangle(), texture[cur.Key], Color.White);
            }
        }

        internal void End()
        {
            Log.Verbose(nameof(StitchedTexture2D), "Finished stitching");

            batch.End();
            batch.GraphicsDevice.SetRenderTarget(null);
            Texture.SetData(target.GetColorData());
            target.Dispose();
            target = null;
            batch = null;
        }

        /// <summary> Resets the <see cref="StitchedTexture2D"/> and disposes its content. </summary>
        public void Dispose()
        {
            if (Texture != null) Texture.Dispose();
            Texture = null;
            Clear();
        }
    }
}