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
    using Structs;
    using System;

    /// <summary>
    /// The class that handles the textures of a <see cref="Items.TextBox"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="TextureHandler.userSet"/> flag for the focused texture is 4.
    /// </remarks>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class TextboxTextureHandler : LabelTextureHandler
    {
        /// <summary>
        /// The textbox focused texture for a <see cref="Items.TextBox"/>. 
        /// </summary>
        public Texture2D Focused
        {
            get { return focus; }
            set
            {
                if (!internCall) userSet[2] = true;
                focus = value;
            }
        }

        private Texture2D focus;

        internal TextboxTextureHandler()
        {
            focus = null;
        }

        internal void SetFocusText(string text, SpriteFont font, Color color, Size size, bool multiLine, int lineStart, SpriteBatch sb)
        {
            SetText(text, font, color, size, multiLine, lineStart, sb);

            internCall = true;
            if (!userSet[2]) Focused = Drawing.FromText(text + '|', font, color, size, multiLine, lineStart, sb);
            internCall = false;
        }

        /// <summary>
        /// Refreshes the <see cref="TextureHandler.DrawTexture"/>.
        /// </summary>
        /// <param name="sb"> The spritebatch used for the underlying rendering. </param>
        public override void Refresh(SpriteBatch sb)
        {
            DrawTexture.Dispose();
            DrawTexture.Start(sb, new Size(Math.Max(Background.Width, Foreground.Width), Background.Height + Foreground.Height + Focused.Height));
            DrawTexture.DrawAt(0, Background, Foreground.Bounds);
            DrawTexture.DrawAt(1, Foreground, new Rectangle(0, Background.Height, Foreground.Width, Foreground.Height));
            DrawTexture.DrawAt(2, Focused, new Rectangle(0, Background.Height + Foreground.Height, Focused.Width, Focused.Height));
            DrawTexture.End();
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