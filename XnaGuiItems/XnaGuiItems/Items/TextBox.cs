using Mentula.GuiItems.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Mentula.GuiItems.Items
{
    /// <summary>
    /// A textbox used for displaying and accepting text.
    /// </summary>
    public class TextBox : Label
    {
        /// <summary>
        /// Gets or sets a value indicating how the textBox should flicker.
        /// </summary>
        public virtual FlickerStyle FlickerStyle { get; set; }
        /// <summary>
        /// Gets or sets a value indicating if the item is in focus.
        /// </summary>
        public virtual bool Focused { get; set; }
        /// <summary>
        /// Gets or sets a value indicating of the textBox can be multiline.
        /// </summary>
        public virtual bool MultiLine { get; set; }
        /// <summary>
        /// Gets or set a value indicating if the textBox is a password field and which char it should use.
        /// Default value = '\0' indicating that it is not a password field.
        /// </summary>
        public virtual char PasswordChar { get; set; }
        /// <summary>
        /// Gets or sets a value indicating the minimum size of the textBox.
        /// Default value = (X = 100, Y = 50)
        /// </summary>
        public virtual Vector2 MinimumSize { get; set; }
        /// <summary>
        /// Gets or sets a value indicating the maximum size of the textbox.
        /// Default value is the screen size.
        /// </summary>
        public virtual Vector2 MaximumSize { get; set; }

        private KeyInputHandler inputHandler;
        private float time;
        private bool showLine;

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Items.TextBox class with default settings.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.TextBox to. </param>
        /// <param name="font"> The font to use while drawing the text. </param>
        public TextBox(GraphicsDevice device, SpriteFont font)
            : base(device, font)
        {
            inputHandler = new KeyInputHandler();
            FlickerStyle = FlickerStyle.Normal;
            MinimumSize = new Vector2(100, 50);
            MaximumSize = device.Viewport.Bounds.Size();
        }

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Items.TextBox class with a specific size.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.TextBox to. </param>
        /// <param name="bounds"> The size of the label in pixels. </param>
        /// <param name="font"> The font to use while drawing the text. </param>
        public TextBox(GraphicsDevice device, Rectangle bounds, SpriteFont font)
            : base(device, bounds, font)
        {
            inputHandler = new KeyInputHandler();
            FlickerStyle = FlickerStyle.Normal;
            MinimumSize = new Vector2(100, 50);
            MaximumSize = device.Viewport.Bounds.Size();
        }

        /// <summary>
        /// Updates the XnaMentula.GuiItems.Items.TextBox and its childs, checking if any mouse event are occuring.
        /// This should only be used when the textBox is out of focus!
        /// </summary>
        /// <param name="state"> The current state of the mouse. </param>
        public override void Update(MouseState state) { base.Update(state); }

        /// <summary>
        /// Updates the XnaMentula.GuiItems.Items.TextBox, checking if any mouse- or keyboard event are occuring.
        /// Use like: myTextBox.Update(Mouse.GetState(), Keyboard.GetState(), (float)gameTime.ElapsedGameTime.TotalSeconds);
        /// </summary>
        /// <param name="mState"> The current state of the mouse. </param>
        /// <param name="kState"> The current state of the keyboard. </param>
        public void Update(MouseState mState, KeyboardState kState, float deltaTime)
        {
            base.Update(mState);

            if (Enabled)
            {
                if (Focused)
                {
                    string newText = inputHandler.GetInputString(kState, MultiLine);
                    if (Text != newText) Text = newText;

                    foregoundTexture = Drawing.FromText((PasswordChar != '\0' ? text.ToPassword(PasswordChar) : text) + (showLine ? "|" : ""), font, foreColor, foregroundRectangle.Width, foregroundRectangle.Height, MultiLine, device);

                    if (FlickerStyle == FlickerStyle.None) return;

                    time += deltaTime;
                    switch (FlickerStyle)
                    {
                        case FlickerStyle.Slow:
                            if (time > 2)
                            {
                                showLine = !showLine;
                                time = 0;
                            }
                            break;
                        case FlickerStyle.Normal:
                            if (time > 1)
                            {
                                showLine = !showLine;
                                time = 0;
                            }
                            break;
                        case FlickerStyle.Fast:
                            if (time > .5f)
                            {
                                showLine = !showLine;
                                time = 0;
                            }
                            break;
                    }
                }
                else
                {
                    foregoundTexture = Drawing.FromText((PasswordChar != '\0' ? text.ToPassword(PasswordChar) : text), font, foreColor, bounds.Width, bounds.Height, MultiLine, device);
                }
            }
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            if (AutoSize)
            {
                Vector2 dim = GetLongTextDimentions();
                dim.X += 3;
                if (dim.Y < MinimumSize.Y) dim.Y = MinimumSize.Y;
                else if (dim.Y > MaximumSize.Y) dim.Y = MaximumSize.Y;

                if (dim.X < MinimumSize.X) dim.X = MinimumSize.X;
                else if (dim.X > MaximumSize.X) dim.X = MaximumSize.X;

                bool width = dim.X != bounds.Width ? true : false;
                bool height = dim.Y != bounds.Height ? true : false;

                if (width && height) Bounds = new Rectangle(bounds.X, bounds.Y, dim.X(), dim.Y());
                else if (width) Bounds = new Rectangle(bounds.X, bounds.Y, dim.X(), bounds.Height);
                else if (height) bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, dim.Y());
            }

            foregroundRectangle = bounds;
            backColorImage = backColorImage.ApplyBorderLabel(BorderStyle);
            if (backgroundImage != null) backgroundImage = backgroundImage.ApplyBorderLabel(BorderStyle);
            foregoundTexture = Drawing.FromText((PasswordChar != '\0' ? text.ToPassword(PasswordChar) : text) + (showLine ? "|" : ""), font, foreColor, foregroundRectangle.Width, foregroundRectangle.Height, MultiLine, device);
        }

        private Vector2 GetLongTextDimentions()
        {
            if (!MultiLine) return font.MeasureString(text);

            string longText = text.Split(new string[1] { "/n" }, StringSplitOptions.None).Max();
            return font.MeasureString(longText);
        }

        protected override void OnTextChanged(object sender, string newText)
        {
            inputHandler.keyboadString = newText;
            base.OnTextChanged(sender, newText);
        }
    }
}