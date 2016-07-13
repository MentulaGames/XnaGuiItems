using Mentula.GuiItems.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static Mentula.GuiItems.Utilities;

namespace Mentula.GuiItems.Items
{
    /// <summary>
    /// A button base class.
    /// </summary>
    public class Button : Label
    {
        protected Texture2D hoverTexture;
        protected Texture2D clickTexture;

        private Texture2D drawTexture;
        private int doubleLeftClicked;
        private int doubleRightClicked;
        private float time;

        /// <summary>
        /// Occurs when the <see cref="Button"/> is left clicked.
        /// </summary>
        public event MouseEventHandler LeftClick;
        /// <summary>
        /// Occurs when the <see cref="Button"/> is right clicked.
        /// </summary>
        public event MouseEventHandler RightClick;
        /// <summary>
        /// Occurs when the <see cref="Button"/> is clicked twice in a short period of time.
        /// </summary>
        public event MouseEventHandler DoubleClick;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class with default settings.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="Button"/> to. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public Button(GraphicsDevice device, SpriteFont font)
             : base(device, font)
        {
            drawTexture = backColorImage;
        }

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
                bool lDown = over && state.LeftButton == ButtonState.Pressed;
                bool rDown = over && state.RightButton == ButtonState.Pressed;

                if (!over) drawTexture = backColorImage;
                else if (over && !lDown && !rDown) drawTexture = hoverTexture;

                if (!leftClicked && lDown)
                {
                    Invoke(LeftClick, this, state);
                    leftClicked = true;

                    doubleLeftClicked++;
                    drawTexture = clickTexture;
                }
                if (!rigthClicked && rDown)
                {
                    Invoke(RightClick, this, state);
                    rigthClicked = true;

                    doubleRightClicked++;
                    drawTexture = clickTexture;
                }

                if (leftClicked && state.LeftButton == ButtonState.Released) leftClicked = false;
                if (rigthClicked && state.RightButton == ButtonState.Released) rigthClicked = false;

                time += deltaTime;

                if (doubleLeftClicked > 1 || doubleRightClicked > 1 || time > 1)
                {
                    doubleLeftClicked = 0;
                    doubleRightClicked = 0;
                    if (time < 1) Invoke(DoubleClick, this, state);
                    time = 0;
                }
            }
        }

        /// <summary>
        /// Draws the <see cref="Button"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The <see cref="SpriteBatch"/> to use. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(drawTexture, Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);

                if (backgroundImage != null)
                {
                    spriteBatch.Draw(backgroundImage, Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
                }

                spriteBatch.Draw(foregoundTexture, foregroundRectangle, null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            if (AutoSize)
            {
                Vector2 dim = font.MeasureString(text);
                dim.X += 3;
                bool width = dim.X != bounds.Width ? true : false;
                bool height = dim.Y != bounds.Height ? true : false;

                if (width && height) Bounds = new Rectangle(bounds.X, bounds.Y, (int)dim.X, (int)dim.Y);
                else if (width) Bounds = new Rectangle(bounds.X, bounds.Y, (int)dim.X, bounds.Height);
                else if (height) bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, (int)dim.Y);
            }

            backColorImage = backColorImage.ApplyBorderButton(ButtonStyle.Default);
            hoverTexture = backColorImage.ApplyBorderButton(ButtonStyle.Hover);
            clickTexture = backColorImage.ApplyBorderButton(ButtonStyle.Click);

            if (backgroundImage != null) backgroundImage = backgroundImage.ApplyBorderButton(0);
            foregoundTexture = Drawing.FromText(text, font, foreColor, foregroundRectangle.Width, foregroundRectangle.Height, false, 0, device);
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
                    Invoke(LeftClick, this, Mouse.GetState());
                    return;
                case MouseClick.Right:
                    Invoke(RightClick, this, Mouse.GetState());
                    return;
                case MouseClick.Double:
                    Invoke(DoubleClick, this, Mouse.GetState());
                    return;
            }
        }
    }
}