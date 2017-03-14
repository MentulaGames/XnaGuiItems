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
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Core.Handlers;
    using static Utilities;

    /// <summary>
    /// A Dropdown with clickable childs.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class DropDown : GuiItem
    {
        /// <summary>
        /// Gets or sets a value indicating if the <see cref="DropDown"/> will adjust its size to the text.
        /// </summary>
        public virtual bool AutoSize { get; set; }
        /// <summary>
        /// Gets or sets the type of border given to the <see cref="DropDown"/>.
        /// </summary>
        public virtual BorderStyle BorderStyle { get; set; }
        /// <summary>
        /// Gets the default background color of the <see cref="DropDown"/>.
        /// </summary>
        new public static Color DefaultBackColor { get { return Color.FromNonPremultiplied(93, 84, 71, 256); } }
        /// <summary>
        /// Gets the default foreground color of the <see cref="DropDown"/>.
        /// </summary>
        new public static Color DefaultForeColor { get { return Color.FromNonPremultiplied(198, 179, 148, 256); } }
        /// <summary>
        /// Gets the default background color of the <see cref="DropDown"/> header.
        /// </summary>
        public static Color DefaultHeaderBackColor { get { return Color.FromNonPremultiplied(7, 8, 2, 256); } }
        /// <summary>
        /// Gets the default text the header will display.
        /// </summary>
        public static string DefaultHeaderText { get { return "Choose Option"; } }
        /// <summary>
        /// Gets or sets the text displayed in the <see cref="DropDown"/> header.
        /// Default: "Choose Option".
        /// </summary>
        public virtual string HeaderText { get; set; }
        /// <summary>
        /// Gets or sets the background color for the <see cref="DropDown"/> header.
        /// </summary>
        public virtual Color HeaderBackgroundColor { get; set; }

        /// <summary>
        /// Occurs when a <see cref="DropDown"/> option is clicked.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_INDEX)]
        public event IndexedClickEventHandler IndexClick;

        /// <summary> The specified <see cref="SpriteFont"/>. </summary>
        protected SpriteFont font;
        /// <summary> The underlying labels for the <see cref="DropDown"/>. </summary>
        protected Pair[][] labels;
        /// <summary> The rectangle used in the foreground. </summary>
        protected Rectangle foregroundRectangle;

        new private DropDownTextureHandler textures { get { return (DropDownTextureHandler)base.textures; } set { base.textures = value; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDown"/> class with default settings.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public DropDown(ref SpriteBatch sb, SpriteFont font)
            : this(ref sb, DefaultBounds, font)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDown"/> class with a specific size.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="bounds"> The size of the <see cref="DropDown"/> in pixels. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        [SuppressMessage(CAT_USAGE, CHECKID_CALL, Justification = JUST_VIRT_FINE)]
        public DropDown(ref SpriteBatch sb, Rectangle bounds, SpriteFont font)
            : base(ref sb, bounds)
        {
            this.font = font;
            HeaderText = DefaultHeaderText;
            labels = new Pair[0][];
            foregroundRectangle = bounds;

            BackColor = DefaultBackColor;
            ForeColor = DefaultForeColor;
            HeaderBackgroundColor = DefaultHeaderBackColor;
        }

        /// <summary>
        /// Adds a option to the <see cref="DropDown"/> with the default color.
        /// </summary>
        /// <param name="parts"> The parts of the option, separated with a space. </param>
        public void AddOption(params string[] parts)
        {
            Pair[] colorParts = new Pair[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                colorParts[i] = new Pair(parts[i], ForeColor);
            }

            AddOption(colorParts);
        }

        /// <summary>
        /// Adds a option to the <see cref="DropDown"/> with a specified color.
        /// </summary>
        /// <param name="parts"> The parts of the option, with a specified colot and separated with a space. </param>
        public void AddOption(params Pair[] parts)
        {
            int index = labels.Length;
            Array.Resize(ref labels, index + 1);

            for (int i = 0; i < parts.Length; i++)
            {
                Color c = parts[i].Color.HasValue ? parts[i].Color.Value : ForeColor;
                parts[i] = new Pair(parts[i].Text, c);
            }

            labels[index] = parts;
        }

        /// <summary>
        /// Updates the <see cref="DropDown"/>, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="deltaTime"> The specified deltatime. </param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Enabled)
            {
                MouseState mState = Mouse.GetState();

                bool down = over && (mState.LeftButton == ButtonState.Pressed || mState.RightButton == ButtonState.Pressed);

                for (int i = 0, hover = GetHoverIndex(mState); i < textures.Buttons.Length; i++)
                {
                    if (i == hover) textures.Buttons[i].state = down ? ButtonStyle.Click : ButtonStyle.Hover;
                    else textures.Buttons[i].state = ButtonStyle.Default;
                }
            }
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            if (!suppressRefresh)
            {
                HandleAutoSize();
                foregroundRectangle = Bounds;
            }

            base.Refresh();
        }

        /// <summary>
        /// Draws the <see cref="DropDown"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The specified <see cref="SpriteBatch"/>. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible)
            {
                float fontHeight = font.GetHeight();
                for (int i = 0; i < textures.Buttons.Length; i++)
                {
                    spriteBatch.Draw(
                        textures.Buttons[i].DrawTexture,
                        new Vector2(Position.X, Position.Y + (fontHeight * (i + 1))),
                        null,
                        Color.White,
                        Rotation,
                        Origin,
                        1f,
                        SpriteEffects.None,
                        0f);
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="AutoSize"/> functionality.
        /// </summary>
        protected virtual void HandleAutoSize()
        {
            if (AutoSize)
            {
                Vector2 dim = font.MeasureString(HeaderText);

                for (int i = 0; i < labels.Length; i++)
                {
                    string full = string.Join(" ", Pair.GetKeys(labels[i]));
                    Vector2 newDim = font.MeasureString(full);

                    if (newDim.X > dim.X) dim.X = newDim.X;
                    dim.Y += newDim.Y;
                }

                dim.X += 3;
                if (dim.X != Width || dim.Y != Height) Size = new Size(dim);
            }
        }

        /// <summary>
        /// This method is called when the <see cref="GuiItem.Move"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new position for the <see cref="Label"/>. </param>
        protected override void OnMove(GuiItem sender, ValueChangedEventArgs<Vector2> e)
        {
            base.OnMove(sender, e);
            foregroundRectangle.X = (int)e.NewValue.X;
            foregroundRectangle.Y = (int)e.NewValue.Y;
        }

        /// <summary>
        /// Sets the background texture for the <see cref="DropDown"/>.
        /// </summary>
        protected override void SetBackgroundTexture()
        {
            textures.SetBackFromClr(BackColor, HeaderBackgroundColor, Size, new Size(Width, (int)font.GetHeight()), batch, BorderStyle);
        }

        /// <summary>
        /// Sets the foreground texture for the <see cref="DropDown"/>.
        /// </summary>
        protected override void SetForegroundTexture()
        {
            textures.SetText(HeaderText, font, ForeColor, new Size(font.MeasureString(HeaderText)), false, 0, batch);
            textures.SetButtons(labels, new Size(Width, Height / (labels.Length + 1)), font, batch);
        }

        /// <summary>
        /// Sets <see cref="GuiItem.textures"/> to the required <see cref="DropDown"/>.
        /// </summary>
        protected override void SetTextureHandler()
        {
            textures = new DropDownTextureHandler();
        }

        /// <summary>
        /// Handles the initializing of the events.
        /// </summary>
        protected override void InitEvents()
        {
            base.InitEvents();
            Click += OnClick;
        }

        private void OnClick(GuiItem sender, MouseEventArgs e)
        {
            int index = GetHoverIndex(e.State);
            if (index == -1) return;

            Invoke(IndexClick, this, new IndexedClickEventArgs(index));
        }

        private int GetHoverIndex(MouseState state)
        {
            int relativeY = state.Y - (int)Position.Y - textures.Foreground.Height;
            return relativeY < 0 ? -1 : relativeY / textures.Foreground.Height;
        }
    }
}