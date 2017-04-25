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
    using Mono::Microsoft.Xna.Framework.Input;
#else
    using Xna::Microsoft.Xna.Framework;
    using Xna::Microsoft.Xna.Framework.Graphics;
    using Xna::Microsoft.Xna.Framework.Input;
#endif
    using Core;
    using Core.EventHandlers;
    using Core.TextureHandlers;
    using System.Diagnostics.CodeAnalysis;
    using DeJong.Utilities.Core;
    using static Utilities;
    using static DeJong.Utilities.Core.EventInvoker;
    using Args = Core.EventHandlers.ValueChangedEventArgs<int>;

    /// <summary>
    /// A slider used for setting rough values like volume.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Slider : GuiItem
    {
        /// <summary>
        /// Gets or sets the type of border given to the <see cref="Slider"/>.
        /// <see cref="GuiItem.Refresh"/> required after change!
        /// </summary>
        public virtual BorderStyle BorderStyle { get; set; }
        /// <summary>
        /// Gets the default size of the <see cref="Slider"/>
        /// </summary>
        new public static Rect DefaultBounds { get { return new Rect(0, 0, 100, 25); } }
        /// <summary>
        /// Gets the default foreground color of the <see cref="Slider"/>.
        /// </summary>
        new public static Color DefaultForeColor { get { return Color.Gray; } }
        /// <summary>
        /// Gets or sets the dimentions of the <see cref="Slider"/> object.
        /// </summary>
        public virtual Rect SliderBarDimentions { get; set; }
        /// <summary>
        /// Gets or sets the maximum value of the <see cref="Slider"/>.
        /// </summary>
        public virtual int MaximumValue { get { return data.Maximum; } set { data.Maximum = value; } }
        /// <summary>
        /// Gets or sets the minimum value of the <see cref="Slider"/>.
        /// </summary>
        public virtual int MinimumValue { get { return data.Minimum; } set { data.Minimum = value; } }
        /// <summary>
        /// Gets or sets the current value of the <see cref="Slider"/>.
        /// Will not change the visuals of the slider!
        /// </summary>
        public virtual int Value { get { return data.Value; } set { Invoke(ValueChanged, this, new Args(data.Value, value)); } }

        /// <summary> The underlying <see cref="ProgressData"/>. </summary>
        protected ProgressData data;

        new private LabelTextureHandler textures { get { return (LabelTextureHandler)base.textures; } set { base.textures = value; } }

        private bool sliding;
        private Vector2 oldOffset;

        /// <summary>
        /// Occurs when the value of the <see cref="Value"/> propery is changed.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event StrongEventHandler<Slider, ValueChangedEventArgs<int>> ValueChanged;
        /// <summary>
        /// Occurs when the mouse is pressed on the <see cref="Slider"/>.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event StrongEventHandler<Slider, MouseEventArgs> MouseDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class with default settings.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        public Slider(ref SpriteBatch sb)
             : this(ref sb, DefaultBounds)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class with a specified size.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="bounds"> The size of the <see cref="Slider"/>. </param>
        [SuppressMessage(CAT_USAGE, CHECKID_CALL, Justification = JUST_VIRT_FINE)]
        public Slider(ref SpriteBatch sb, Rect bounds)
             : base(ref sb, bounds)
        {
            SliderBarDimentions = new Rect(bounds.X, bounds.Y, Width / 10, Height);
            data = new ProgressData(0);
            BorderStyle = BorderStyle.FixedSingle;
            ForeColor = DefaultForeColor;
        }

        /// <summary>
        /// Updates the <see cref="Slider"/>, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="deltaTime"> The specified deltatime. </param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Enabled)
            {
                MouseState mState = Mouse.GetState();

                if (sliding && leftDown)
                {
                    Invoke(MouseDown, this, GetMouseEventArgs());
                }
                else if (sliding && !leftDown && !IsSliding(mState.Position()))
                {
                    sliding = false;
                    oldOffset = Vector2.Zero;
                }
            }
        }

        /// <summary>
        /// Draws the <see cref="Slider"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The spritebatch to use. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(textures.Background, Position, null, textures.userset_background ? BackColor : Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, 1f);
                spriteBatch.Draw(textures.Foreground, Position + SliderBarDimentions.Position, null, Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// This method is called when the <see cref="GuiItem.Clicked"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The current <see cref="MouseState"/>. </param>
        protected void OnClick(object sender, MouseEventArgs e)
        {
            if (IsSliding(GetRotatedMouse(e.State))) sliding = true;
        }

        /// <summary>
        /// This method is called when the <see cref="ValueChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="Slider"/> that raised the event. </param>
        /// <param name="e"> The new value of the slider. </param>
        protected void OnValueChanged(Slider sender, Args e)
        {
            data.Value = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="MouseDown"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="Slider"/> that raised the event. </param>
        /// <param name="e"> The current <see cref="MouseState"/>. </param>
        protected void OnMouseDown(Slider sender, MouseEventArgs e)
        {
            Vector2 offset = SliderBarDimentions.Position + Position - GetRotatedMouse(e.State);
            if (oldOffset == Vector2.Zero) oldOffset = offset;
            else if (offset != oldOffset)
            {
                Rect newDim = SliderBarDimentions;
                newDim.Position += new Vector2(-offset.X - (SliderBarDimentions.Width >> 1), 0);

                if (newDim.X + Position.X > Bounds.X && newDim.X + SliderBarDimentions.Width <= Bounds.Width) SliderBarDimentions = newDim;
                else if (newDim.X + Position.X > Bounds.X) SliderBarDimentions = new Rect(Bounds.Width - SliderBarDimentions.Width, 0, SliderBarDimentions.Width, SliderBarDimentions.Height);
                else SliderBarDimentions = new Rect(0, 0, SliderBarDimentions.Width, SliderBarDimentions.Height);

                oldOffset = offset;

                float ppp = 100f / Bounds.Width;
                bool overCenter = SliderBarDimentions.X * ppp >= 50;
                float percent = (SliderBarDimentions.X + (overCenter ? SliderBarDimentions.Width : 0)) * ppp;

                int old = Value;
                data.ChangeValue((int)percent);
                if (Value != old) Invoke(ValueChanged, this, new Args(old, Value));
            }
        }

        /// <summary>
        /// Sets the background texture for the <see cref="Slider"/>.
        /// </summary>
        protected override void SetBackgroundTexture()
        {
            textures.SetBackFromClr(BackColor, Size, batch.GraphicsDevice, BorderStyle);
        }

        /// <summary>
        /// Sets the foreground texture for the <see cref="Slider"/>.
        /// </summary>
        protected override void SetForegroundTexture()
        {
            textures.SetForeFromClr(ForeColor, Size, SliderBarDimentions, batch.GraphicsDevice);
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
            Clicked += OnClick;
            MouseDown += OnMouseDown;
        }

        private bool IsSliding(Vector2 mousePosition)
        {
            Rect absBarPos = SliderBarDimentions;
            absBarPos.Position += Position;
            return absBarPos.Contains(mousePosition);
        }
    }
}