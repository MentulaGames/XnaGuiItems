using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mentula.GuiItems.Core
{
    /// <summary>
    /// The absolute base class for all Mentula.GuiItems.
    /// </summary>
    public class GuiItem : IDisposable
    {
        /// <summary>
        /// Gets or sets the background color for the GuiItem.
        /// </summary>
        public virtual Color BackColor { get { return backColor; } set { BackColorChanged(this, value); } }
        /// <summary>
        /// Gets or sets the background image displayed in the GuiItem.
        /// </summary>
        public virtual Texture2D BackgroundImage { get { return backgroundImage; } set { BackgroundImageChanged(this, value); } }
        /// <summary>
        /// Gets or sets the size of the guiItem including its nonclient elements,
        /// in pixels, relative to the parent guiItem.
        /// </summary>
        public virtual Rectangle Bounds { get { return bounds; } set { Resize(this, value); } }
        /// <summary>
        /// Gets the default background color of the GuiItem.
        /// </summary>
        public static Color DefaultBackColor { get { return Color.WhiteSmoke; } }
        /// <summary>
        /// Gets the default foreground color of the GuiItem.
        /// </summary>
        public static Color DefaultForeColor { get { return Color.Black; } }
        /// <summary>
        /// Gets a value indicating wether the base XnaMentula.GuiItems.Core.GuiItem class is in the process of disposing.
        /// </summary>
        public virtual bool Disposing { get; protected set; }
        /// <summary>
        /// Gets or sets a value indicating whether the GuiItem can respond to user interaction.
        /// </summary>
        public virtual bool Enabled { get; set; }
        /// <summary>
        /// Gets or sets the foregound color for the GuiItem.
        /// </summary>
        public virtual Color ForeColor { get { return foreColor; } set { ForeColorChanged(this, value); } }
        /// <summary>
        /// Gets or sets the height of the guiItem including its nonclient elements,
        /// in pixels, relative to the parent guiItem.
        /// </summary>
        public virtual int Height { get { return Bounds.Height; } set { Bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, value); } }
        /// <summary>
        /// Gets a value indicating whether the GuiItem has been disposed of.
        /// </summary>
        public virtual bool IsDisposed { get; protected set; }
        /// <summary>
        /// Gets or sets the name of the GuiItem.
        /// </summary>
        public virtual string Name { get { return name; } set { NameChanged(this, value); } }
        /// <summary>
        /// Gets or sets the client side rotation of the GuiItem.
        /// </summary>
        public virtual float Rotation { get { return rotation; } set { Rotated(this, value); } }
        /// <summary>
        /// Gets or sets the position of the GuiItem.
        /// </summary>
        public virtual Vector2 Position { get { return bounds.Position(); } set { Move(this, value); } }
        /// <summary>
        /// Gets or sets a value indicating weather a menu should call the update method.
        /// </summary>
        public virtual bool SuppressUpdate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating weather a menu should call the draw method.
        /// </summary>
        public virtual bool SuppressDraw { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the GuiItem and all its child Mentula.GuiItems are displayed.
        /// </summary>
        public virtual bool Visible { get { return visible; } set { VisibilityChanged(this, value); } }
        /// <summary>
        /// Gets or sets the width of the guiItem including its nonclient elements,
        /// in pixels, relative to the parent guiItem.
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
        protected bool leftClicked;
        protected bool rigthClicked;

        /// <summary>
        /// Occurs when the value of the XnaGuiItem.Core.GuiItem.BackColor property changes.
        /// </summary>
        public event ReColorEventHandler BackColorChanged;
        /// <summary>
        /// Occurs when the value of the XnaMentula.GuiItems.Core.GuiItem.BackgroundImage property changes.
        /// </summary>
        public event ReTextureEventhandler BackgroundImageChanged;
        /// <summary>
        /// Occurs when the guiItem is clicked.
        /// </summary>
        public event MouseEventHandler Click;
        /// <summary>
        /// Occurs when the XnaMentula.GuiItems.Core.GuiItem.ForeColor property changes.
        /// </summary>
        public event ReColorEventHandler ForeColorChanged;
        /// <summary>
        /// Occurs when the mouse pointer rests on the guiItem.
        /// </summary>
        public event MouseEventHandler Hover;
        /// <summary>
        /// Occurs when the GuiItem is moved.
        /// </summary>
        public event MoveEventHandler Move;
        /// <summary>
        /// Occurs when the XnaMentula.GuiItems.Core.GuiItem.Name proprty changes.
        /// </summary>
        public event TextChangedEventHandler NameChanged;
        /// <summary>
        /// Occurs when the XnaMentula.GuiItems.Core.GuiItem.Rotation propery changes.
        /// </summary>
        public event RotationChangedEventHandler Rotated;
        /// <summary>
        /// Occurs when the GuiItem is resized.
        /// </summary>
        public event ReSizeEventhandler Resize;
        /// <summary>
        /// Occurs when the XnaMentula.GuiItems.Core.GuiItem.Visible property changes.
        /// </summary>
        public event VisibilityChangedEventHandler VisibilityChanged;

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Core.GuiItem class with default settings.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.GuiItem to. </param>
        public GuiItem(GraphicsDevice device)
        {
            InitEvents();
            this.device = device;
            bounds = new Rectangle(0, 0, 100, 50);
            BackColor = DefaultBackColor;
            ForeColor = DefaultForeColor;
            Enabled = true;
            visible = true;
        }

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Core.GuiItem class with specific size.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.GuiItem to. </param>
        /// <param name="bounds"> The size of the GuiItem in pixels. </param>
        public GuiItem(GraphicsDevice device, Rectangle bounds)
            : this(device)
        {
            if (bounds.Width <= 0 || bounds.Height <= 0) throw new ArgumentException("Bounds.Width and bounds.Height must be greater then zero");
            this.bounds = bounds;
        }

        /// <summary>
        /// Release the unmanaged and managed resources used by the XnaMentula.GuiItems.Core.GuiItem and its child Mentula.GuiItems.
        /// </summary>
        public virtual void Dispose()
        {
            if (!IsDisposed)
            {
                Disposing = true;

                if (backColorImage != null) backColorImage.Dispose();
                if (backgroundImage != null) backgroundImage.Dispose();
                if (foregoundTexture != null) foregoundTexture.Dispose();

                Enabled = false;
                IsDisposed = true;
                Disposing = false;
            }
        }

        /// <summary>
        /// Perform a mouse click.
        /// </summary>
        public void PerformClick()
        {
            if (Click != null) Click(this, Mouse.GetState());
        }

        /// <summary>
        /// Updates the XnaMentula.GuiItems.Core.GuiItem, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="mState"> The current state of the mouse. </param>
        public virtual void Update(MouseState mState)
        {
            if (Enabled)
            {
                Vector2 mPos = GetRotatedMouse(mState);

                bool over = new Rectangle(Position.X(), Position.Y(), bounds.Width, bounds.Height).Contains(mPos.X(), mPos.Y());
                bool down = over && (mState.LeftButton == ButtonState.Pressed || mState.RightButton == ButtonState.Pressed);

                if (!down && over && Hover != null) Hover.Invoke(this, mState);
                else if (down && Click != null && !leftClicked)
                {
                    Click.Invoke(this, mState);
                    leftClicked = true;
                }
                else if (down && Click != null && !rigthClicked)
                {
                    Click.Invoke(this, mState);
                    rigthClicked = true;
                }

                if (leftClicked && mState.LeftButton == ButtonState.Released) leftClicked = false;
                if (rigthClicked && mState.RightButton == ButtonState.Released) rigthClicked = false;
            }
        }

        /// <summary>
        /// Draws the XnaMentula.GuiItems.Core.GuiItem and its childs to the specified spritebatch.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(backColorImage, Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);

                if (backgroundImage != null)
                {
                    spriteBatch.Draw(backgroundImage, Position + bounds.Position(), null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
                }
            }
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;
        }

        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;
        }

        protected Vector2 GetRotatedMouse(MouseState state)
        {
            return Vector2.Transform(state.Position() - Position, Matrix.CreateRotationZ(-rotation)) + Position;
        }

        protected virtual void OnBackColorChanged(object sender, Color newColor) { backColor = newColor; backColorImage = Drawing.FromColor(backColor, bounds.Width, bounds.Height, device); }
        protected virtual void OnBackgroundImageChaned(object sender, Texture2D newTexture) { backgroundImage = newTexture; }
        protected virtual void OnForeColorChanged(object sender, Color newColor) { foreColor = newColor; foregoundTexture = Drawing.FromColor(foreColor, bounds.Width, bounds.Height, device); }
        protected virtual void OnMove(object sender, Vector2 newpos) { bounds.X = newpos.X(); bounds.Y = newpos.Y(); }
        protected virtual void OnNameChange(object sender, string newName) { name = newName; }
        protected virtual void OnRotationChanged(object sender, float newRot) { rotation = newRot; }
        protected virtual void OnResize(object sender, Rectangle newSize)
        {
            bounds = newSize;
            backColorImage = Drawing.FromColor(backColor, bounds.Width, bounds.Height, device);
            foregoundTexture = Drawing.FromColor(foreColor, bounds.Width, bounds.Height, device);
        }
        protected virtual void OnVisibilityChanged(object sender, bool visibility) { visible = visibility; }

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

        public class GuiItemCollection : IList<GuiItem>, ICollection<GuiItem>, IEnumerable<GuiItem>
        {
            /// <summary>
            /// Gets the GuiItem that owns this Mentula.GuiItems.GuiItem.GuiItemCollection.
            /// </summary>
            public GuiItem Owner { get; private set; }
            protected List<GuiItem> Items { get; set; }

            /// <summary>
            /// Get the number of elements actually contained in the XnaMentula.GuiItems.GuiItem.GuiItemCollection.
            /// </summary>
            public int Count { get { return Items.Count; } }
            /// <summary>
            /// Gets a value indicating whether the collection is read-only.
            /// </summary>
            public virtual bool IsReadOnly { get { return false; } }
            /// <summary>
            /// Initializes a new instance of the XnaMentula.GuiItems.GuiItem.GuiItemCollection class.
            /// </summary>
            /// <param name="owner"> A XnaMentula.GuiItems.Mentula.GuiItems representing the GuiItem that owns the collection. </param>
            public GuiItemCollection(GuiItem owner) { Owner = owner; Items = new List<GuiItem>(); }
            /// <summary>
            /// Indicates the GuiItem at the specified indexed location in the collection.
            /// </summary>
            /// <param name="index"> The index of the GuiItem to retriece from the GuiItemCollection. </param>
            /// <returns> The XnaMentula.GuiItems.GuiItem located at the specified index location within the GuiItemCollection. </returns>
            /// <exception cref="System.ArgumentOutOfRangeException"> The index value is less than zero or is greater than or equal to the number of Mentula.GuiItems in the collection. </exception>
            public virtual GuiItem this[int index] { get { return Items[index]; } set { Items[index] = value; } }
            /// <summary>
            /// Indicates a GuiItem with the specified key in the collection.
            /// </summary>
            /// <param name="key"> The name of the guiItem to retrieve from the GuiItemCollection. </param>
            /// <returns> The XnaMentula.GuiItems.GuiItem with the specified key within the GuiItemCollection. </returns>
            public virtual GuiItem this[string key] { get { return Items.First(i => i.Name == key); } set { Items[Items.FindIndex(i => i.Name == key)] = value; } }
            /// <summary>
            /// Retrieves the index of the specified GuiItem in the GuiItemCollection.
            /// </summary>
            /// <param name="item"> The GuiItem to locate in the collection. </param>
            /// <returns> A zero-based index value that represents the posiion of the specified Mentula.GuiItems.GuiItem in the GuiItemCollection. </returns>
            public virtual int IndexOf(GuiItem item) { return Items.IndexOf(item); }
            /// <summary>
            /// Instert the specified GuiItem to the GuiItemCollection at the specified index.
            /// </summary>
            /// <param name="index"> The index of the GuiItem used to insert the Item. </param>
            /// <param name="item"> The XnaMentula.GuiItems.GuiItem to add to the GuiItemCollection. </param>
            public virtual void Insert(int index, GuiItem item) { Items.Insert(index, item); }
            /// <summary>
            /// Removes a GuiItem from the GuiItemCollection at the specifed indexed location.
            /// </summary>
            /// <param name="index"> The index value of the XnaMentula.GuiItems.GuiItem to remove. </param>
            public virtual void RemoveAt(int index) { Items.RemoveAt(index); }
            /// <summary>
            /// Adds the specified GuiItem to the GuiItemCollection.
            /// </summary>
            /// <param name="item"> The XnaMentula.GuiItems.GuiItem to add to the GuiItemCollection. </param>
            public virtual void Add(GuiItem item) { Items.Add(item); }
            /// <summary>
            /// Removes all Mentula.GuiItems from the GuiItemCollection.
            /// </summary>
            public virtual void Clear() { Items.Clear(); }
            /// <summary>
            /// Determines whether the specified GuiItem is a member of the GuiItemCollection.
            /// </summary>
            /// <param name="item"> The XnaMentula.GuiItems.GuiItem to locate in the GuiItemCollection. </param>
            /// <returns> true if the XnaMentula.GuiItems.GuiItem is a member of the collection; otherwise false. </returns>
            public virtual bool Contains(GuiItem item) { return Items.Contains(item); }
            /// <summary>
            /// Copies the entire contents of this GuiItemCollection to a compatible one-dimensional XnaMentula.GuiItems.GuiItem[],
            /// starting at the specified index of the target array.
            /// </summary>
            /// <param name="array"> 
            /// The one-dimensional XnaMentula.GuiItems.GuiItem[] that is the destination of the elements copied from the current collection.
            /// The array must have zero-based indexing.
            /// </param>
            /// <param name="arrayIndex"> The zero-based index in array at which copying begins. </param>
            /// <exception cref="System.ArgumentNullException"> Array is null. </exception>
            /// <exception cref="System.ArgumentOutOfRangeException"> index is less than 0. </exception>
            /// <exception cref="System.ArgumentException"> 
            /// Array is multidimensional. -or- 
            /// The number of elements in the source collection is greater than the available space from index to the end of array. </exception>
            public virtual void CopyTo(GuiItem[] array, int arrayIndex) { Items.CopyTo(array, arrayIndex); }
            /// <summary>
            /// Removes the specified GuiItem from the GuiItemCollection.
            /// </summary>
            /// <param name="item"> The XnaMentula.GuiItems.GuiItem to remove from the GuiItemCollection. </param>
            public virtual bool Remove(GuiItem item) { return Items.Remove(item); }
            /// <summary>
            /// Retrives a refrence to an enumerator object that is used to iterate over a XnaMentula.GuiItems.GuiItem.GuiItemCollection.
            /// </summary>
            /// <returns> An System.Collections.IEnumerator. </returns>
            public virtual IEnumerator<GuiItem> GetEnumerator() { return Items.GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() { return Items.GetEnumerator(); }
        }
    }
}