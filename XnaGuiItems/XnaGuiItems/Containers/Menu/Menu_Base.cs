using Mentula.GuiItems.Core;
using Mentula.GuiItems.Core.Interfaces;
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
    /// <typeparam name="T"> The type of <see cref="Game"/> to refrence to. </typeparam>
    /// <remarks>
    /// This object is used to handle default operations when dealing with GuiItems.
    /// Properties like <see cref="ScreenWidthMiddle"/> are also added for extra support when adding GuiItems.
    /// As default the <see cref="Menu{T}"/> will also suppress refresh calls to controlls whilst they are being initialzes
    /// making the process of initialization faster when using a <see cref="Menu{T}"/>.
    /// </remarks>
    /// <example>
    /// In this example a main menu is created.
    /// The two properties are public for use in other menu's.
    /// Line 9 makes sure that when the menu becomes visible the mouse will be useable.
    /// 
    /// In initialize the default font option is used so we don't have to specify one every time we make a button.
    /// A background is loaded and four buttons are made.
    /// 
    /// <code>
    /// public sealed class MainMenu : <![CDATA[Menu<MainGame>]]>
    /// {
    ///     public static readonly Color ButtonBackColor = new Color(150, 150, 130, 150);
    ///     public static readonly int TxtW = 150, TxtH = 25;
    ///
    ///     public MainMenu(MainGame game)
    ///         : base(game)
    ///     {
    ///          VisibleChanged += (s, e) => { if (Visible) { Game.IsMouseVisible = true; } };
    ///     }
    ///
    ///     public override void Initialize()
    ///     {
    ///         font = Game.vGraphics.fonts["MenuFont"];
    ///         int txtHM = TxtH >> 1;
    ///
    ///         GuiItem bg = AddGuiItem();
    ///         bg.BackgroundImage = <![CDATA[Game.Content.Load<Texture2D>("Utilities\\MainBackground");]]>
    ///         bg.Enabled = false;
    ///
    ///         Button btnSingleplayer = AddDefButton();
    ///         btnSingleplayer.MoveRelative(Anchor.MiddleWidth, y: ScreenHeightMiddle + txtHM * 4);
    ///         btnSingleplayer.Text = "Singleplayer";
    ///
    ///         Button btnMultiplayer = AddDefButton();
    ///         btnMultiplayer.MoveRelative(Anchor.MiddleWidth, y: ScreenheightMiddle + txtHM * 8);
    ///         btnMultiplayer.Text = "Multiplayer";
    ///
    ///         Button btnOptions = AddDefButton();
    ///         btnOptions.MoveRelative(Anchor.MiddleWidth, y: ScreenheightMiddle + txtHM * 12);
    ///         btnOptions.Text = "Options";
    ///
    ///         Button btnQuit = AddButton();
    ///         btnQuit.MoveRelative(Anchor.MiddleWidth, y: ScreenheightMiddle + txtHM * 16);
    ///         btnQuit.Text = "Quit";
    ///
    ///         btnSingleplayer.LeftClick += (sender, args) => Game.SetState(GameState.SingleplayerMenu);
    ///         btnMultiplayer.LeftClick += (sender, args) => Game.SetState(GameState.MultiplayerMenu);
    ///         btnOptions.LeftClick += (sender, args) => Game.SetState(GameState.OptionsMenu);
    ///         btnQuit.LeftClick += (sender, args) => Game.Exit();
    ///
    ///         base.Initialize();
    ///     }
    /// 
    ///     private Button AddDefButton()
    ///     {
    ///         Button result = AddButton();
    ///         result.Width = TxtW;
    ///         result.Height = TxtH;
    ///         result.BackColor = ButtonBackColor;
    ///         return result;
    ///     }
    /// }
    /// </code>
    /// </example>
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
        /// Default value = <see langword="true"/>.
        /// </summary>
        protected bool autoFocusTextbox;
        /// <summary>
        /// Indicates whether the <see cref="Menu{T}"/> should handle dropdown focusing.
        /// Default value = <see langword="true"/>.
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu{T}"/> class.
        /// </summary>
        /// <param name="game"> The game to associate with this <see cref="Menu{T}"/>. </param>
        public Menu(T game)
            : this(game, true)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu{T}"/> class.
        /// </summary>
        /// <param name="game"> The game to associate with this <see cref="Menu{T}"/>. </param>
        /// <param name="allowRefreshSuppression"> 
        /// Whether the <see cref="Menu{T}"/> is allowed to suppress refresh calls
        /// when the controlls are being initialized.
        ///  </param>
        public Menu(T game, bool allowRefreshSuppression)
            : base(game)
        {
            device = game.GraphicsDevice;
            controlls = new GuiItemCollection(null);

            autoFocusTextbox = true;
            autoFocusDropDown = true;
            if (allowRefreshSuppression) suppressRefresh = true;
        }

        /// <summary>
        /// Initializes the <see cref="SpriteBatch"/> used for drawing.
        /// Override to initialize <see cref="GuiItems"/>.
        /// </summary>
        public override void Initialize()
        {
            batch = new SpriteBatch(device);
            suppressRefresh = false;

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
        /// <typeparam name="TControl"> The <see cref="GuiItem"/> to cast to. </typeparam>
        /// <param name="name"> The specified <see cref="GuiItem.Name"/> to search for. </param>
        /// /// <returns> The <see cref="GuiItem"/> that was found as type TControl; otherwise null. </returns>
        public TControl FindControl<TControl>(string name)
            where TControl : GuiItem
        {
            return (TControl)FindControl(name);
        }

        /// <summary>
        /// Gets a control with a specified name.
        /// </summary>
        /// <param name="name"> The specified <see cref="GuiItem.Name"/> to search for. </param>
        /// <returns> The <see cref="GuiItem"/> that was found; otherwise null. </returns>
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
        /// <param name="gameTime"> Time elapsed since the last call to Update. </param>
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
        /// <param name="gameTime"> Time elapsed since the last call to Draw. </param>
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