﻿#if MONO
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
    using System.Diagnostics.CodeAnalysis;
    using static Utilities;
    using Args = Core.ValueChangedEventArgs<int>;

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
        public virtual int Value { get { return data.Value; } set { Invoke(ValueChanged, this, new Args(data.Value, value)); } }

        /// <summary> The underlying <see cref="ProgressData"/>. </summary>
        protected ProgressData data;

        private bool sliding;
        private Vector2 oldOffset;

        /// <summary>
        /// Occurs when the value of the <see cref="Value"/> propery is changed.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<int> ValueChanged;
        /// <summary>
        /// Occurs when the mouse is pressed on the <see cref="Slider"/>.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event MouseEventHandler MouseDown;

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
        public Slider(ref SpriteBatch sb, Rectangle bounds)
             : base(ref sb, bounds)
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
                    Invoke(MouseDown, this, GetMouseEventArgs());
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
        /// Recalculates the background and the foreground.
        /// </summary>
        public override void Refresh()
        {
            if (suppressRefresh) return;

            textures.Foreground = Drawing.FromColor(ForeColor, Size, SliderBarDimentions, batch.GraphicsDevice);
            textures.Background = Drawing.FromColor(BackColor, Size, batch.GraphicsDevice).ApplyBorderLabel(BorderStyle);
        }

        /// <summary>
        /// This method is called when the <see cref="GuiItem.Click"/> event is raised.
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
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new value of the slider. </param>
        protected void OnValueChanged(object sender, Args e)
        {
            data.Value = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="MouseDown"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The current <see cref="MouseState"/>. </param>
        protected void OnMouseDown(object sender, MouseEventArgs e)
        {
            Vector2 offset = SliderBarDimentions.Position() + Position - GetRotatedMouse(e.State);
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
                if (Value != old) ValueChanged.Invoke(this, new Args(old, Value));
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