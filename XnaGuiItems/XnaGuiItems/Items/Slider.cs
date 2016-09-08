using Mentula.GuiItems.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Mentula.GuiItems.Utilities;

namespace Mentula.GuiItems.Items
{
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
        /// <see cref="Refresh"/> required after change!
        /// </summary>
        public virtual BorderStyle BorderStyle { get; set; }
        /// <summary>
        /// Gets the default size of the <see cref="Slider"/>
        /// </summary>
        new public static Rectangle DefaultBounds { get { return new Rectangle(0, 0, 100, 25); } }
        /// <summary>
        /// Gets or sets the dimentions of the <see cref="Slider"/> object.
        /// </summary>
        public virtual Rectangle SliderBarDimentions { get; set; }
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
        public virtual int Value { get { return data.Value; } set { Invoke(ValueChanged, this, value); } }

        /// <summary> The underlying <see cref="ProgressData"/>. </summary>
        protected ProgressData data;

        private bool sliding;
        private Vector2 oldOffset;

        /// <summary>
        /// Occurs when the value of the <see cref="Value"/> propery is changed.
        /// </summary>
        public event ValueChangedEventHandler<int> ValueChanged;
        /// <summary>
        /// Occurs when the mouse is pressed on the <see cref="Slider"/>.
        /// </summary>
        public event MouseEventHandler MouseDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class with default settings.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="Slider"/> to. </param>
        public Slider(GraphicsDevice device)
             : this(device, DefaultBounds)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class with a specified size.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="Slider"/> to. </param>
        /// <param name="bounds"> The size of the <see cref="Slider"/>. </param>
        public Slider(GraphicsDevice device, Rectangle bounds)
             : base(device, bounds)
        {
            InitEvents();

            SliderBarDimentions = new Rectangle(bounds.X, bounds.Y, Bounds.Width / 10, Bounds.Height);
            data = new ProgressData(0);
            BorderStyle = BorderStyle.FixedSingle;
            ForeColor = Color.Gray;
        }

        /// <summary>
        /// Updates the <see cref="Slider"/>, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="state"> The current state of the <see cref="Mouse"/>. </param>
        public override void Update(MouseState state)
        {
            base.Update(state);

            if (Enabled)
            {
                if (sliding && leftDown)
                {
                    Invoke(MouseDown, this, state);
                    Refresh();
                }
                else if (sliding && !leftDown && !IsSliding(state.Position()))
                {
                    sliding = false;
                    oldOffset = Vector2.Zero;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Draws the <see cref="Slider"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The specified <see cref="SpriteBatch"/>. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible)
            {
                spriteBatch.Draw(foregoundTexture, Position, null, Color.White, Rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            foregoundTexture = Drawing.FromColor(ForeColor, Bounds.Size(), SliderBarDimentions, device);
            backColorImage = Drawing.FromColor(BackColor, Bounds.Size(), device).ApplyBorderLabel(BorderStyle);
        }

        /// <summary>
        /// This method is called when the <see cref="GuiItem.Click"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="state"> The current <see cref="MouseState"/>. </param>
        protected void OnClick(object sender, MouseState state)
        {
            if (IsSliding(GetRotatedMouse(state))) sliding = true;
        }

        /// <summary>
        /// This method is called when the <see cref="ValueChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="newValue"> The new value of the slider. </param>
        protected void OnValueChanged(object sender, int newValue)
        {
            data.Value = newValue;
        }

        /// <summary>
        /// This method is called when the <see cref="MouseDown"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="state"> The current <see cref="MouseState"/>. </param>
        protected void OnMouseDown(object sender, MouseState state)
        {
            Vector2 offset = SliderBarDimentions.Position() + Position - GetRotatedMouse(state);
            if (oldOffset == Vector2.Zero) oldOffset = offset;
            else if (offset != oldOffset)
            {
                Rectangle newDim = SliderBarDimentions.Add(new Vector2(-offset.X - (SliderBarDimentions.Width >> 1), 0));

                if (newDim.X + Position.X > Bounds.X && newDim.X + SliderBarDimentions.Width <= Bounds.Width) SliderBarDimentions = newDim;
                else if (newDim.X + Position.X > Bounds.X) SliderBarDimentions = new Rectangle(Bounds.Width - SliderBarDimentions.Width, 0, SliderBarDimentions.Width, SliderBarDimentions.Height);
                else SliderBarDimentions = new Rectangle(0, 0, SliderBarDimentions.Width, SliderBarDimentions.Height);

                oldOffset = offset;

                float ppp = 100f / Bounds.Width;
                bool overCenter = SliderBarDimentions.X * ppp >= 50;
                float percent = (SliderBarDimentions.X + (overCenter ? SliderBarDimentions.Width : 0)) * ppp;

                int old = Value;
                data.ChangeValue((int)percent);
                if (ValueChanged != null && Value != old) ValueChanged.Invoke(this, Value);
            }
        }

        private void InitEvents()
        {
            ValueChanged += OnValueChanged;
            Click += OnClick;
            MouseDown += OnMouseDown;
            Resize += (sender, args) => Refresh();
        }

        private bool IsSliding(Vector2 mousePosition)
        {
            return SliderBarDimentions.Add(Position).Contains(mousePosition.ToPoint());
        }
    }
}