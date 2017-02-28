﻿namespace Mentula.GuiItems.Items
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
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        public PictureBox(ref SpriteBatch sb)
            : this(ref sb, DefaultBounds)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureBox"/> class with specific size.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="bounds"> The size of the <see cref="PictureBox"/> in pixels. </param>
        public PictureBox(ref SpriteBatch sb, Rectangle bounds)
            : base(ref sb, bounds)
        {
            SizeModeChanged += OnSizeModeChanged;
        }

        /// <summary>
        /// Recalculates the foreground.
        /// </summary>
        public override void Refresh()
        {
            if (suppressRefresh) return;

            if (Image == null)
            {
                textures.SetForeFromClr(Color.Transparent, Size, batch.GraphicsDevice);
                return;
            }

            switch (SizeMode)
            {
                case ResizeMode.AutoSize:
                    if (Image.Width != Bounds.Width || Image.Height != Bounds.Height) Size = new Size(Image.Width, Image.Height);
                    textures.Foreground = Image;
                    break;
                case ResizeMode.CenterImage:
                    Size offset = (Size >> 1) - (Image.Bounds.Size() >> 1);
                    textures.Foreground = Image.RenderOnto(batch, Size, position: offset.ToVector2());
                    break;
                case ResizeMode.Normal:
                    textures.Foreground = Image.Clip(new Rectangle(0, 0, Width, Height));
                    break;
                case ResizeMode.StretchImage:
                    textures.Foreground = Image.Stretch(Size);
                    break;
                case ResizeMode.Zoom:
                    Vector2 zoom = new Vector2(Width / (float)Image.Width, Height / (float)Image.Height);
                    textures.Foreground = Image.RenderOnto(batch, Size, scale: new Vector2(Math.Min(zoom.X, zoom.Y)));
                    break;
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