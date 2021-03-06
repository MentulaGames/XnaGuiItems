﻿#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Items
{
#if MONO
    using Mono::Microsoft.Xna.Framework;
    using Mono::Microsoft.Xna.Framework.Graphics;
    using Mono::Microsoft.Xna.Framework.Input;
#else
    using Xna::Microsoft.Xna.Framework;
    using Xna::Microsoft.Xna.Framework.Graphics;
    using Xna::Microsoft.Xna.Framework.Input;
#endif
    using Core;
    using Core.EventHandlers;
    using Core.TextureHandlers;
    using Core.Input;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using DeJong.Utilities.Core;
    using static Utilities;
    using static DeJong.Utilities.Core.EventInvoker;
    using Args = Core.EventHandlers.ValueChangedEventArgs<bool>;

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
        public static Size DefaultMinimumSize { get { return new Size(100, 25); } }
        /// <summary>
        /// Gets or sets a value indicating how the <see cref="TextBox"/> should flicker.
        /// </summary>
        public virtual FlickerStyle FlickerStyle { get; set; }
        /// <summary>
        /// Gets or sets a value indicating if the <see cref="TextBox"/> is in focus.
        /// </summary>
        public virtual bool Focused { get { return focus; } set { Invoke(FocusChanged, this, new Args(focus, value)); } }
        /// <summary>
        /// Gets or sets a value indicating the types of input allowed in this <see cref="TextBox"/>.
        /// </summary>
        public virtual InputFlags InputType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating the maximum length of the text input.
        /// Negative values indicate no maximum length.
        /// Default value = -1
        /// </summary>
        public virtual int MaxLength { get; set; }
        /// <summary>
        /// Gets or sets a value indicating the maximum size of the <see cref="TextBox"/>.
        /// Default value is the screen size.
        /// </summary>
        public virtual Size MaximumSize { get; set; }
        /// <summary>
        /// Gets or sets a value indicating the minimum size of the <see cref="TextBox"/>.
        /// </summary>
        public virtual Size MinimumSize { get; set; }
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
        /// Gets the visible text of the <see cref="TextBox"/>. 
        /// </summary>
        public virtual string VisisbleText { get { return PasswordChar != '\0' ? new string(PasswordChar, Text.Length) : Text; } }

        new private TextboxTextureHandler textures { get { return (TextboxTextureHandler)base.textures; } set { base.textures = value; } }

        /// <summary>
        /// Occurs when the value of the <see cref="Focused"/> propery is changed.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event StrongEventHandler<TextBox, ValueChangedEventArgs<bool>> FocusChanged;

        /// <summary>
        /// Occures when return is pressed on a non <see cref="MultiLine"/> <see cref="TextBox"/>.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST)]
        public event StrongEventHandler<TextBox, EventArgs>  Confirmed;

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
        public TextBox(ref SpriteBatch sb, Rect bounds, SpriteFont font)
            : base(ref sb, bounds, font)
        {
            suppressChecking = true;
            inputHandler = new KeyInputHandler();
            FlickerStyle = FlickerStyle.Normal;
            MinimumSize = DefaultMinimumSize;
            MaximumSize = new Rect(batch.GraphicsDevice.Viewport.Bounds).Size;
            MaxLength = -1;
            AutoRefresh = true;
        }

        /// <summary>
        /// Updates the <see cref="TextBox"/>, checking if any mouse- or keyboard event are occuring.
        /// Use like: myTextBox.Update(Mouse.GetState(), Keyboard.GetState(), (float)gameTime.ElapsedGameTime.TotalSeconds);
        /// </summary>
        /// <param name="deltaTime"> The specified deltatime. </param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Enabled && Focused)
            {
                KeyboardState kState = Keyboard.GetState();

                bool confirmed = false;
                Text = inputHandler.GetInputString(kState, MultiLine, MaxLength, out confirmed, InputType);

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
            if (Visible)
            {
                spriteBatch.Draw(textures.DrawTexture.Texture, Position, textures.DrawTexture[0], textures.userset_background ? BackColor : Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, 1f);
                spriteBatch.Draw(textures.DrawTexture.Texture, Position, textures.DrawTexture[showLine ? 2 : 1], Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Handles the <see cref="Label.AutoSize"/> functionality.
        /// </summary>
        protected internal override void HandleAutoSize()
        {
            if (AutoSize)
            {
                Size dim = new Size(GetLongTextDimentions());
                dim.Width += 3;

                dim.Clamp(MinimumSize, MaximumSize);
                if (dim.Width != Width || dim.Height != Height) Size = dim;
            }
        }

        /// <summary>
        /// This method gets called when the <see cref="Label.TextChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new text for the sender. </param>
        protected override void OnTextChanged(Label sender, ValueChangedEventArgs<string> e)
        {
            inputHandler.keyboadString = e.NewValue;
            base.OnTextChanged(sender, e);
        }

        /// <summary>
        /// This method gets called when the <see cref="FocusChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="TextBox"/> that raised the event. </param>
        /// <param name="e"> The new focus value for the sender. </param>
        protected virtual void OnFocusChanged(TextBox sender, Args e)
        {
            focus = e.NewValue;
            if (!focus) showLine = false;
        }

        /// <summary>
        /// Sets the foreground texture for the <see cref="TextBox"/>.
        /// </summary>
        protected override void SetForegroundTexture()
        {
            textures.SetFocusText(VisisbleText, font, ForeColor, ForegroundRectangle.Size, MultiLine, LineStart, batch);
        }

        /// <summary>
        /// Sets <see cref="GuiItem.textures"/> to the required <see cref="TextureHandler"/>.
        /// </summary>
        protected override void SetTextureHandler()
        {
            textures = new TextboxTextureHandler();
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