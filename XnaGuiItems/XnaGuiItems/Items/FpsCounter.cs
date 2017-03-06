#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Items
{
#if MONO
    using Mono.Microsoft.Xna.Framework;
    using Mono.Microsoft.Xna.Framework.Graphics;
    using Mono.Microsoft.Xna.Framework.Input;
#else
    using Xna.Microsoft.Xna.Framework;
    using Xna.Microsoft.Xna.Framework.Graphics;
    using Xna.Microsoft.Xna.Framework.Input;
#endif
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Core.Interfaces;

    /// <summary>
    /// A default fps counter label for debugging.
    /// </summary>
    public sealed class FpsCounter : Label, IDeltaUpdate
    {
        /// <summary> The size of the frame buffer. </summary>
        public uint BufferSize { get; set; }
        /// <summary> The current fps. </summary>
        public float Current { get; private set; }
        /// <summary> The average fps of the buffer. </summary>
        public float Average { get; private set; }

        private Queue<float> buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FpsCounter"/> class.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public FpsCounter(ref SpriteBatch sb, SpriteFont font)
            : base(ref sb, font)
        {
            AutoSize = true;
            BackColor = Color.Transparent;
            buffer = new Queue<float>(new float[] { 0 });
        }

        /// <summary>
        /// Updates the <see cref="FpsCounter"/>, checking if any mouse events are occuring.
        /// This should not be used.
        /// </summary>
        /// <param name="mState"> The current state of the <see cref="Mouse"/>. </param>
        [Obsolete("Use Update(MouseState, float) instead!", true)]
        public override void Update(MouseState mState) { base.Update(mState); }

        /// <summary>
        /// Updates the <see cref="FpsCounter"/>, checking if any mouse events are occuring and updates the fram buffer.
        /// </summary>
        /// <param name="mState"> The current state of the <see cref="Mouse"/>. </param>
        /// <param name="deltaTime"> The specified deltatime. </param>
        public void Update(MouseState mState, float deltaTime)
        {
            if (deltaTime > 0)
            {
                buffer.Enqueue(Current = 1 / deltaTime);
                if (buffer.Count > BufferSize) buffer.Dequeue();
                Average = buffer.Average();
            }

            Text = $"Fps: {Average}";
            base.Update(mState);
        }
    }
}