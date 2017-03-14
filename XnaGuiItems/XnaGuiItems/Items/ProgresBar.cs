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
#else
    using Xna.Microsoft.Xna.Framework;
    using Xna.Microsoft.Xna.Framework.Graphics;
#endif
    using Core;
    using System.Diagnostics.CodeAnalysis;
    using static Utilities;
    using Args = Core.ValueChangedEventArgs<int>;
    using Core.Handlers;

    /// <summary>
    /// A progress bar used for displaying progress.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ProgressBar : GuiItem
    {
        /// <summary>
        /// Gets or sets the direction of the bar.
        /// <see cref="GuiItem.Refresh"/> required after change!
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
        new public static Rectangle DefaultBounds { get { return new Rectangle(0, 0, 100, 25); } }
        /// <summary>
        /// Gets the default foreground color of the <see cref="ProgressBar"/>.
        /// </summary>
        new public static Color DefaultForeColor { get { return Color.LimeGreen; } }
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
        public virtual int Value { get { return data.Value; } set { Invoke(ValueChanged, this, new Args(data.Value, value)); } }

        /// <summary> The underlying <see cref="ProgressData"/>. </summary>
        protected ProgressData data;

        new private LabelTextureHandler textures { get { return (LabelTextureHandler)base.textures; } set { base.textures = value; } }

        /// <summary>
        /// Occurs when the value of the <see cref="Value"/> propery is changed.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<int> ValueChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class with default settings.
        /// </summary>
        /// <param name="sb"> The <see cref="GraphicsDevice"/> to display the <see cref="ProgressBar"/> to. </param>
        public ProgressBar(ref SpriteBatch sb)
             : this(ref sb, DefaultBounds)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class with a specified size.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="bounds"> The size of the <see cref="ProgressBar"/> in pixels. </param>
        [SuppressMessage(CAT_USAGE, CHECKID_CALL, Justification = JUST_VIRT_FINE)]
        public ProgressBar(ref SpriteBatch sb, Rectangle bounds)
             : base(ref sb, bounds)
        {
            data = new ProgressData(0);
            BorderStyle = BorderStyle.FixedSingle;
            ForeColor = DefaultForeColor;
        }

        /// <summary>
        /// This method is called when the <see cref="ValueChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="args"> The new value of the <see cref="ProgressBar"/>. </param>
        protected virtual void OnValueChanged(GuiItem sender, Args args)
        {
            data.Value = args.NewValue;
        }

        /// <summary>
        /// Sets the background texture for the <see cref="ProgressBar"/>.
        /// </summary>
        protected override void SetBackgroundTexture()
        {
            textures.SetBackFromClr(BackColor, Size, batch.GraphicsDevice, BorderStyle);
        }

        /// <summary>
        /// Sets the foreground texture for the <see cref="ProgressBar"/>.
        /// </summary>
        protected override void SetForegroundTexture()
        {
            int width = (int)(Bounds.Width / 100.0f * data.Value);
            Rectangle destination = Inverted ? new Rectangle(Width - width - 1, 1, width - 1, Height - 2) : new Rectangle(1, 1, width - 2, Height - 2);

            textures.SetForeFromClr(ForeColor, Size, destination, batch.GraphicsDevice);
        }

        /// <summary>
        /// Sets <see cref="GuiItem.textures"/> to the required <see cref="TextureHandler"/>.
        /// </summary>
        protected override void SetTextureHandler()
        {
            textures = new LabelTextureHandler();
        }

        /// <summary>
        /// Handles the initializing of the events.
        /// </summary>
        protected override void InitEvents()
        {
            base.InitEvents();
            ValueChanged += OnValueChanged;
        }
    }
}