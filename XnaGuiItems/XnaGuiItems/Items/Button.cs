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
    using Core;
    using Core.Handlers;
    using Core.Interfaces;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using static Utilities;

    /// <summary>
    /// A button base class.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Button : Label
    {
        new private ButtonTextureHandler textures { get { return (ButtonTextureHandler)base.textures; } }

        private Texture2D drawTexture;
        private bool leftInvoked;
        private bool rightInvoked;
        private int doubleLeftClicked;
        private int doubleRightClicked;
        private float time;

        /// <summary>
        /// Occurs when the <see cref="Button"/> is left clicked.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event MouseEventHandler LeftClick;
        /// <summary>
        /// Occurs when the <see cref="Button"/> is right clicked.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event MouseEventHandler RightClick;
        /// <summary>
        /// Occurs when the <see cref="Button"/> is clicked twice in a short period of time.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event MouseEventHandler DoubleClick;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class with default settings.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public Button(ref SpriteBatch sb, SpriteFont font)
             : this(ref sb, DefaultBounds, font)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class with a specified size.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="bounds"> The size of the <see cref="Button"/> in pixels. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public Button(ref SpriteBatch sb, Rectangle bounds, SpriteFont font)
             : base(ref sb, bounds, font)
        {
            base.textures = new ButtonTextureHandler();
            drawTexture = textures.Background;
        }

        /// <summary>
        /// Updates the <see cref="Button"/>, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="deltaTime"> The deltatime </param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Enabled)
            {
                if (!over) drawTexture = textures.Background;
                else if (over && !leftDown && !rightDown) drawTexture = textures.Hover;

                if (leftDown && !leftInvoked)
                {
                    Invoke(LeftClick, this, GetMouseEventArgs());
                    leftInvoked = true;

                    doubleLeftClicked++;
                    drawTexture = textures.Click;
                }
                if (rightDown && !rightInvoked)
                {
                    Invoke(RightClick, this, GetMouseEventArgs());
                    rightInvoked = true;

                    doubleRightClicked++;
                    drawTexture = textures.Click;
                }

                time += deltaTime;
                if (doubleLeftClicked > 1 || doubleRightClicked > 1 || time > 1)
                {
                    doubleLeftClicked = 0;
                    doubleRightClicked = 0;
                    if (time < 1) Invoke(DoubleClick, this, GetMouseEventArgs());
                    time = 0;
                }

                if (leftInvoked && !leftDown) leftInvoked = false;
                if (rightInvoked && !rightDown) rightInvoked = false;
            }
        }

        /// <summary>
        /// Draws the <see cref="Button"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The <see cref="SpriteBatch"/> to use. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(drawTexture, Position, null, Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, 1f);
                spriteBatch.Draw(textures.Foreground, foregroundRectangle, null, Color.White, Rotation, Origin, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
            if (suppressRefresh) return;

            textures.ApplyBorders();
        }

        /// <summary>
        /// Perfoms a specified button click.
        /// </summary>
        /// <param name="button"> The type of click event to invoke. </param>
        public void PerformClick(MouseClick button = MouseClick.Default)
        {
            switch (button)
            {
                case MouseClick.Default:
                    base.PerformClick();
                    return;
                case MouseClick.Left:
                    Invoke(LeftClick, this, GetMouseEventArgs());
                    return;
                case MouseClick.Right:
                    Invoke(RightClick, this, GetMouseEventArgs());
                    return;
                case MouseClick.Double:
                    Invoke(DoubleClick, this, GetMouseEventArgs());
                    return;
            }
        }
    }
}