using Mentula.GuiItems.Core;
using Mentula.GuiItems.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics.CodeAnalysis;
using static Mentula.GuiItems.Utilities;

namespace Mentula.GuiItems.Items
{
    /// <summary>
    /// A button base class.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Button : Label, IDeltaUpdate
    {
        /// <summary> The <see cref="Texture2D"/> used for drawing the hover style <see cref="Button"/>. </summary>
        protected Texture2D hoverTexture;
        /// <summary> The <see cref="Texture2D"/> used for drawing the click style <see cref="Button"/>. </summary>
        protected Texture2D clickTexture;

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
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="Button"/> to. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public Button(GraphicsDevice device, SpriteFont font)
             : this(device, DefaultBounds, font)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class with a specified size.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="Button"/> to. </param>
        /// <param name="bounds"> The size of the <see cref="Button"/> in pixels. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public Button(GraphicsDevice device, Rectangle bounds, SpriteFont font)
             : base(device, bounds, font)
        {
            drawTexture = backColorImage;
        }

        /// <summary>
        /// This method cannot be used withing a <see cref="Button"/>.
        /// </summary>
        /// <param name="mState"> The <see cref="MouseState"/> to use. </param>
        [Obsolete("Use Update(MouseState, float) instead", true)]
        new public void Update(MouseState mState) { }

        /// <summary>
        /// Updates the <see cref="Button"/>, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="state"> The current <see cref="Mouse"/> state. </param>
        /// <param name="deltaTime"> The deltatime </param>
        public void Update(MouseState state, float deltaTime)
        {
            base.Update(state);

            if (Enabled)
            {
                if (!over) drawTexture = backColorImage;
                else if (over && !leftDown && !rightDown) drawTexture = hoverTexture;

                if (leftDown && !leftInvoked)
                {
                    Invoke(LeftClick, this, GetMouseEventArgs());
                    leftInvoked = true;

                    doubleLeftClicked++;
                    drawTexture = clickTexture;
                }
                if (rightDown && !rightInvoked)
                {
                    Invoke(RightClick, this, GetMouseEventArgs());
                    rightInvoked = true;

                    doubleRightClicked++;
                    drawTexture = clickTexture;
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
                spriteBatch.Draw(drawTexture, Position, null, Color.White, Rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);

                if (BackgroundImage != null)
                {
                    spriteBatch.Draw(BackgroundImage, Position, null, Color.White, Rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
                }

                spriteBatch.Draw(foregroundTexture, foregroundRectangle, null, Color.White, Rotation, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            if (suppressRefresh) return;

            if (AutoSize)
            {
                Vector2 dim = font.MeasureString(Text);
                dim.X += 3;
                bool width = dim.X != Bounds.Width;
                bool height = dim.Y != Bounds.Height;

                if (width && height) Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)dim.X, (int)dim.Y);
                else if (width) Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)dim.X, Bounds.Height);
                else if (height) Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, (int)dim.Y);
            }

            backColorImage = backColorImage.ApplyBorderButton(ButtonStyle.Default);
            hoverTexture = backColorImage.ApplyBorderButton(ButtonStyle.Hover);
            clickTexture = backColorImage.ApplyBorderButton(ButtonStyle.Click);

            if (BackgroundImage != null) BackgroundImage = BackgroundImage.ApplyBorderButton(ButtonStyle.Default);
            foregroundTexture = Drawing.FromText(Text, font, ForeColor, foregroundRectangle.Size(), false, 0, device);
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