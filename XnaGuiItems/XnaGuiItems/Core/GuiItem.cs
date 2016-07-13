using Mentula.GuiItems.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Mentula.GuiItems.Utilities;

namespace Mentula.GuiItems.Core
{
    /// <summary>
    /// The absolute base class for all <see cref="GuiItems"/>.
    /// </summary>
    public class GuiItem : IDisposable
    {
        /// <summary>
        /// Gets or sets the background color for the <see cref="GuiItem"/>.
        /// </summary>
        public virtual Color BackColor { get { return backColor; } set { Invoke(BackColorChanged, this, value); } }
        /// <summary>
        /// Gets or sets the background image displayed in the <see cref="GuiItem"/>.
        /// </summary>
        public virtual Texture2D BackgroundImage { get { return backgroundImage; } set { Invoke(BackgroundImageChanged, this, value); } }
        /// <summary>
        /// Gets or sets the size of the <see cref="GuiItem"/> including its nonclient elements in pixels.
        /// </summary>
        public virtual Rectangle Bounds { get { return bounds; } set { Invoke(Resize, this, value); } }
        /// <summary>
        /// Gets the default background color of the <see cref="GuiItem"/>.
        /// </summary>
        public static Color DefaultBackColor { get { return Color.WhiteSmoke; } }
        /// <summary>
        /// Gets the default foreground color of the <see cref="GuiItem"/>.
        /// </summary>
        public static Color DefaultForeColor { get { return Color.Black; } }
        /// <summary>
        /// Gets a value indicating wether the base <see cref="GuiItem"/> class is in the process of disposing.
        /// </summary>
        public virtual bool Disposing { get; protected set; }
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="GuiItem"/> can respond to user interaction.
        /// </summary>
        public virtual bool Enabled { get; set; }
        /// <summary>
        /// Gets or sets the foregound color for the <see cref="GuiItem"/>.
        /// </summary>
        public virtual Color ForeColor { get { return foreColor; } set { Invoke(ForeColorChanged, this, value); } }
        /// <summary>
        /// Gets or sets the height of the <see cref="GuiItem"/> including its nonclient elements in pixels.
        /// </summary>
        public virtual int Height { get { return Bounds.Height; } set { Bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, value); } }
        /// <summary>
        /// Gets a value indicating whether the <see cref="GuiItem"/> has been disposed.
        /// </summary>
        public virtual bool IsDisposed { get; protected set; }
        /// <summary>
        /// Gets or sets the name of the <see cref="GuiItem"/>.
        /// </summary>
        public virtual string Name { get { return name; } set { Invoke(NameChanged, this, value); } }
        /// <summary>
        /// Gets or sets the client side rotation of the <see cref="GuiItem"/>.
        /// </summary>
        public virtual float Rotation { get { return rotation; } set { Invoke(Rotated, this, value); } }
        /// <summary>
        /// Gets or sets the position of the <see cref="GuiItem"/>.
        /// </summary>
        public virtual Vector2 Position { get { return bounds.Position(); } set { Invoke(Move, this, value); } }
        /// <summary>
        /// Gets or sets a value indicating whether a <see cref="Menu{T}"/> should call the <see cref="Update(MouseState)"/> method.
        /// </summary>
        public virtual bool SuppressUpdate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether a <see cref="Menu{T}"/> should call the <see cref="Draw(SpriteBatch)"/> method.
        /// </summary>
        public virtual bool SuppressDraw { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="GuiItem"/> is displayed.
        /// </summary>
        public virtual bool Visible { get { return visible; } set { Invoke(VisibilityChanged, this, value); } }
        /// <summary>
        /// Gets or sets the width of the <see cref="GuiItem"/> including its nonclient elements in pixels.
        /// </summary>
        public virtual int Width { get { return Bounds.Width; } set { Bounds = new Rectangle(bounds.X, bounds.Y, value, bounds.Height); } }

        protected GraphicsDevice device;
        protected Color backColor;
        protected Texture2D backColorImage;
        protected Texture2D backgroundImage;
        protected Rectangle bounds;
        protected Color foreColor;
        protected string name;
        protected float rotation;
        protected Texture2D foregoundTexture;
        protected bool visible;
        protected bool over;
        protected bool leftClicked;
        protected bool rigthClicked;

        /// <summary>
        /// Occurs when the value of the <see cref="BackColor"/> property changes.
        /// </summary>
        public event ReColorEventHandler BackColorChanged;
        /// <summary>
        /// Occurs when the value of the <see cref="BackgroundImage"/> property changes.
        /// </summary>
        public event ReTextureEventhandler BackgroundImageChanged;
        /// <summary>
        /// Occurs when the <see cref="GuiItem"/> is clicked.
        /// </summary>
        public event MouseEventHandler Click;
        /// <summary>
        /// Occurs when the <see cref="ForeColor"/> property changes.
        /// </summary>
        public event ReColorEventHandler ForeColorChanged;
        /// <summary>
        /// Occurs when the mouse pointer rests on the <see cref="GuiItem"/>.
        /// </summary>
        public event MouseEventHandler Hover;
        /// <summary>
        /// Occurs when the <see cref="GuiItem"/> is moved.
        /// </summary>
        public event MoveEventHandler Move;
        /// <summary>
        /// Occurs when the <see cref="Name"/> proprty changes.
        /// </summary>
        public event TextChangedEventHandler NameChanged;
        /// <summary>
        /// Occurs when the <see cref="Rotation"/> propery changes.
        /// </summary>
        public event RotationChangedEventHandler Rotated;
        /// <summary>
        /// Occurs when the <see cref="GuiItem"/> is resized.
        /// </summary>
        public event ReSizeEventhandler Resize;
        /// <summary>
        /// Occurs when the <see cref="Visible"/> property changes.
        /// </summary>
        public event VisibilityChangedEventHandler VisibilityChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiItem"/> class with default settings.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="GuiItem"/> to. </param>
        public GuiItem(GraphicsDevice device)
        {
            InitEvents();
            this.device = device;
            bounds = new Rectangle(0, 0, 100, 50);
            BackColor = DefaultBackColor;
            ForeColor = DefaultForeColor;
            Show();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiItem"/> class with specific size.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="GuiItem"/> to. </param>
        /// <param name="bounds"> The size of the <see cref="GuiItem"/> in pixels. </param>
        public GuiItem(GraphicsDevice device, Rectangle bounds)
            : this(device)
        {
            if (bounds.Width <= 0 || bounds.Height <= 0) throw new ArgumentException("bounds.Width and bounds.Height must be greater then zero");
            this.bounds = bounds;
        }

        /// <summary>
        /// Release the unmanaged and managed resources used by the <see cref="GuiItem"/>.
        /// </summary>
        public virtual void Dispose()
        {
            if (!IsDisposed)
            {
                Disposing = true;

                if (backColorImage != null) backColorImage.Dispose();
                if (backgroundImage != null) backgroundImage.Dispose();
                if (foregoundTexture != null) foregoundTexture.Dispose();

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
            Invoke(Click, Mouse.GetState());
        }

        /// <summary>
        /// Updates the <see cref="GuiItem"/>, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="mState"> The current state of the <see cref="Mouse"/>. </param>
        public virtual void Update(MouseState mState)
        {
            if (Enabled)
            {
                Vector2 mPos = GetRotatedMouse(mState);

                over = new Rectangle((int)Position.X, (int)Position.Y, bounds.Width, bounds.Height).Contains((int)mPos.X, (int)mPos.Y);
                bool down = over && (mState.LeftButton == ButtonState.Pressed || mState.RightButton == ButtonState.Pressed);

                if (!down && over) Invoke(Hover, this, mState);
                else if (down && !leftClicked)
                {
                    Invoke(Click, this, mState);
                    leftClicked = true;
                }
                else if (down && !rigthClicked)
                {
                    Invoke(Click, this, mState);
                    rigthClicked = true;
                }

                if (leftClicked && mState.LeftButton == ButtonState.Released) leftClicked = false;
                if (rigthClicked && mState.RightButton == ButtonState.Released) rigthClicked = false;
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
                if (backgroundImage != null)
                {
                    spriteBatch.Draw(backgroundImage, Position, null, BackColor, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
                }
                else
                {
                    spriteBatch.Draw(backColorImage, Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
                }
            }
        }

        /// <summary>
        /// Enables the <see cref="GuiItem"/> and makes it visible.
        /// </summary>
        public virtual void Show()
        {
            Enabled = true;
            Visible = true;
        }

        /// <summary>
        /// Disables the <see cref="GuiItem"/> and makes it hiden.
        /// </summary>
        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;
        }

        /// <summary>
        /// When overriden in a derived class, updates the textures used for drawing.
        /// </summary>
        public virtual void Refresh() { }

        /// <summary>
        /// Gets the position of the mouse in respect to the <see cref="GuiItem"/>.
        /// </summary>
        /// <param name="state"> The <see cref="MouseState"/> to use. </param>
        protected Vector2 GetRotatedMouse(MouseState state)
        {
            return Vector2.Transform(state.Position() - Position, Matrix.CreateRotationZ(-rotation)) + Position;
        }

        protected virtual void OnBackColorChanged(GuiItem sender, Color newColor) { backColor = newColor; backColorImage = Drawing.FromColor(backColor, bounds.Width, bounds.Height, device); }
        protected virtual void OnBackgroundImageChaned(GuiItem sender, Texture2D newTexture) { backgroundImage = newTexture; }
        protected virtual void OnForeColorChanged(GuiItem sender, Color newColor) { foreColor = newColor; foregoundTexture = Drawing.FromColor(foreColor, bounds.Width, bounds.Height, device); }
        protected virtual void OnMove(GuiItem sender, Vector2 newpos) { bounds.X = (int)newpos.X; bounds.Y = (int)newpos.Y; }
        protected virtual void OnNameChange(GuiItem sender, string newName) { name = newName; }
        protected virtual void OnRotationChanged(GuiItem sender, float newRot) { rotation = newRot; }
        protected virtual void OnResize(GuiItem sender, Rectangle newSize)
        {
            bounds = newSize;
            backColorImage = Drawing.FromColor(backColor, bounds.Width, bounds.Height, device);
            foregoundTexture = Drawing.FromColor(foreColor, bounds.Width, bounds.Height, device);
        }
        protected virtual void OnVisibilityChanged(GuiItem sender, bool visibility) { visible = visibility; }

        private void InitEvents()
        {
            BackColorChanged += OnBackColorChanged;
            BackgroundImageChanged += OnBackgroundImageChaned;
            ForeColorChanged += OnForeColorChanged;
            Move += OnMove;
            NameChanged += OnNameChange;
            Rotated += OnRotationChanged;
            Resize += OnResize;
            VisibilityChanged += OnVisibilityChanged;
        }

        /// <summary>
        /// A collection for storing <see cref="GuiItem"/> in a list with a specified owner.
        /// </summary>
        public class GuiItemCollection : IList<GuiItem>
        {
            /// <summary>
            /// Gets the <see cref="GuiItem"/> that owns this <see cref="GuiItemCollection"/>.
            /// </summary>
            public GuiItem Owner { get; private set; }
            /// <summary>
            /// Gets or sets the items in this <see cref="GuiItemCollection"/>.
            /// </summary>
            protected List<GuiItem> Items { get; set; }

            /// <summary>
            /// Get the number of elements actually contained in the <see cref="GuiItemCollection"/>.
            /// </summary>
            public int Count { get { return Items.Count; } }
            /// <summary>
            /// Gets a value indicating whether the collection is read-only.
            /// </summary>
            public virtual bool IsReadOnly { get { return false; } }
            /// <summary>
            /// Initializes a new instance of the <see cref="GuiItemCollection"/> class.
            /// </summary>
            /// <param name="owner"> A <see cref="GuiItem"/> that owns the collection. </param>
            public GuiItemCollection(GuiItem owner) { Owner = owner; Items = new List<GuiItem>(); }
            /// <summary>
            /// Indicates the <see cref="GuiItem"/> at the specified indexed location in the collection.
            /// </summary>
            /// <param name="index"> The index of the <see cref="GuiItem"/> to retriece from the <see cref="GuiItemCollection"/>. </param>
            /// <returns> The <see cref="GuiItem"/> located at the specified index location within the <see cref="GuiItemCollection"/>. </returns>
            /// <exception cref="ArgumentOutOfRangeException"> The index value is less than zero or is greater than or equal to the number of <see cref="GuiItem"/> in the collection. </exception>
            public virtual GuiItem this[int index] { get { return Items[index]; } set { Items[index] = value; } }
            /// <summary>
            /// Indicates a <see cref="GuiItem"/> with the specified key in the collection.
            /// </summary>
            /// <param name="key"> The <see cref="Name"/> of the <see cref="GuiItem"/> to retrieve from the <see cref="GuiItemCollection"/>. </param>
            /// <returns> The XnaMentula.GuiItems.GuiItem with the specified key within the GuiItemCollection. </returns>
            public virtual GuiItem this[string key] { get { return Items.First(i => i.Name == key); } set { Items[Items.FindIndex(i => i.Name == key)] = value; } }
            /// <summary>
            /// Retrieves the index of the specified <see cref="GuiItem"/> in the <see cref="GuiItemCollection"/>.
            /// </summary>
            /// <param name="item"> The <see cref="GuiItem"/> to locate in the collection. </param>
            /// <returns> A zero-based index value that represents the position of the specified <see cref="GuiItem"/> in the <see cref="GuiItemCollection"/>. </returns>
            public virtual int IndexOf(GuiItem item) { return Items.IndexOf(item); }
            /// <summary>
            /// Instert the specified <see cref="GuiItem"/> to the <see cref="GuiItemCollection"/> at the specified index.
            /// </summary>
            /// <param name="index"> The index of the <see cref="GuiItem"/> used to insert the Item. </param>
            /// <param name="item"> The <see cref="GuiItem"/> to add to the collection. </param>
            public virtual void Insert(int index, GuiItem item) { Items.Insert(index, item); }
            /// <summary>
            /// Removes a <see cref="GuiItem"/> from the <see cref="GuiItemCollection"/> at the specifed indexed location.
            /// </summary>
            /// <param name="index"> The index value of the <see cref="GuiItem"/> to remove. </param>
            public virtual void RemoveAt(int index) { Items.RemoveAt(index); }
            /// <summary>
            /// Adds the specified <see cref="GuiItem"/> to the <see cref="GuiItemCollection"/>.
            /// </summary>
            /// <param name="item"> The <see cref="GuiItem"/> to add to the collection. </param>
            public virtual void Add(GuiItem item) { Items.Add(item); }
            /// <summary>
            /// Removes all <see cref="GuiItem"/> from the <see cref="GuiItemCollection"/>.
            /// </summary>
            public virtual void Clear() { Items.Clear(); }
            /// <summary>
            /// Determines whether the specified <see cref="GuiItem"/> is a member of the <see cref="GuiItemCollection"/>.
            /// </summary>
            /// <param name="item"> The <see cref="GuiItem"/> to locate in the collection. </param>
            /// <returns> true if the <see cref="GuiItem"/> is a member of the collection; otherwise false. </returns>
            public virtual bool Contains(GuiItem item) { return Items.Contains(item); }
            /// <summary>
            /// Copies the entire contents of this <see cref="GuiItemCollection"/> to a compatible one-dimensional <see cref="GuiItem"/>[],
            /// starting at the specified index of the target array.
            /// </summary>
            /// <param name="array"> 
            /// The one-dimensional <see cref="GuiItem"/>[] that is the destination of the elements copied from the current collection.
            /// The array must have zero-based indexing.
            /// </param>
            /// <param name="arrayIndex"> The zero-based index in array at which copying begins. </param>
            /// <exception cref="ArgumentNullException"> Array is null. </exception>
            /// <exception cref="ArgumentOutOfRangeException"> index is less than 0. </exception>
            /// <exception cref="ArgumentException"> 
            /// Array is multidimensional. -or- 
            /// The number of elements in the source collection is greater than the available space from index to the end of array. </exception>
            public virtual void CopyTo(GuiItem[] array, int arrayIndex) { Items.CopyTo(array, arrayIndex); }
            /// <summary>
            /// Removes the specified <see cref="GuiItem"/> from the <see cref="GuiItemCollection"/>.
            /// </summary>
            /// <param name="item"> The <see cref="GuiItem"/> to remove from the <see cref="GuiItemCollection"/>. </param>
            public virtual bool Remove(GuiItem item) { return Items.Remove(item); }
            /// <summary>
            /// Retrieves a refrence to an enumerator object that is used to iterate over a <see cref="GuiItemCollection"/>.
            /// </summary>
            /// <returns> An <see cref="IEnumerator{GuiItem}"/>. </returns>
            public virtual IEnumerator<GuiItem> GetEnumerator() { return Items.GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() { return Items.GetEnumerator(); }
        }
    }
}