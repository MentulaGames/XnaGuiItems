using Mentula.GuiItems.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Mentula.GuiItems.Utilities;

namespace Mentula.GuiItems.Items
{
    /// <summary>
    /// A progress bar used for displaying progress.
    /// </summary>
    public class ProgressBar : GuiItem
    {
        /// <summary>
        /// Gets or sets the direction of the bar.
        /// <see cref="Refresh"/> required after change!
        /// </summary>
        public virtual bool Inverted { get; set; }
        /// <summary>
        /// Gets or sets the type of border given to the <see cref="ProgressBar"/>.
        /// Refresh required after change!
        /// </summary>
        public virtual BorderStyle BorderStyle { get; set; }
        /// <summary>
        /// Gets the default size of the <see cref="ProgressBar"/>
        /// </summary>
        new public static Rectangle DefaultSize { get { return new Rectangle(0, 0, 100, 25); } }
        /// <summary>
        /// Gets the maximum value of the <see cref="ProgressBar"/>.
        /// </summary>
        public virtual int MaxiumValue { get { return data.Maximum; } }
        /// <summary>
        /// Gets the minimum value of the <see cref="ProgressBar"/>.
        /// </summary>
        public virtual int MinimumValue { get { return data.Minimum; } }
        /// <summary>
        /// Gets or sets the current value of the <see cref="ProgressBar"/>.
        /// </summary>
        public virtual int Value { get { return data.Value; } set { Invoke(ValueChanged, this, value); } }

        protected ProgressData data;

        /// <summary>
        /// Occurs when the value of the <see cref="Value"/> propery is changed.
        /// </summary>
        public event ValueChangedEventHandler<int> ValueChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class with default settings.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="ProgressBar"/> to. </param>
        public ProgressBar(GraphicsDevice device)
             : this(device, DefaultSize)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class with a specified size.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="ProgressBar"/> to. </param>
        /// <param name="bounds"> The size of the <see cref="ProgressBar"/> in pixels. </param>
        public ProgressBar(GraphicsDevice device, Rectangle bounds)
             : base(device, bounds)
        {
            InitEvents();

            data = new ProgressData(0);
            BorderStyle = BorderStyle.FixedSingle;
            ForeColor = Color.LimeGreen;

            Refresh();
        }

        /// <summary>
        /// Draws the <see cref="ProgressBar"/> and its childs to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The specified <see cref="SpriteBatch"/>. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (visible)
            {
                spriteBatch.Draw(foregoundTexture, Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            float ppp = (float)bounds.Width / 100;
            int width = (int)(ppp * data.Value);

            foregoundTexture = Drawing.FromColor(foreColor, bounds.Width, bounds.Height, Inverted ? new Rectangle(bounds.Width - width, 0, width, bounds.Height) : new Rectangle(0, 0, width, bounds.Height), device);
            backColorImage = Drawing.FromColor(backColor, bounds.Width, bounds.Height, device).ApplyBorderLabel(BorderStyle);
        }

        protected virtual void OnValueChanged(object sender, int newVal) { data.Value = newVal; Refresh(); }

        private void InitEvents()
        {
            ValueChanged += OnValueChanged;
        }
    }
}