﻿using Mentula.GuiItems.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static Mentula.GuiItems.Utilities;

namespace Mentula.GuiItems.Items
{
    /// <summary>
    /// A label used for displaying text.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Label : GuiItem
    {
        /// <summary>
        /// Gets or sets a value indicating if the <see cref="Label"/> will adjust its size to the text.
        /// </summary>
        public virtual bool AutoSize { get; set; }
        /// <summary>
        /// Gets or sets the type of border given to the <see cref="Label"/>.
        /// </summary>
        public virtual BorderStyle BorderStyle { get; set; }
        /// <summary>
        /// Gets or sets the text of the <see cref="Label"/>. 
        /// </summary>
        public virtual string Text { get { return text; } set { TextChanged.Invoke(this, new ValueChangedEventArgs<string>(text, value)); } }
        /// <summary>
        /// Gets or sets the font used by the <see cref="Label"/>.
        /// </summary>
        public virtual SpriteFont Font { get { return font; } set { FontChanged.Invoke(this, new ValueChangedEventArgs<SpriteFont>(font, value)); } }
        /// <summary>
        /// Gets or sets the <see cref="Rectangle"/> used to draw the text.
        /// </summary>
        public virtual Rectangle ForegroundRectangle { get { return foregroundRectangle; } set { foregroundRectangle = value; Refresh(); } }
        /// <summary>
        /// Gets or sets the size of the <see cref="GuiItem"/> including its nonclient elements in pixels.
        /// </summary>
        public override Rectangle Bounds { get { return base.Bounds; } set { base.Bounds = value; ForegroundRectangle = value; } }
        /// <summary>
        /// Gets or sets a value indicating from what line the <see cref="Label"/> should be shown.
        /// </summary>
        public int LineStart { get { return lineStart; } set { lineStart = value; Refresh(); } }

        /// <summary> The specified <see cref="SpriteFont"/>. </summary>
        protected SpriteFont font;
        /// <summary> The rectangle used in the foreground. </summary>
        protected Rectangle foregroundRectangle;

        private int lineStart;
        private string text;

        /// <summary>
        /// Occurs when the value of the <see cref="Text"/> propery is changed.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<string> TextChanged;
        /// <summary>
        /// Occurs when the value of the <see cref="Font"/> propery is changed.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<SpriteFont> FontChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class with default settings.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="Label"/> to. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public Label(GraphicsDevice device, SpriteFont font)
             : this(device, DefaultBounds, font)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class with a specific size.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="Label"/> to. </param>
        /// <param name="bounds"> The size of the <see cref="Label"/> in pixels. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public Label(GraphicsDevice device, Rectangle bounds, SpriteFont font)
             : base(device, bounds)
        {
            InitEvents();
            foregroundRectangle = bounds;
            this.font = font;
            text = string.Empty;
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            if (Utilities.suppressRefresh) return;

            if (AutoSize)
            {
                Vector2 dim = font.MeasureString(text);
                dim.X += 3;
                bool width = dim.X != Bounds.Width && dim.X != 0;
                bool height = dim.Y != Bounds.Height && dim.Y != 0;

                if (width && height) Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)dim.X, (int)dim.Y);
                else if (width) Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)dim.X, Bounds.Height);
                else if (height) Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, (int)dim.Y);
            }

            foregroundRectangle = Bounds;
            backColorImage = Drawing.FromColor(BackColor, Bounds.Size(), device).ApplyBorderLabel(BorderStyle);
            if (BackgroundImage != null) BackgroundImage = BackgroundImage.ApplyBorderLabel(BorderStyle);
            foregroundTexture = Drawing.FromText(text, font, ForeColor, foregroundRectangle.Size(), true, LineStart, device);
        }

        /// <summary>
        /// Draws the <see cref="Label"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The specified <see cref="SpriteBatch"/>. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible)
            {
                spriteBatch.Draw(foregroundTexture, foregroundRectangle, null, Color.White, Rotation, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Get the number of lines in this <see cref="Label"/>
        /// </summary>
        /// <returns> The amount of lines in the <see cref="Label"/>. </returns>
        public int GetLineCount()
        {
            return Text.Count(c => c == '\n') + 1;
        }

        /// <summary>
        /// This method is called when the <see cref="TextChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new text of the <see cref="Label"/>. </param>
        protected virtual void OnTextChanged(GuiItem sender, ValueChangedEventArgs<string> e)
        {
            text = e.NewValue;
            Refresh();
        }

        /// <summary>
        /// This method is called when the <see cref="FontChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new <see cref="SpriteFont"/>. </param>
        protected virtual void OnFontChanged(GuiItem sender, ValueChangedEventArgs<SpriteFont> e)
        {
            font = e.NewValue;
            Refresh();
        }

        /// <summary>
        /// This method is called when the <see cref="GuiItem.ForeColorChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new <see cref="Color"/> for the foreground. </param>
        protected override void OnForeColorChanged(GuiItem sender, ValueChangedEventArgs<Color> e)
        {
            base.OnForeColorChanged(sender, e);
            if (font != null) Refresh();
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

        private void InitEvents()
        {
            TextChanged += OnTextChanged;
            FontChanged += OnFontChanged;
        }
    }
}