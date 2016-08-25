using Mentula.GuiItems.Core;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using static Mentula.GuiItems.Core.GuiItem;
using static Mentula.GuiItems.Utilities;

namespace Mentula.GuiItems.Containers
{
    /// <summary>
    /// A class for grouping <see cref="GuiItems"/>.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public partial class Menu<T> : MentulaGameComponent<T>, IMenu
        where T : Game
    {
        /// <summary>
        /// The order in which to draw this object relative to other objects. Objects with
        /// a lower value are drawn first.
        /// </summary>
        public int DrawOrder { get { return drawOrder; } set { drawOrder = value; Invoke(DrawOrderChanged, this, EventArgs.Empty); } }
        /// <summary>
        /// Indicates whether <see cref="IDrawable.Draw(GameTime)"/> should be called for this game component.
        /// </summary>
        public bool Visible { get { return visible; } set { visible = value; Invoke(VisibleChanged, this, EventArgs.Empty); } }

        /// <summary>
        /// The center width of the viewport.
        /// </summary>
        public int ScreenWidthMiddle { get { return ScreenWidth >> 1; } }
        /// <summary>
        /// The center height of the viewport.
        /// </summary>
        public int ScreenHeightMiddle { get { return ScreenHeight >> 1; } }
        /// <summary>
        /// The width of the viewport.
        /// </summary>
        public int ScreenWidth { get { return device.Viewport.Width; } }
        /// <summary>
        /// The Height of the viewport.
        /// </summary>
        public int ScreenHeight { get { return device.Viewport.Height; } }

        /// <summary>
        /// Occures when the value of <see cref="DrawOrder"/> is changed.
        /// </summary>
        public event EventHandler<EventArgs> DrawOrderChanged;
        /// <summary>
        /// Occures when the value of <see cref="Visible"/> is changed.
        /// </summary>
        public event EventHandler<EventArgs> VisibleChanged;

        /// <summary>
        /// Indicates whether the <see cref="Menu{T}"/> should handle textbox focusing.
        /// Default value = true
        /// </summary>
        protected bool autoFocusTextbox;
        /// <summary>
        /// Indicates whether the <see cref="Menu{T}"/> should handle dropdown focusing.
        /// Default value = true
        /// </summary>
        protected bool autoFocusDropDown;
        /// <summary>
        /// The underlying <see cref="GuiItems"/> of the <see cref="Menu{T}"/>
        /// </summary>
        protected GuiItemCollection controlls;
        /// <summary>
        /// The default <see cref="SpriteFont"/> to use if none is specified.
        /// </summary>
        protected SpriteFont font;
        /// <summary>
        /// The <see cref="GraphicsDevice"/> associated with this <see cref="Menu{T}"/>.
        /// </summary>
        protected GraphicsDevice device;

        private SpriteBatch batch;
        private bool visible;
        private int drawOrder;
        private static readonly InvalidOperationException noFont = new InvalidOperationException("Menu.font must be set before calling this method or a font must be specified!");

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu{T}"/> class.
        /// </summary>
        /// <param name="game"> The game to associate with this <see cref="Menu{T}"/>. </param>
        public Menu(T game)
            : base(game)
        {
            device = game.GraphicsDevice;
            controlls = new GuiItemCollection(null);
            autoFocusTextbox = true;
            autoFocusDropDown = true;
        }

        /// <summary>
        /// Initializes the <see cref="SpriteBatch"/> used for drawing.
        /// Override to initialize <see cref="GuiItems"/>.
        /// </summary>
        public override void Initialize()
        {
            batch = new SpriteBatch(device);

            for (int i = 0; i < controlls.Count; i++)
            {
                controlls[i].Refresh();
            }

            base.Initialize();
        }

        /// <summary>
        /// Release the unmanaged and managed resources used by the <see cref="Menu{T}"/>.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged
        /// resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (int i = 0; i < controlls.Count; i++)
                {
                    controlls[i].Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets a control with a specified <see cref="GuiItem.Name"/> as a specified <see cref="GuiItem"/>.
        /// </summary>
        /// <typeparam name="TControll"> The <see cref="GuiItem"/> to cast to. </typeparam>
        /// <param name="name"> The specified <see cref="GuiItem.Name"/> to search for. </param>
        public TControll FindControl<TControll>(string name)
            where TControll : GuiItem
        {
            return (TControll)FindControl(name);
        }

        /// <summary>
        /// Gets a control with a specified name.
        /// </summary>
        /// <param name="name"> The specified <see cref="GuiItem.Name"/> to search for. </param>
        public GuiItem FindControl(string name)
        {
            return controlls.FirstOrDefault(c => c.Name == name);
        }

        /// <summary>
        /// Disables and hides the <see cref="Menu{T}"/>.
        /// </summary>
        public void Hide()
        {
            Enabled = false;
            Visible = false;
        }

        /// <summary>
        /// Enables and shows the <see cref="Menu{T}"/>.
        /// </summary>
        public void Show()
        {
            Enabled = true;
            Visible = true;
        }

        /// <summary>
        /// Updates the <see cref="Menu{T}"/> and its controlls.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                MouseState mState = Mouse.GetState();
                KeyboardState kState = Keyboard.GetState();
                float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

                for (int i = 0; i < controlls.Count; i++)
                {
                    GuiItem control = controlls[i];
                    if (control.SuppressUpdate)
                    {
                        control.SuppressUpdate = false;
                        continue;
                    }

                    control.Update_S(mState, kState, delta);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the <see cref="Menu{T}"/> and its controlls.
        /// </summary>
        public virtual void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                batch.Begin();

                for (int i = 0; i < controlls.Count; i++)
                {
                    if (controlls[i].SuppressDraw)
                    {
                        controlls[i].SuppressDraw = false;
                        continue;
                    }
                    controlls[i].Draw(batch);
                }

                batch.End();
            }
        }

        private void TextBox_Click(GuiItem sender, MouseState state)
        {
            if (autoFocusTextbox)
            {
                for (int i = 0; i < controlls.Count; i++)
                {
                    TextBox txt;
                    if ((txt = controlls[i] as TextBox) != null)
                    {
                        txt.Focused = txt.Name == sender.Name;
                        txt.Refresh();
                    }
                }
            }
        }

        private void DropDown_VisibilityChanged(GuiItem sender, bool visibility)
        {
            if (autoFocusDropDown && visibility)
            {
                for (int i = 0; i < controlls.Count; i++)
                {
                    DropDown dd;
                    if ((dd = controlls[i] as DropDown) != null)
                    {
                        if (dd.Name != sender.Name) dd.Hide();
                        dd.Refresh(); 
                    }
                }
            }
        }
    }
}