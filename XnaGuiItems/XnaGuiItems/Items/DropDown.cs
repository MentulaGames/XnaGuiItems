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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
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
        new public static Color DefaultBackColor { get { return Color.FromNonPremultiplied(42, 41, 27, 256); } }
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
        protected KeyValuePair<string, Color>[][] labels;
        /// <summary> The rectangle used in the foreground. </summary>
        protected Rectangle foregroundRectangle;

        private Texture2D headerTexture;
        private KeyValuePair<Texture2D, ButtonStyle>[] itemTextures;

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
            labels = new KeyValuePair<string, Color>[0][];
            itemTextures = new KeyValuePair<Texture2D, ButtonStyle>[0];
            foregroundRectangle = DefaultBounds;

            BackColor = DefaultBackColor;
            ForeColor = DefaultForeColor;
            HeaderBackgroundColor = DefaultHeaderBackColor;
            Click += DropDown_Click;
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

            KeyValuePair<string, Color>[] fixedParts = new KeyValuePair<string, Color>[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                Pair cur = parts[i];
                Color c = cur.Color.HasValue ? cur.Color.Value : ForeColor;
                fixedParts[i] = new KeyValuePair<string, Color>(cur.Text, c);
            }

            labels[index] = fixedParts;
        }

        /// <summary>
        /// Updates the <see cref="DropDown"/>, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="state"> The current state of the <see cref="Mouse"/>. </param>
        public override void Update(MouseState state)
        {
            base.Update(state);

            if (Enabled)
            {
                bool down = over && (state.LeftButton == ButtonState.Pressed || state.RightButton == ButtonState.Pressed);

                for (int i = 0; i < itemTextures.Length; i++)
                {
                    itemTextures[i] = new KeyValuePair<Texture2D, ButtonStyle>(itemTextures[i].Key, ButtonStyle.Default);
                }

                if (over)
                {
                    int index = GetHoverIndex(state);
                    if (index != -1)
                    {
                        itemTextures[index] = new KeyValuePair<Texture2D, ButtonStyle>(itemTextures[index].Key, down ? ButtonStyle.Click : ButtonStyle.Hover);
                    }
                }
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
                Vector2 dim = font.MeasureString(HeaderText);

                for (int i = 0; i < labels.Length; i++)
                {
                    string full = string.Join(" ", labels[i].Select(l => l.Key));
                    Vector2 newDim = font.MeasureString(full);

                    if (newDim.X > dim.X) dim.X = newDim.X;
                    dim.Y += newDim.Y;
                }

                dim.X += 3;
                if ((dim.X != Bounds.Width && dim.X != 0) ||
                    (dim.Y != Bounds.Height && dim.Y != 0))
                {
                    Size = new Size(dim);
                }
            }

            foregroundRectangle = Bounds;
            textures.Background = textures.Background.ApplyBorderLabel(BorderStyle);
            if (BackgroundImage != null) BackgroundImage = BackgroundImage.ApplyBorderLabel(BorderStyle);

            textures.Foreground = Drawing.FromText(HeaderText, font, ForeColor, foregroundRectangle.Size(), false, 0, batch);
            headerTexture = Drawing.FromColor(HeaderBackgroundColor, new Size(textures.Foreground.Width, (int)font.GetHeight()), batch.GraphicsDevice);

            itemTextures = new KeyValuePair<Texture2D, ButtonStyle>[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                itemTextures[i] = new KeyValuePair<Texture2D, ButtonStyle>(
                    Drawing.FromLabels(labels[i], font, new Size(textures.Foreground.Width, foregroundRectangle.Height / (labels.Length + 1)), batch),
                    ButtonStyle.Default);
            }
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
                spriteBatch.Draw(headerTexture, foregroundRectangle.Position(), null, Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, 0f);
                spriteBatch.Draw(textures.Foreground, foregroundRectangle, null, Color.White, Rotation, Origin, SpriteEffects.None, 0f);

                float fontHeight = font.GetHeight();
                for (int i = 0; i < itemTextures.Length; i++)
                {
                    Texture2D texture = itemTextures[i].Value == ButtonStyle.Default ? itemTextures[i].Key : itemTextures[i].Key.ApplyBorderButton(itemTextures[i].Value);
                    spriteBatch.Draw(
                        texture,
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

        private void DropDown_Click(GuiItem sender, MouseEventArgs e)
        {
            int index = GetHoverIndex(e.State);
            if (index == -1) return;

            Invoke(IndexClick, this, new IndexedClickEventArgs(index));
        }

        private int GetHoverIndex(MouseState state)
        {
            int relativeY = state.Y - (int)Position.Y - headerTexture.Height;
            return relativeY < 0 ? -1 : relativeY / headerTexture.Height;
        }
    }
}