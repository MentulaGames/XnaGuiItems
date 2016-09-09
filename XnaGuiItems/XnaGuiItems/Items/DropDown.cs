using Mentula.GuiItems.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mentula.GuiItems.Items
{
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
        /// Gets or sets the position of the <see cref="GuiItem"/>.
        /// </summary>
        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                foregroundRectangle.X = (int)value.X;
                foregroundRectangle.Y = (int)value.Y;
            }
        }

        /// <summary>
        /// Occurs when a <see cref="DropDown"/> option is clicked.
        /// </summary>
        public event ValueChangedEventHandler<int> IndexClick;

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
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="DropDown"/> to. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public DropDown(GraphicsDevice device, SpriteFont font)
            : this(device, DefaultBounds, font)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDown"/> class with a specific size.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="DropDown"/> to. </param>
        /// <param name="bounds"> The size of the <see cref="DropDown"/> in pixels. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public DropDown(GraphicsDevice device, Rectangle bounds, SpriteFont font)
            : base(device, bounds)
        {
            this.font = font;
            Init();
        }

        /// <summary>
        /// Adds a option to the <see cref="DropDown"/> with the default color.
        /// </summary>
        /// <param name="parts"> The parts of the option, separated with a space. </param>
        public void AddOption(params string[] parts)
        {
            KeyValuePair<string, Color?>[] colorParts = new KeyValuePair<string, Color?>[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                colorParts[i] = new KeyValuePair<string, Color?>(parts[i], ForeColor);
            }

            AddOption(colorParts);
        }

        /// <summary>
        /// Adds a option to the <see cref="DropDown"/> with a specified color.
        /// </summary>
        /// <param name="parts"> The parts of the option, with a specified colot and separated with a space. </param>
        public void AddOption(params KeyValuePair<string, Color?>[] parts)
        {
            int index = labels.Length;
            Array.Resize(ref labels, index + 1);

            KeyValuePair<string, Color>[] fixedParts = new KeyValuePair<string, Color>[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                KeyValuePair<string, Color?> cur = parts[i];
                Color c = cur.Value == null ? ForeColor : cur.Value.Value;
                fixedParts[i] = new KeyValuePair<string, Color>(cur.Key, c);
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
            if (Utilities.suppressRefresh) return;

            if (AutoSize)
            {
                Vector2 dim = font.MeasureString(HeaderText);
                int yAdder = 0;

                for (int i = 0; i < labels.Length; i++)
                {
                    string full = string.Join(" ", labels[i].Select(l => l.Key));
                    Vector2 newDim = font.MeasureString(full);

                    if (newDim.X > dim.X) dim.X = newDim.X;
                    yAdder += (int)newDim.Y;
                }

                dim.X += 3;
                bool width = dim.X != Bounds.Width ? true : false;
                bool height = dim.Y != Bounds.Height ? true : false;

                if (width && height) Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)dim.X, (int)dim.Y + yAdder);
                else if (width) Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)dim.X, Bounds.Height);
                else if (height) Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, (int)dim.Y + yAdder);
            }

            foregroundRectangle = Bounds;
            backColorImage = backColorImage.ApplyBorderLabel(BorderStyle);
            if (BackgroundImage != null) BackgroundImage = BackgroundImage.ApplyBorderLabel(BorderStyle);

            foregroundTexture = Drawing.FromText(HeaderText, font, ForeColor, foregroundRectangle.Size(), false, 0, device);
            headerTexture = Drawing.FromColor(HeaderBackgroundColor, new Size(foregroundTexture.Width, (int)font.MeasureString("a").Y), device);

            itemTextures = new KeyValuePair<Texture2D, ButtonStyle>[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                itemTextures[i] = new KeyValuePair<Texture2D, ButtonStyle>(
                    Drawing.FromLabels(labels[i], font, new Size(foregroundTexture.Width, foregroundRectangle.Height / (labels.Length + 1)), device),
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
                spriteBatch.Draw(headerTexture, foregroundRectangle.Position(), null, Color.White, Rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                spriteBatch.Draw(foregroundTexture, foregroundRectangle, null, Color.White, Rotation, Vector2.Zero, SpriteEffects.None, 0f);

                float fontHeight = font.MeasureString("a").Y;
                for (int i = 0; i < itemTextures.Length; i++)
                {
                    Texture2D texture = itemTextures[i].Value == ButtonStyle.Default ? itemTextures[i].Key : itemTextures[i].Key.ApplyBorderButton(itemTextures[i].Value);
                    spriteBatch.Draw(
                        texture,
                        new Vector2(Position.X, Position.Y + (fontHeight * (i + 1))),
                        null,
                        Color.White,
                        Rotation,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0f);
                }
            }
        }

        private void Init()
        {
            HeaderText = DefaultHeaderText;
            labels = new KeyValuePair<string, Color>[0][];
            itemTextures = new KeyValuePair<Texture2D, ButtonStyle>[0];
            foregroundRectangle = DefaultBounds;

            BackColor = DefaultBackColor;
            ForeColor = DefaultForeColor;
            HeaderBackgroundColor = DefaultHeaderBackColor;
            Click += DropDown_Click;
        }

        private void DropDown_Click(GuiItem sender, MouseState state)
        {
            int index = GetHoverIndex(state);
            if (index == -1) return;

            if (IndexClick != null) IndexClick(sender, index);
        }

        private int GetHoverIndex(MouseState state)
        {
            int relativeY = state.Y - (int)Position.Y - headerTexture.Height;
            int lineHeight = headerTexture.Height;

            if (relativeY < 0) return -1;
            int remain = relativeY % lineHeight;
            return relativeY / lineHeight;
        }
    }
}