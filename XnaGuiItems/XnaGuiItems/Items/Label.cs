#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Items
{
#if MONO
    using Mono::Microsoft.Xna.Framework;
    using Mono::Microsoft.Xna.Framework.Graphics;
#else
    using Xna::Microsoft.Xna.Framework;
    using Xna::Microsoft.Xna.Framework.Graphics;
#endif
    using Core;
    using Core.EventHandlers;
    using Core.TextureHandlers;
    using Core.Structs;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using static Utilities;

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
        /// Gets or sets a value indicating if the <see cref="Label"/> will call <see cref="Refresh"/> on a <see cref="TextChanged"/> event.
        /// </summary>
        public virtual bool AutoRefresh { get; set; }
        /// <summary>
        /// Gets or sets the type of border given to the <see cref="Label"/>.
        /// </summary>
        public virtual BorderStyle BorderStyle { get; set; }
        /// <summary>
        /// Gets or sets the text of the <see cref="Label"/>. 
        /// </summary>
        public virtual string Text { get { return text; } set { Invoke(TextChanged, this, new ValueChangedEventArgs<string>(text, value)); } }
        /// <summary>
        /// Gets or sets the font used by the <see cref="Label"/>.
        /// </summary>
        public virtual SpriteFont Font { get { return font; } set { Invoke(FontChanged, this, new ValueChangedEventArgs<SpriteFont>(font, value)); } }
        /// <summary>
        /// Gets or sets the <see cref="Rectangle"/> used to draw the text.
        /// </summary>
        public virtual Rect ForegroundRectangle { get { return foregroundRectangle; } set { foregroundRectangle = value; } }
        /// <summary>
        /// Gets or sets a value indicating from what line the <see cref="Label"/> should be shown.
        /// </summary>
        public int LineStart { get { return lineStart; } set { lineStart = value; } }

        new private LabelTextureHandler textures { get { return (LabelTextureHandler)base.textures; } set { base.textures = value; } }

        /// <summary> The specified <see cref="SpriteFont"/>. </summary>
        protected SpriteFont font;
        /// <summary> The rectangle used in the foreground. </summary>
        protected Rect foregroundRectangle;

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
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public Label(ref SpriteBatch sb, SpriteFont font)
             : this(ref sb, DefaultBounds, font)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class with a specific size.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="bounds"> The size of the <see cref="Label"/> in pixels. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        [SuppressMessage(CAT_USAGE, CHECKID_CALL, Justification = JUST_VIRT_FINE)]
        public Label(ref SpriteBatch sb, Rect bounds, SpriteFont font)
             : base(ref sb, bounds)
        {
#if DEBUG
            type = LogMsgType.Ctor;
#endif

            ForegroundRectangle = bounds;
            this.font = font;
            text = string.Empty;

#if DEBUG
            type = LogMsgType.Call;
#endif
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            if (!suppressRefresh) HandleAutoSize();
            base.Refresh();
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
        /// Handles the <see cref="AutoSize"/> functionality.
        /// </summary>
        protected virtual void HandleAutoSize()
        {
            if (AutoSize)
            {
                Size dim = new Size(font.MeasureString(text));
                dim.Width += 3;

                if (dim.Width != Width || dim.Width != Height) Size = dim;
            }
        }

        /// <summary>
        /// Sets the background texture for the <see cref="Label"/>.
        /// </summary>
        protected override void SetBackgroundTexture()
        {
            textures.SetBackFromClr(BackColor, Size, batch.GraphicsDevice, BorderStyle);
        }

        /// <summary>
        /// Sets the foreground texture for the <see cref="Label"/>.
        /// </summary>
        protected override void SetForegroundTexture()
        {
            textures.SetText(text, font, ForeColor, ForegroundRectangle.Size, true, LineStart, batch);
        }

        /// <summary>
        /// This method is called when the <see cref="TextChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new text of the <see cref="Label"/>. </param>
        protected virtual void OnTextChanged(GuiItem sender, ValueChangedEventArgs<string> e)
        {
            text = e.NewValue;
            if (AutoRefresh) Refresh();
        }

        /// <summary>
        /// This method is called when the <see cref="FontChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new <see cref="SpriteFont"/>. </param>
        protected virtual void OnFontChanged(GuiItem sender, ValueChangedEventArgs<SpriteFont> e)
        {
            font = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="GuiItem.ForeColorChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new <see cref="Color"/> for the foreground. </param>
        protected override void OnForeColorChanged(GuiItem sender, ValueChangedEventArgs<Color> e)
        {
            base.OnForeColorChanged(sender, e);
        }

        /// <summary>
        /// This method is called when the <see cref="GuiItem.Moved"/> event is raised.
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
        /// This method is called when the <see cref="GuiItem.Resized"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new size of the <see cref="GuiItem"/>. </param>
        protected override void OnResize(GuiItem sender, ValueChangedEventArgs<Size> e)
        {
            base.OnResize(sender, e);
            foregroundRectangle.Size = e.NewValue;
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
            TextChanged += OnTextChanged;
            FontChanged += OnFontChanged;
        }
    }
}