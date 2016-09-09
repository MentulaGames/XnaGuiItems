﻿using Mentula.GuiItems.Core;
using Mentula.GuiItems.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using static Mentula.GuiItems.Utilities;

namespace Mentula.GuiItems.Items
{
    /// <summary>
    /// A textbox used for displaying and accepting text.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class TextBox : Label, IDeltaKeyboardUpdate
    {
        /// <summary>
        /// Gets the default minimum size of the <see cref="TextBox"/>.
        /// </summary>
        public static Size DefaultMinimumSize { get { return new Size(100, 50); } }
        /// <summary>
        /// Gets or sets a value indicating how the <see cref="TextBox"/> should flicker.
        /// </summary>
        public virtual FlickerStyle FlickerStyle { get; set; }
        /// <summary>
        /// Gets or sets a value indicating if the <see cref="TextBox"/> is in focus.
        /// </summary>
        public virtual bool Focused { get { return focus; } set { Invoke(FocusChanged, this, value); } }
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
        /// </summary>
        public virtual Size MinimumSize { get; set; }
        /// <summary>
        /// Gets or sets a value indicating the maximum size of the <see cref="TextBox"/>.
        /// Default value is the screen size.
        /// </summary>
        public virtual Size MaximumSize { get; set; }

        /// <summary>
        /// Occurs when the value of the <see cref="Focused"/> propery is changed.
        /// </summary>
        public event ValueChangedEventHandler<bool> FocusChanged;

        /// <summary> The <see cref="Texture2D"/> used for drawing the foreground (with the focus indicator). </summary>
        protected Texture2D foregoundTextureFocused;

        private KeyInputHandler inputHandler;
        private float time;
        private bool showLine;
        private bool focus;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class with default settings.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="TextBox"/> to. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public TextBox(GraphicsDevice device, SpriteFont font)
            : this(device, DefaultBounds, font)
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
            FocusChanged += OnFocusChanged;
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

                    if (FlickerStyle == FlickerStyle.None) return;

                    time += deltaTime;
                    switch (FlickerStyle)
                    {
                        case FlickerStyle.Slow:
                            if (time > 2) ToggleShowLine();
                            break;
                        case FlickerStyle.Normal:
                            if (time > 1) ToggleShowLine();
                            break;
                        case FlickerStyle.Fast:
                            if (time > .5f) ToggleShowLine();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the <see cref="TextBox"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The specified <see cref="SpriteBatch"/>. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D temp = null;
            if (showLine)
            {
                temp = foregroundTexture;
                foregroundTexture = foregoundTextureFocused;
            }

            base.Draw(spriteBatch);

            if (temp != null) foregroundTexture = temp;
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            if (suppressRefresh) return;

            if (AutoSize)
            {
                Vector2 dim = GetLongTextDimentions();
                dim.X += 3;
                if (dim.Y < MinimumSize.Height) dim.Y = MinimumSize.Height;
                else if (dim.Y > MaximumSize.Height) dim.Y = MaximumSize.Height;

                if (dim.X < MinimumSize.Width) dim.X = MinimumSize.Width;
                else if (dim.X > MaximumSize.Width) dim.X = MaximumSize.Width;

                bool width = dim.X != Bounds.Width;
                bool height = dim.Y != Bounds.Height;

                if (width && height) Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)dim.X, (int)dim.Y);
                else if (width) Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)dim.X, Bounds.Height);
                else if (height) Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, (int)dim.Y);
            }

            foregroundRectangle = Bounds;
            backColorImage = backColorImage.ApplyBorderLabel(BorderStyle);
            if (BackgroundImage != null) BackgroundImage = BackgroundImage.ApplyBorderLabel(BorderStyle);
            CreateForegroundTextures();
        }

        /// <summary>
        /// This method gets called when the <see cref="Label.TextChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="newText"> The new text for the sender. </param>
        protected override void OnTextChanged(GuiItem sender, string newText)
        {
            inputHandler.keyboadString = newText;
            base.OnTextChanged(sender, newText);
        }

        /// <summary>
        /// This method gets called when the <see cref="TextBox.FocusChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="recievedFocus"> The new focus value for the sender. </param>
        protected virtual void OnFocusChanged(GuiItem sender, bool recievedFocus)
        {
            focus = recievedFocus;
            if (!recievedFocus)
            {
                CreateForegroundTextures();
                showLine = false;
            }
        }

        private void CreateForegroundTextures()
        {
            string text = GetDrawableText();
            Size size = foregroundRectangle.Size();

            foregroundTexture = Drawing.FromText(text, font, ForeColor, size, MultiLine, LineStart, device);
            foregoundTextureFocused = Drawing.FromText(text + '|', font, ForeColor, size, MultiLine, LineStart, device);
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

        private void ToggleShowLine()
        {
            time = 0;
            showLine = !showLine;
        }
    }
}