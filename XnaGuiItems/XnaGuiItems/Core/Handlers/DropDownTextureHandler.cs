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
    /// The class that handles the textures for a <see cref="Items.DropDown"/>.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class DropDownTextureHandler : TextureHandler
    {
        /// <summary>
        /// The textures for the underlying buttons.
        /// </summary>
        public DropDownButtonTextureHandler[] Buttons { get; set; }

        internal DropDownTextureHandler()
        {
            Buttons = new DropDownButtonTextureHandler[0];
        }

        internal void SetBackFromClr(Color back, Color header, Size size, Size headerSize, SpriteBatch sb, BorderStyle style)
        {
            internCall = true;
            if (!userSet[0])
            {
                Rectangle backRect = new Rectangle(0, headerSize.Height, size.Width, size.Height);
                Rectangle headerRect = new Rectangle(0, 0, headerSize.Width, headerSize.Height);
                Background = Drawing.FromMultiColor(back, header, backRect, headerRect, sb.GraphicsDevice).ApplyBorderLabel(style);
            }
            internCall = false;
        }

        internal void SetButtons(Pair[][] labels, Size size, SpriteFont font, SpriteBatch sb)
        {
            Buttons = new DropDownButtonTextureHandler[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                Buttons[i] = new DropDownButtonTextureHandler();
                Buttons[i].SetBackFromLabels(labels[i], size, font, sb);
                Buttons[i].ApplyBorders();
            }
        }
    }
}