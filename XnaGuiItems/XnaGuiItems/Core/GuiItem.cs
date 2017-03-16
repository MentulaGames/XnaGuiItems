#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Core
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
    using TextureHandlers;
    using EventHandlers;
    using Interfaces;
    using Structs;
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using static Utilities;

    /// <summary>
    /// The absolute base class for all <see cref="GuiItems"/>.
    /// </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("{ToString()}")]
    public partial class GuiItem : IDisposable, IToggleable
    {
        /// <summary>
        /// Gets or sets the background color for the <see cref="GuiItem"/>.
        /// </summary>
        public virtual Color BackColor { get { return backColor; } set { Invoke(BackColorChanged, this, new ValueChangedEventArgs<Color>(backColor, value)); } }
        /// <summary>
        /// Gets or sets the background image displayed in the <see cref="GuiItem"/>.
        /// </summary>
        public virtual Texture2D BackgroundImage { get { return textures.Background; } set { Invoke(BackgroundImageChanged, this, new ValueChangedEventArgs<Texture2D>(textures.Background, value)); } }
        /// <summary>
        /// Gets or sets the size of the <see cref="GuiItem"/> including its nonclient elements in pixels.
        /// </summary>
        public virtual Rect Bounds { get { return new Rect(Position, Size); } set { Size = value.Size; Position = value.Position; } }
        /// <summary>
        /// Gets the default background color of the <see cref="GuiItem"/>.
        /// </summary>
        public static Color DefaultBackColor { get { return Color.WhiteSmoke; } }
        /// <summary>
        /// Gets the default foreground color of the <see cref="GuiItem"/>.
        /// </summary>
        public static Color DefaultForeColor { get { return Color.Black; } }
        /// <summary>
        /// Gets the default size of the <see cref="GuiItem"/>
        /// </summary>
        public static Rect DefaultBounds { get { return new Rect(0, 0, 100, 50); } }
        /// <summary>
        /// Gets a value indicating wether the base <see cref="GuiItem"/> class is in the process of disposing.
        /// </summary>
        public virtual bool Disposing { get; protected set; }
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="GuiItem"/> can respond to user interaction.
        /// </summary>
        public virtual bool Enabled { get { return enabled; } set { Invoke(EnabledChanged, this, new ValueChangedEventArgs<bool>(Enabled, value)); } }
        /// <summary>
        /// Gets or sets the foregound color for the <see cref="GuiItem"/>.
        /// </summary>
        public virtual Color ForeColor { get { return foreColor; } set { Invoke(ForeColorChanged, this, new ValueChangedEventArgs<Color>(foreColor, value)); } }
        /// <summary>
        /// Gets or sets the height of the <see cref="GuiItem"/> including its nonclient elements in pixels.
        /// </summary>
        public virtual int Height { get { return Size.Height; } set { Size = new Size(Width, value); } }
        /// <summary>
        /// Gets a value indicating whether the <see cref="GuiItem"/> has been disposed.
        /// </summary>
        public virtual bool IsDisposed { get; protected set; }
        /// <summary>
        /// Gets or sets the name of the <see cref="GuiItem"/>.
        /// </summary>
        public virtual string Name { get { return name; } set { Invoke(NameChanged, this, new ValueChangedEventArgs<string>(name, value)); } }
        /// <summary>
        /// Gets or sets a value indicating the origin of rotation.
        /// </summary>
        public virtual Vector2 Origin { get; set; }
        /// <summary>
        /// Gets or sets the client side rotation of the <see cref="GuiItem"/>.
        /// </summary>
        public virtual float Rotation { get { return rotation; } set { Invoke(Rotated, this, new ValueChangedEventArgs<float>(rotation, value)); } }
        /// <summary>
        /// Gets or sets the position of the <see cref="GuiItem"/>.
        /// </summary>
        public virtual Vector2 Position { get { return position; } set { Invoke(Moved, this, new ValueChangedEventArgs<Vector2>(Position, value)); } }
        /// <summary>
        /// Gets or sets the size of the <see cref="GuiItem"/>.
        /// </summary>
        public virtual Size Size { get { return size; } set { Invoke(Resized, this, new ValueChangedEventArgs<Size>(Size, value)); } }
        /// <summary>
        /// Gets or sets a value indicating whether a <see cref="Containers.Menu{T}"/> should call the <see cref="Update(float)"/> method.
        /// </summary>
        public virtual bool SuppressUpdate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether a <see cref="Containers.Menu{T}"/> should call the <see cref="Draw(SpriteBatch)"/> method.
        /// </summary>
        public virtual bool SuppressDraw { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="GuiItem"/> is displayed.
        /// </summary>
        public virtual bool Visible { get { return visible; } set { Invoke(VisibilityChanged, this, new ValueChangedEventArgs<bool>(visible, value)); } }
        /// <summary>
        /// Gets or sets the width of the <see cref="GuiItem"/> including its nonclient elements in pixels.
        /// </summary>
        public virtual int Width { get { return Size.Width; } set { Size = new Size(value, Height); } }
        /// <summary>
        /// Gets or sets the horizontal component of the <see cref="GuiItem"/>.
        /// </summary>
        public virtual float X { get { return Position.X; } set { Position = new Vector2(value, Y); } }
        /// <summary>
        /// Gets or sets the vertical component of the <see cref="GuiItem"/>.
        /// </summary>
        public virtual float Y { get { return Position.Y; } set { Position = new Vector2(X, value); } }

        /// <summary> The <see cref="SpriteBatch"/> used for generating the underlying <see cref="Texture2D"/>. </summary>
        protected SpriteBatch batch;
        /// <summary> Stores the <see cref="Texture2D"/> used for drawing. </summary>
        protected TextureHandler textures;
        /// <summary> Whether the <see cref="Mouse"/> is hovering over the <see cref="GuiItem"/>. </summary>
        protected bool over;
        /// <summary> Whether the <see cref="GuiItem"/> is left clicked. </summary>
        protected bool leftDown;
        /// <summary> Whether the <see cref="GuiItem"/> is right clicked. </summary>
        protected bool rightDown;

        private Color backColor;
        private Size size;
        private Vector2 position;
        private Color foreColor;
        private string name;
        private float rotation;
        private bool visible;
        private bool enabled;

        /// <summary>
        /// Occurs when the value of the <see cref="BackColor"/> property changes.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<Color> BackColorChanged;
        /// <summary>
        /// Occurs when the value of the <see cref="BackgroundImage"/> property changes.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<Texture2D> BackgroundImageChanged;
        /// <summary>
        /// Occurs when the <see cref="GuiItem"/> is clicked.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event MouseEventHandler Clicked;
        /// <summary>
        /// Occurs when the <see cref="Enabled"/> property changes.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<bool> EnabledChanged;
        /// <summary>
        /// Occurs when the <see cref="ForeColor"/> property changes.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<Color> ForeColorChanged;
        /// <summary>
        /// Occurs when the mouse pointer rests on the <see cref="GuiItem"/>.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event MouseEventHandler Hover;
        /// <summary>
        /// Occurs when the mouse pointer enters the <see cref="GuiItem"/>.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event MouseEventHandler HoverEnter;
        /// <summary>
        /// Occurs when the mouse pointer leaves the <see cref="GuiItem"/>.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event MouseEventHandler HoverLeave;
        /// <summary>
        /// Occurs when the <see cref="GuiItem"/> is moved.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<Vector2> Moved;
        /// <summary>
        /// Occurs when the <see cref="Name"/> proprty changes.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<string> NameChanged;
        /// <summary>
        /// Occurs when the <see cref="Rotation"/> propery changes.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<float> Rotated;
        /// <summary>
        /// Occurs when the <see cref="GuiItem"/> is resized.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<Size> Resized;
        /// <summary>
        /// Occurs when the <see cref="Visible"/> property changes.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event ValueChangedEventHandler<bool> VisibilityChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiItem"/> class with default settings.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        public GuiItem(ref SpriteBatch sb)
            : this(ref sb, DefaultBounds)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiItem"/> class with specific size.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="bounds"> The size of the <see cref="GuiItem"/> in pixels. </param>
        [SuppressMessage(CAT_USAGE, CHECKID_CALL, Justification = JUST_VIRT_FINE)]
        public GuiItem(ref SpriteBatch sb, Rect bounds)
        {
#if DEBUG
            type = LogMsgType.Ctor;
#endif
            CheckBounds(bounds.Size);

            InitEvents();
            SetTextureHandler();
            batch = sb;

            Bounds = bounds;
            BackColor = DefaultBackColor;
            ForeColor = DefaultForeColor;

            Show();

#if DEBUG
            type = LogMsgType.Call;
#endif
        }

        /// <summary>
        /// Disposes the unmanaged resources.
        /// </summary>
        ~GuiItem()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases the managed and unmanaged resources used by the <see cref="GuiItem"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged and managed resources used by the <see cref="GuiItem"/>.
        /// </summary>
        /// <param name="disposing"> Whether the managed resources should be disposed. </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                Disposing = true;
                textures.Dispose();
                Hide();
                IsDisposed = true;
                Disposing = false;
            }
        }

        /// <summary>
        /// Perform a mouse click.
        /// </summary>
        public void PerformClick()
        {
            Invoke(Clicked, this, GetMouseEventArgs());
        }

        /// <summary>
        /// Updates the <see cref="GuiItem"/>, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="deltaTime"> The specified deltatime. </param>
        public virtual void Update(float deltaTime)
        {
            if (Enabled)
            {
                MouseState mState = Mouse.GetState();

                bool newOver = Bounds.Contains(GetRotatedMouse(mState));
                if (!over && newOver) Invoke(HoverEnter, this, GetMouseEventArgs());
                else if (over && !newOver) Invoke(HoverLeave, this, GetMouseEventArgs());

                if (over = newOver)
                {
                    bool down = mState.LeftButton == ButtonState.Pressed || mState.RightButton == ButtonState.Pressed;

                    if (!down && !(leftDown || rightDown)) Invoke(Hover, this, GetMouseEventArgs());
                    else if (mState.LeftButton == ButtonState.Pressed && !(leftDown || rightDown))
                    {
                        Invoke(Clicked, this, GetMouseEventArgs());
                        leftDown = true;
                    }
                    else if (mState.RightButton == ButtonState.Pressed && !(rightDown || leftDown))
                    {
                        Invoke(Clicked, this, GetMouseEventArgs());
                        rightDown = true;
                    }
                }

                if (leftDown && mState.LeftButton == ButtonState.Released) leftDown = false;
                if (rightDown && mState.RightButton == ButtonState.Released) rightDown = false;
            }
        }

        /// <summary>
        /// Draws the <see cref="GuiItem"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The spritebatch to use. </param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(textures.Background, Position, null, textures.BackgroundSet() ? BackColor : Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, 1f);
                spriteBatch.Draw(textures.Foreground, Position, null, Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Moves the <see cref="GuiItem"/> to a specified relative position.
        /// If x is set the horizontal component of the anchor will have no effect.
        /// If y is set the vertical component of the anchor will have no effect.
        /// </summary>
        /// <param name="anchor"> A valid relative position. </param>
        /// <param name="x"> The horizontal component of the new position. </param>
        /// <param name="y"> The vertical component of the new position. </param>
        /// <exception cref="ArgumentException"> The anchor is invalid. </exception>
        public void MoveRelative(Anchor anchor, float? x = null, float? y = null)
        {
            if (anchor.RequiresWork())
            {
                if (!x.HasValue)
                {
                    if (anchor.ContainesValue(Anchor.CenterWidth)) x = (batch.GraphicsDevice.Viewport.Width >> 1) - (Width >> 1);
                    if (anchor.ContainesValue(Anchor.Left)) x = 0;
                    if (anchor.ContainesValue(Anchor.Right)) x = batch.GraphicsDevice.Viewport.Width - Width;
                    if (!x.HasValue) x = Position.X;
                }
                if (!y.HasValue)
                {
                    if (anchor.ContainesValue(Anchor.CenterHeight)) y = (batch.GraphicsDevice.Viewport.Height >> 1) - (Height >> 1);
                    if (anchor.ContainesValue(Anchor.Top)) y = 0;
                    if (anchor.ContainesValue(Anchor.Bottom)) y = batch.GraphicsDevice.Viewport.Height - Height;
                    if (!y.HasValue) y = Position.Y;
                }

                if (x != Position.X || y != Position.Y) Position = new Vector2(x.Value, y.Value);
            }
            else if (!anchor.IsValid()) throw new ArgumentException("The anchor is invalid!");
        }

        /// <summary>
        /// Enables the <see cref="GuiItem"/> and makes it visible.
        /// </summary>
        public void Show()
        {
            Enabled = true;
            Visible = true;
        }

        /// <summary>
        /// Disables the <see cref="GuiItem"/> and makes it hiden.
        /// </summary>
        public void Hide()
        {
            Enabled = false;
            Visible = false;
        }

        /// <summary>
        /// Updates the textures used for drawing.
        /// </summary>
        public virtual void Refresh()
        {
            if (suppressRefresh) return;

#if DEBUG
            Stopwatch sw = Stopwatch.StartNew();
#endif
            SetBackgroundTexture();
            SetForegroundTexture();

#if DEBUG
            sw.Stop();
            LogBase(ToString(), $"refreshed, texture creation took {sw.ElapsedMilliseconds} milliseconds");
#endif
        }

        /// <summary>
        /// Returns a string that represents the current GuiItem.
        /// </summary>
        /// <returns> A string that represents the current GuiItem. </returns>
        public override string ToString()
        {
            return string.Format("{0}{1}", GetType().Name, string.IsNullOrEmpty(Name) ? string.Empty : $"({Name})");
        }

        /// <summary>
        /// Gets the position of the mouse in respect to the <see cref="GuiItem"/>.
        /// </summary>
        /// <param name="state"> The <see cref="MouseState"/> to use. </param>
        /// <returns> The position of the <see cref="Mouse"/> rotated like the <see cref="GuiItem"/>. </returns>
        /// <remarks>
        /// This method is used internaly to determine whether the mouse is inside the client rectangle of the GuiItem.
        /// </remarks>
        /// <example>
        /// This example show how the method is used internaly.
        /// 
        /// <code>
        /// public virtual void Update(MouseState mState)
        /// {
        ///    if (Enabled)
        ///    {
        ///        Vector2 mPos = GetRotatedMouse(mState);
        ///        over = bounds.Contains(mPos.ToPoint());
        ///        <![CDATA[bool down = over && (mState.LeftButton == ButtonState.Pressed || mState.RightButton == ButtonState.Pressed);]]>
        ///     }
        /// }
        /// </code>
        /// </example>
        protected Vector2 GetRotatedMouse(MouseState state)
        {
            return Vector2.Transform(state.Position() - Position, Matrix.CreateRotationZ(-rotation)) + Position;
        }

        /// <summary>
        /// Gets the mouse state as a <see cref="MouseEventArgs"/> object.
        /// </summary>
        /// <returns> The current mouse state. </returns>
        protected MouseEventArgs GetMouseEventArgs()
        {
            return new MouseEventArgs(Mouse.GetState());
        }

        /// <summary>
        /// This method is called when the <see cref="BackColorChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new <see cref="Color"/> for the background. </param>
        protected virtual void OnBackColorChanged(GuiItem sender, ValueChangedEventArgs<Color> e)
        {
            backColor = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="BackgroundImageChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new <see cref="Texture2D"/> to use as background. </param>
        protected virtual void OnBackgroundImageChanged(GuiItem sender, ValueChangedEventArgs<Texture2D> e)
        {
            textures.Background = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="EnabledChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new enabled value of the <see cref="GuiItem"/>. </param>
        protected virtual void OnEnabledChanged(GuiItem sender, ValueChangedEventArgs<bool> e)
        {
            enabled = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="ForeColorChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new <see cref="Color"/> for the foreground. </param>
        protected virtual void OnForeColorChanged(GuiItem sender, ValueChangedEventArgs<Color> e)
        {
            foreColor = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="Moved"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new position of the <see cref="GuiItem"/>. </param>
        protected virtual void OnMove(GuiItem sender, ValueChangedEventArgs<Vector2> e)
        {
            position = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="NameChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new name for the <see cref="GuiItem"/>. </param>
        protected virtual void OnNameChange(GuiItem sender, ValueChangedEventArgs<string> e)
        {
            name = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="Rotated"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new rotation (in radians). </param>
        protected virtual void OnRotationChanged(GuiItem sender, ValueChangedEventArgs<float> e)
        {
            rotation = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="Resized"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new size of the <see cref="GuiItem"/>. </param>
        protected virtual void OnResize(GuiItem sender, ValueChangedEventArgs<Size> e)
        {
            CheckBounds(e.NewValue);
            size = e.NewValue;
        }

        /// <summary>
        /// This method is called when the <see cref="VisibilityChanged"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new visibility of the <see cref="GuiItem"/>. </param>
        protected virtual void OnVisibilityChanged(GuiItem sender, ValueChangedEventArgs<bool> e)
        {
            visible = e.NewValue;
        }

        /// <summary>
        /// Sets the background texture for the <see cref="GuiItem"/>.
        /// </summary>
        protected virtual void SetBackgroundTexture()
        {
            textures.SetBackFromClr(BackColor, Size, batch.GraphicsDevice);
        }

        /// <summary>
        /// Sets the foreground texture for the <see cref="GuiItem"/>.
        /// </summary>
        protected virtual void SetForegroundTexture()
        {
            textures.SetForeFromClr(ForeColor, Size, batch.GraphicsDevice);
        }
        
        /// <summary>
        /// Sets <see cref="textures"/> to the required <see cref="TextureHandler"/>.
        /// </summary>
        protected virtual void SetTextureHandler()
        {
            textures = new TextureHandler();
        }

        /// <summary>
        /// Handles the initializing of the events.
        /// </summary>
        protected virtual void InitEvents()
        {
            BackColorChanged += OnBackColorChanged;
            BackgroundImageChanged += OnBackgroundImageChanged;
            EnabledChanged += OnEnabledChanged;
            ForeColorChanged += OnForeColorChanged;
            Moved += OnMove;
            NameChanged += OnNameChange;
            Rotated += OnRotationChanged;
            Resized += OnResize;
            VisibilityChanged += OnVisibilityChanged;
        }

        private void CheckBounds(Size size)
        {
            if (size.Width <= 0 || size.Height <= 0)
            {
                ArgumentException e = new ArgumentException($"Width or Height of {Name ?? GetType().Name} must be greater the zero!");
                e.Data.Add("Size", size);
                throw e;
            }
        }
    }
}