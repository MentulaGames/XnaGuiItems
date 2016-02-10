using Mentula.GuiItems.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mentula.GuiItems.Items
{
    /// <summary>
    /// A label used for displaying text.
    /// </summary>
    public class Label : GuiItem
    {
        /// <summary>
        /// Gets or sets a value indicating if the label will adjust its size to the text.
        /// </summary>
        public virtual bool AutoSize { get; set; }
        /// <summary>
        /// Gets or sets the type of border given to the label.
        /// </summary>
        public virtual BorderStyle BorderStyle { get; set; }
        /// <summary>
        /// Gets or sets the text of the label. 
        /// </summary>
        public virtual string Text { get { return text; } set { TextChanged.DynamicInvoke(this, value); } }
        /// <summary>
        /// Gets or sets the font used by the label.
        /// </summary>
        public virtual SpriteFont Font { get { return font; } set { FontChanged.DynamicInvoke(this, value); } }
        /// <summary>
        /// Gets or sets the rectangle used to draw the text.
        /// </summary>
        public virtual Rectangle ForegroundRectangle { get { return foregroundRectangle; } set { foregroundRectangle = value; Refresh(); } }
        /// <summary>
        /// Gets or sets the size of the guiItem including its nonclient elements,
        /// in pixels, relative to the parent guiItem.
        /// </summary>
        public override Rectangle Bounds { get { return base.Bounds; } set { base.Bounds = value; ForegroundRectangle = value; } }
        /// <summary>
        /// Gets or sets a value indicating from what line the label should be shown.
        /// </summary>
        public int LineStart { get; set; }

        protected string text;
        protected SpriteFont font;
        protected Rectangle foregroundRectangle;

        /// <summary>
        /// Occurs when the value of the XnaGuiItem.Items.Label.Text propery is changed.
        /// </summary>
        public event TextChangedEventHandler TextChanged;
        /// <summary>
        /// Occurs when the value of the XnaGuiItem.Items.Label.Font propery is changed.
        /// </summary>
        public event ReFontEventHandler FontChanged;

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Items.Label class with default settings.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.Label to. </param>
        /// <param name="font"> The font to use while drawing the text. </param>
        public Label(GraphicsDevice device, SpriteFont font)
             : this(device, new Rectangle(0, 0, 100, 50), font)
        { }

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Items.Label class with a specific size.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.Label to. </param>
        /// <param name="bounds"> The size of the label in pixels. </param>
        /// <param name="font"> The font to use while drawing the text. </param>
        public Label(GraphicsDevice device, Rectangle bounds, SpriteFont font)
             : base(device, bounds)
        {
            InitEvents();
            foregroundRectangle = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            this.font = font;
            text = string.Empty;
            Refresh();
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public virtual void Refresh()
        {
            if (AutoSize)
            {
                Vector2 dim = font.MeasureString(text);
                dim.X += 3;
                bool width = dim.X != bounds.Width ? true : false;
                bool height = dim.Y != bounds.Height ? true : false;

                if (width && height) Bounds = new Rectangle(bounds.X, bounds.Y, dim.X(), dim.Y());
                else if (width) Bounds = new Rectangle(bounds.X, bounds.Y, dim.X(), bounds.Height);
                else if (height) bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, dim.Y());
            }

            foregroundRectangle = bounds;
            backColorImage = Drawing.FromColor(BackColor, bounds.Width, bounds.Height, device).ApplyBorderLabel(BorderStyle);
            if (backgroundImage != null) backgroundImage = backgroundImage.ApplyBorderLabel(BorderStyle);
            foregoundTexture = Drawing.FromText(text, font, foreColor, foregroundRectangle.Width, foregroundRectangle.Height, false, device);
        }

        /// <summary>
        /// Draws the XnaMentula.GuiItems.Items.Label to the specified spritebatch.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (visible)
            {
                int y = font.MeasureString("a").Y();
                Rectangle selection = new Rectangle(0, y * LineStart, foregroundRectangle.Width, foregroundRectangle.Height);

                spriteBatch.Draw(foregoundTexture, foregroundRectangle, selection, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        protected virtual void OnTextChanged(object sender, string newText) { text = newText; Refresh(); }
        protected virtual void OnFontChanged(object sender, SpriteFont newFont) { font = newFont; Refresh(); }
        protected override void OnForeColorChanged(object sender, Color newColor) { foreColor = newColor; if (font != null) Refresh(); }
        protected override void OnMove(object sender, Vector2 newpos)
        {
            base.OnMove(sender, newpos);
            foregroundRectangle.X = newpos.X();
            foregroundRectangle.Y = newpos.Y();
        }

        private void InitEvents()
        {
            TextChanged += OnTextChanged;
            FontChanged += OnFontChanged;
        }
    }
}