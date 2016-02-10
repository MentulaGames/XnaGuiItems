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
    public class DropDown : GuiItem
    {
        /// <summary>
        /// Gets or sets a value indicating if the dropdown will adjust its size to the text.
        /// </summary>
        public virtual bool AutoSize { get; set; }
        /// <summary>
        /// Gets or sets the type of border given to the dropdown.
        /// </summary>
        public virtual BorderStyle BorderStyle { get; set; }
        /// <summary>
        /// Gets or sets the text displayed in the dropdown header.
        /// Default: "Choose Option".
        /// </summary>
        public virtual string HeaderText { get; set; }
        /// <summary>
        /// Gets or sets the background color for the dropdown header.
        /// </summary>
        public virtual Color HeaderBackgroundColor { get; set; }
        /// <summary>
        /// Gets or sets the position of the GuiItem.
        /// </summary>
        public override Vector2 Position { get { return base.Position; } set { base.Position = value; foregroundRectangle.X = value.X(); foregroundRectangle.Y = value.Y(); } }

        /// <summary>
        /// Occurs when a XnaMentula.GuiItems.Items.DropDown option is clicked.
        /// </summary>
        public event ValueChangedEventHandler<int> IndexClick;

        protected SpriteFont font;
        protected KeyValuePair<string, Color>[][] labels;
        protected Rectangle foregroundRectangle;

        private Texture2D headerTexture;
        private KeyValuePair<Texture2D, ButtonStyle>[] itemTextures;

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Items.DropDown class with default settings.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.DropDown to. </param>
        /// <param name="font"> The font to use while drawing the text. </param>
        public DropDown(GraphicsDevice device, SpriteFont font)
            : base(device)
        {
            Init(font);
        }

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Items.DropDown class with a specific size.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.DropDown to. </param>
        /// <param name="bounds"> The size of the dropdown in pixels. </param>
        /// <param name="font"> The font to use while drawing the text. </param>
        public DropDown(GraphicsDevice device, Rectangle bounds, SpriteFont font)
            : base(device, bounds)
        {
            Init(font);
        }

        /// <summary>
        /// Adds a option to the dropdown with the default color.
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
        /// Adds a option to the dropdown with a specified color.
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
        /// Updates the XnaMentula.GuiItems.Items.DropDown, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="state"> The current state of the mouse. </param>
        public override void Update(MouseState state)
        {
            if (Enabled)
            {
                bool over = new Rectangle(Position.X(), Position.Y(), bounds.Width, bounds.Height).Contains(state.X, state.Y);
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

            base.Update(state);
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public void Refresh()
        {
            if (AutoSize)
            {
                Vector2 dim = font.MeasureString(HeaderText);
                int yAdder = 0;

                for (int i = 0; i < labels.Length; i++)
                {
                    string full = string.Join(" ", labels[i].Select(l => l.Key));
                    Vector2 newDim = font.MeasureString(full);

                    if (newDim.X > dim.X) dim.X = newDim.X;
                    yAdder += newDim.Y();
                }

                dim.X += 3;
                bool width = dim.X != bounds.Width ? true : false;
                bool height = dim.Y != bounds.Height ? true : false;

                if (width && height) Bounds = new Rectangle(bounds.X, bounds.Y, dim.X(), dim.Y() + yAdder);
                else if (width) Bounds = new Rectangle(bounds.X, bounds.Y, dim.X(), bounds.Height);
                else if (height) bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, dim.Y() + yAdder);
            }

            foregroundRectangle = bounds;
            backColorImage = backColorImage.ApplyBorderLabel(BorderStyle);
            if (backgroundImage != null) backgroundImage = backgroundImage.ApplyBorderLabel(BorderStyle);

            foregoundTexture = Drawing.FromText(HeaderText, font, foreColor, foregroundRectangle.Width, foregroundRectangle.Height, false, device);
            headerTexture = Drawing.FromColor(HeaderBackgroundColor, foregoundTexture.Width, font.MeasureString("a").Y(), device);

            itemTextures = new KeyValuePair<Texture2D, ButtonStyle>[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                itemTextures[i] = new KeyValuePair<Texture2D, ButtonStyle>(
                    Drawing.FromLabels(labels[i], font, foregoundTexture.Width, foregroundRectangle.Height / (labels.Length + 1), device),
                    ButtonStyle.Default);
            }
        }

        /// <summary>
        /// Draws the XnaMentula.GuiItems.Items.DropDown to the specified spritebatch.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (visible)
            {
                spriteBatch.Draw(headerTexture, foregroundRectangle.Position(), null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                spriteBatch.Draw(foregoundTexture, foregroundRectangle, null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0f);

                int fontHeight = font.MeasureString("a").Y();
                for (int i = 0; i < itemTextures.Length; i++)
                {
                    Texture2D texture = itemTextures[i].Value == ButtonStyle.Default ? itemTextures[i].Key : itemTextures[i].Key.ApplyBorderButton(itemTextures[i].Value);
                    spriteBatch.Draw(
                        texture,
                        new Vector2(Position.X, Position.Y + (fontHeight * (i + 1))),
                        null,
                        Color.White,
                        rotation,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0f);
                }
            }
        }

        private void Init(SpriteFont font)
        {
            HeaderText = "Choose Option";
            this.font = font;
            labels = new KeyValuePair<string, Color>[0][];
            itemTextures = new KeyValuePair<Texture2D, ButtonStyle>[0];
            foregroundRectangle = Extentions.FromPosition(Position, 100, 50);

            BackColor = Color.FromNonPremultiplied(42, 41, 27, 256);
            HeaderBackgroundColor = Color.FromNonPremultiplied(7, 8, 2, 256);
            ForeColor = Color.FromNonPremultiplied(198, 179, 148, 256);
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
            int relativeY = state.Y - Position.Y() - headerTexture.Height;
            int lineHeight = headerTexture.Height;

            if (relativeY < 0) return -1;
            int remain = relativeY % lineHeight;
            return relativeY / lineHeight;
        }
    }
}