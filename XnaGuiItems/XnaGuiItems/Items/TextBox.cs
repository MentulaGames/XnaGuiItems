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
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class TextBox : Label
    {
        /// <summary>
        /// Gets the default minimum size of the <see cref="TextBox"/>.
        /// </summary>
        public static Vector2 DefaultMinimumSize { get { return new Vector2(100, 50); } }
        /// <summary>
        /// Gets or sets a value indicating how the <see cref="TextBox"/> should flicker.
        /// </summary>
        public virtual FlickerStyle FlickerStyle { get; set; }
        /// <summary>
        /// Gets or sets a value indicating if the <see cref="TextBox"/> is in focus.
        /// </summary>
        public virtual bool Focused { get; set; }
        /// <summary>
        /// Gets or sets a value indicating of the <see cref="TextBox"/> can be multiline.
        /// </summary>
        public virtual bool MultiLine { get; set; }
        /// <summary>
        /// Gets or set a value indicating if the <see cref="TextBox"/> is a password field and which char it should use.
        /// Default value = '\0' indicating that it is not a password field.
        /// </summary>
        public virtual char PasswordChar { get; set; }
        /// <summary>
        /// Gets or sets a value indicating the minimum size of the <see cref="TextBox"/>.
        /// Default value = (X = 100, Y = 50)
        /// </summary>
        public virtual Vector2 MinimumSize { get; set; }
        /// <summary>
        /// Gets or sets a value indicating the maximum size of the <see cref="TextBox"/>.
        /// Default value is the screen size.
        /// </summary>
        public virtual Vector2 MaximumSize { get; set; }

        private KeyInputHandler inputHandler;
        private float time;
        private bool showLine;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class with default settings.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="TextBox"/> to. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public TextBox(GraphicsDevice device, SpriteFont font)
            : this(device, DefaultSize, font)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class with a specific size.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="TextBox"/> to. </param>
        /// <param name="bounds"> The size of the <see cref="TextBox"/> in pixels. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public TextBox(GraphicsDevice device, Rectangle bounds, SpriteFont font)
            : base(device, bounds, font)
        {
            inputHandler = new KeyInputHandler();
            FlickerStyle = FlickerStyle.Normal;
            MinimumSize = DefaultMinimumSize;
            MaximumSize = device.Viewport.Bounds.Size();
        }

        /// <summary>
        /// Updates the <see cref="TextBox"/>, checking if any mouse event are occuring.
        /// This should only be used when the <see cref="TextBox"/> is out of focus!
        /// </summary>
        /// <param name="state"> The current state of the <see cref="Mouse"/>. </param>
        public override void Update(MouseState state) { base.Update(state); }

        /// <summary>
        /// Updates the <see cref="TextBox"/>, checking if any mouse- or keyboard event are occuring.
        /// Use like: myTextBox.Update(Mouse.GetState(), Keyboard.GetState(), (float)gameTime.ElapsedGameTime.TotalSeconds);
        /// </summary>
        /// <param name="mState"> The current state of the <see cref="Mouse"/>. </param>
        /// <param name="kState"> The current state of the <see cref="Keyboard"/>. </param>
        /// <param name="deltaTime"> The specified deltatime. </param>
        public void Update(MouseState mState, KeyboardState kState, float deltaTime)
        {
            base.Update(mState);

            if (Enabled)
            {
                if (Focused)
                {
                    string newText = inputHandler.GetInputString(kState, MultiLine);
                    if (Text != newText) Text = newText;

                    foregoundTexture = Drawing.FromText(GetDrawableText() + (showLine ? "|" : ""), font, ForeColor, foregroundRectangle.Width, foregroundRectangle.Height, MultiLine, LineStart, device);

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
                    foregoundTexture = Drawing.FromText(GetDrawableText(), font, ForeColor, Bounds.Width, Bounds.Height, MultiLine, LineStart, device);
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

                bool width = dim.X != Bounds.Width;
                bool height = dim.Y != Bounds.Height;

                if (width && height) Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)dim.X, (int)dim.Y);
                else if (width) Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)dim.X, Bounds.Height);
                else if (height) Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, (int)dim.Y);
            }

            foregroundRectangle = Bounds;
            backColorImage = backColorImage.ApplyBorderLabel(BorderStyle);
            if (BackgroundImage != null) BackgroundImage = BackgroundImage.ApplyBorderLabel(BorderStyle);
            foregoundTexture = Drawing.FromText(GetDrawableText() + (showLine ? "|" : ""), font, ForeColor, foregroundRectangle.Width, foregroundRectangle.Height, MultiLine, LineStart, device);
        }

        protected override void OnTextChanged(GuiItem sender, string newText)
        {
            inputHandler.keyboadString = newText;
            base.OnTextChanged(sender, newText);
        }

        private Vector2 GetLongTextDimentions()
        {
            if (!MultiLine) return font.MeasureString(Text);

            string longText = Text.Split(new string[1] { "/n" }, StringSplitOptions.None).Max();
            return font.MeasureString(longText);
        }

        private string GetDrawableText()
        {
            return PasswordChar != '\0' ? Text.ToPassword(PasswordChar) : Text;
        }
    }
}