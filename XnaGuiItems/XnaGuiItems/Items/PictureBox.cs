namespace Mentula.GuiItems.Items
{
    using Core;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using static Utilities;
    using Args = Core.ValueChangedEventArgs<Core.ResizeMode>;

    /// <summary>
    /// A Image displayer base class.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class PictureBox : GuiItem
    {
        /// <summary>
        /// Indicates how the image is displayed.
        /// </summary>
        public virtual ResizeMode SizeMode { get { return sizeMode; } set { Invoke(SizeModeChanged, this, new Args(sizeMode, value)); } }
        /// <summary>
        /// Gets or sets the image that is displayed by the <see cref="PictureBox"/>.
        /// </summary>
        public virtual Texture2D Image { get; set; }

        /// <summary>
        /// Occurs when the value of the <see cref="SizeMode"/> propery is changed.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<ResizeMode> SizeModeChanged;

        private ResizeMode sizeMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureBox"/> class with default settings.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="PictureBox"/> to. </param>
        public PictureBox(GraphicsDevice device)
            : this(device, DefaultBounds)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureBox"/> class with specific size.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="PictureBox"/> to. </param>
        /// <param name="bounds"> The size of the <see cref="PictureBox"/> in pixels. </param>
        public PictureBox(GraphicsDevice device, Rectangle bounds)
            : base(device, bounds)
        {
            SizeModeChanged += OnSizeModeChanged;
        }

        /// <summary>
        /// Recalculates the foreground.
        /// </summary>
        public override void Refresh()
        {
            if (suppressRefresh) return;

            switch (SizeMode)
            {
                case ResizeMode.AutoSize:
                    bool width = Image.Width != Bounds.Width;
                    bool height = Image.Height != Bounds.Height;

                    if (width || height) Bounds = new Rectangle(Bounds.X, Bounds.Y, Image.Width, Image.Height);
                    foregroundTexture = Image;
                    break;
                case ResizeMode.CenterImage:
                    Size offset = (Size >> 1) - (Image.Bounds.Size() >> 1);
                    foregroundTexture = Image.RenderOnto(Size, position: offset.ToVector2());
                    break;
                case ResizeMode.Normal:
                    foregroundTexture = Image.Clip(new Rectangle(0, 0, Width, Height));
                    break;
                case ResizeMode.StretchImage:
                    foregroundTexture = Image.Stretch(Size);
                    break;
                case ResizeMode.Zoom:
                    Vector2 zoom = new Vector2(Width / (float)Image.Width, Height / (float)Image.Height);
                    foregroundTexture = Image.RenderOnto(Size, scale: new Vector2(Math.Min(zoom.X, zoom.Y)));
                    break;
            }
        }

        /// <summary>
        /// Draws the <see cref="PictureBox"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The spritebatch to use. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible)
            {
                spriteBatch.Draw(foregroundTexture, Position, null, BackColor, Rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
            }
        }

        /// <summary>
        /// This method is called when the <see cref="SizeModeChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new <see cref="ResizeMode"/> for the foreground. </param>
        protected virtual void OnSizeModeChanged(GuiItem sender, Args e)
        {
            sizeMode = e.NewValue;
        }
    }
}