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
    using Core.Input;
    using Core.Interfaces;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using static Utilities;
    using Args = Core.ValueChangedEventArgs<bool>;

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
        /// Gets the visible text of the <see cref="TextBox"/>. 
        /// </summary>
        public virtual string VisisbleText { get { return PasswordChar != '\0' ? Text.ToPassword(PasswordChar) : Text; } }
        /// <summary>
        /// Gets or sets a value indicating how the <see cref="TextBox"/> should flicker.
        /// </summary>
        public virtual FlickerStyle FlickerStyle { get; set; }
        /// <summary>
        /// Gets or sets a value indicating if the <see cref="TextBox"/> is in focus.
        /// </summary>
        public virtual bool Focused { get { return focus; } set { Invoke(FocusChanged, this, new Args(focus, value)); } }
        /// <summary>
        /// Gets or sets a value indicating the maximum length of the text input.
        /// Negative values indicate no maximum length.
        /// Default value = -1
        /// </summary>
        public virtual int MaxLength { get; set; }
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

        new private TextboxTextureHandler textures { get { return (TextboxTextureHandler)base.textures; } }

        /// <summary>
        /// Occurs when the value of the <see cref="Focused"/> propery is changed.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<bool> FocusChanged;

        /// <summary>
        /// Occures when return is pressed on a non <see cref="MultiLine"/> <see cref="TextBox"/>.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST)]
        public event GuiItemEventHandler Confirmed;

        private KeyInputHandler inputHandler;
        private float time;
        private bool showLine;
        private bool focus;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class with default settings.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public TextBox(ref SpriteBatch sb, SpriteFont font)
            : this(ref sb, DefaultBounds, font)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class with a specific size.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="bounds"> The size of the <see cref="TextBox"/> in pixels. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        [SuppressMessage(CAT_USAGE, CHECKID_CALL, Justification = JUST_VIRT_FINE)]
        public TextBox(ref SpriteBatch sb, Rectangle bounds, SpriteFont font)
            : base(ref sb, bounds, font)
        {
            base.textures = new TextboxTextureHandler();
            inputHandler = new KeyInputHandler();
            FlickerStyle = FlickerStyle.Normal;
            MinimumSize = DefaultMinimumSize;
            MaximumSize = batch.GraphicsDevice.Viewport.Bounds.Size();
            MaxLength = -1;
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

            if (Enabled && Focused)
            {
                bool confirmed = false;
                Text = inputHandler.GetInputString(kState, MultiLine, MaxLength, out confirmed);

                if (confirmed) Invoke(Confirmed, this, EventArgs.Empty);
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

        /// <summary>
        /// Draws the <see cref="TextBox"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The specified <see cref="SpriteBatch"/>. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D temp = null;
            if (showLine)
            {
                temp = textures.Foreground;
                textures.Swap();
            }

            base.Draw(spriteBatch);

            if (temp != null) textures.SetBack(temp);
        }

        /// <summary>
        /// Handles the <see cref="Label.AutoSize"/> functionality.
        /// </summary>
        protected override void HandleAutoSize()
        {
            if (AutoSize)
            {
                Vector2 dim = GetLongTextDimentions();
                dim.X += 3;

                if (dim.Y < MinimumSize.Height) dim.Y = MinimumSize.Height;
                else if (dim.Y > MaximumSize.Height) dim.Y = MaximumSize.Height;

                if (dim.X < MinimumSize.Width) dim.X = MinimumSize.Width;
                else if (dim.X > MaximumSize.Width) dim.X = MaximumSize.Width;

                if (dim.X > 0 && dim.X != Bounds.Width) Width = (int)dim.X;
                if (dim.Y > 0 && dim.Y != Bounds.Height) Height = (int)dim.Y;
            }
        }

        /// <summary>
        /// This method gets called when the <see cref="Label.TextChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new text for the sender. </param>
        protected override void OnTextChanged(GuiItem sender, ValueChangedEventArgs<string> e)
        {
            inputHandler.keyboadString = e.NewValue;
            base.OnTextChanged(sender, e);
        }

        /// <summary>
        /// This method gets called when the <see cref="TextBox.FocusChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new focus value for the sender. </param>
        protected virtual void OnFocusChanged(GuiItem sender, Args e)
        {
            focus = e.NewValue;
            if (!focus)
            {
                CreateTextTexture();
                showLine = false;
            }
            else Refresh();
        }

        /// <summary>
        /// Handles the creation on the text texture.
        /// Override for special text texture handling.
        /// </summary>
        protected override void CreateTextTexture()
        {
            textures.SetFocusText(VisisbleText, font, ForeColor, foregroundRectangle.Size(), MultiLine, LineStart, batch);
        }

        /// <summary>
        /// Handles the initializing of the events.
        /// </summary>
        protected override void InitEvents()
        {
            base.InitEvents();
            FocusChanged += OnFocusChanged;
        }

        private Vector2 GetLongTextDimentions()
        {
            if (!MultiLine) return font.MeasureString(Text);

            string longText = Text.Split(new string[1] { "/n" }, StringSplitOptions.None).Max();
            return font.MeasureString(longText);
        }

        private void ToggleShowLine()
        {
            time = 0;
            showLine = !showLine;
        }
    }
}