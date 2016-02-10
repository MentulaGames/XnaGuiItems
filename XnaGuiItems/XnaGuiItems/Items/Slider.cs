using Mentula.GuiItems.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mentula.GuiItems.Items
{
    /// <summary>
    /// A slider used for setting rough values like volume.
    /// </summary>
    public class Slider : GuiItem
    {
        /// <summary>
        /// Gets or sets the type of border given to the slider.
        /// Refresh required after change!
        /// </summary>
        public virtual BorderStyle BorderStyle { get; set; }
        /// <summary>
        /// Gets or sets the dimentions of the slider object.
        /// </summary>
        public virtual Rectangle SlidBarDimentions { get; set; }
        /// <summary>
        /// Gets or sets the maximum value of the slider.
        /// </summary>
        public virtual int MaximumValue { get { return data.Maximum; } set { data.Maximum = value; } }
        /// <summary>
        /// Gets or sets the minimum value of the slider.
        /// </summary>
        public virtual int MinimumValue { get { return data.Minimum; } set { data.Minimum = value; } }
        /// <summary>
        /// Gets or sets the current value of the slider.
        /// Will not change the visuals of the slider!
        /// </summary>
        public virtual int Value { get { return data.Value; } set { ValueChanged.DynamicInvoke(this, value); } }

        protected ProgresData data;
        private bool over;
        private Vector2 oldOffset;

        /// <summary>
        /// Occurs when the value of the XnaGuiItem.Items.Slider.Value propery is changed.
        /// </summary>
        public event ValueChangedEventHandler<int> ValueChanged;
        /// <summary>
        /// occurs when the mouse is pressed on the sliderbar.
        /// </summary>
        public event MouseEventHandler MouseDown;

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Items.Slider class with default settings.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.Slider to. </param>
        public Slider(GraphicsDevice device)
             : base(device)
        {
            InitEvents();

            Bounds = new Rectangle(0, 0, 100, 25);
            SlidBarDimentions = new Rectangle(0, 0, 10, 25);
            data = new ProgresData(0);
            BorderStyle = BorderStyle.FixedSingle;
            foreColor = Color.Gray;

            Refresh();
        }

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Items.SLider class with a specified size.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.Slider to. </param>
        /// <param name="bounds"> The size of the XnaMentula.GuiItems.Items.Slider. </param>
        public Slider(GraphicsDevice device, Rectangle bounds)
             : base(device, bounds)
        {
            InitEvents();

            SlidBarDimentions = new Rectangle(0, 0, Bounds.Width / 10, Bounds.Height);
            data = new ProgresData(0);
            BorderStyle = Core.BorderStyle.FixedSingle;
            foreColor = Color.Gray;

            Refresh();
        }

        /// <summary>
        /// Updates the XnaMentula.GuiItems.Items.Slider, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="state"> The current state of the mouse. </param>
        public override void Update(MouseState state)
        {
            base.Update(state);

            if (Enabled)
            {
                if (over && state.LeftButton == ButtonState.Pressed)
                {
                    MouseDown.DynamicInvoke(this, state);
                    Refresh();
                }
                else if (over && state.LeftButton == ButtonState.Released && !IsSliding(state.Position()))
                {
                    over = false;
                    oldOffset = Vector2.Zero;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Draws the XnaMentula.GuiItems.Items.Slider to the specified spritebatch.
        /// </summary>
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
        public void Refresh()
        {
            foregoundTexture = Drawing.FromColor(ForeColor, bounds.Width, bounds.Height, SlidBarDimentions, device);
            backColorImage = Drawing.FromColor(backColor, bounds.Width, bounds.Height, device).ApplyBorderLabel(BorderStyle);
        }

        protected void OnClick(object sender, MouseState state) { if (IsSliding(state.Position())) over = true; }
        protected void OnValueChanged(object sender, int newValue) { data.Value = newValue; }
        protected void OnMouseDown(object sender, MouseState state)
        {
            Vector2 offset = SlidBarDimentions.Position() + Position - state.Position();
            if (oldOffset == Vector2.Zero) oldOffset = offset;
            else if (offset != oldOffset)
            {
                Rectangle newDim = SlidBarDimentions.Add(new Vector2(-offset.X - (SlidBarDimentions.Width >> 1), 0));

                if (newDim.X + Position.X > bounds.X && newDim.X + SlidBarDimentions.Width <= bounds.Width) SlidBarDimentions = newDim;
                else if (newDim.X + Position.X > bounds.X) SlidBarDimentions = new Rectangle(bounds.Width - SlidBarDimentions.Width, 0, SlidBarDimentions.Width, SlidBarDimentions.Height);
                else SlidBarDimentions = new Rectangle(0, 0, SlidBarDimentions.Width, SlidBarDimentions.Height);

                oldOffset = offset;

                float ppp = (float)(bounds.Width + SlidBarDimentions.Width) / 100;
                float percent = SlidBarDimentions.X * ppp;
                data.ChangeValue(data.Value >= (data.Maximum - ppp) && percent >= 99 ? 100 : (int)percent);
            }
        }

        private void InitEvents()
        {
            ValueChanged += OnValueChanged;
            Click += OnClick;
            MouseDown += OnMouseDown;
        }

        private bool IsSliding(Vector2 mousePosition)
        {
            if (SlidBarDimentions.Add(Position).Contains(mousePosition.ToPoint())) return true;
            return false;
        }
    }
}