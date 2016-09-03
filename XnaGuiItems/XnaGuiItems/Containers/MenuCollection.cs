using Microsoft.Xna.Framework;
using System;
using KVP = System.Collections.Generic.KeyValuePair<string, Mentula.GuiItems.Containers.IMenu>;
using static Mentula.GuiItems.Utilities;
using Mentula.GuiItems.Core;

namespace Mentula.GuiItems.Containers
{
    /// <summary>
    /// A class for grouping <see cref="Menu{T}"/>.
    /// </summary>
    /// <typeparam name="TGame"> The specified game class. </typeparam>
    /// <remarks>
    /// This object is used to house a unspecified amount of IMenu's.
    /// It supplies easy methods for adding menus to the collection and
    /// methods for handeling menu switching.
    /// </remarks>
    /// <example>
    /// This example will show how a MenuCollection should be used in your Xna project.
    /// Here the Collection is used as a base class for an easier facade.
    /// 
    /// <code>
    /// public sealed class ClientMenus : <![CDATA[MenuCollection<MainGame>]]>
    /// {
    ///     // Every menus has its own propertie to supply easy acces to them.
    ///     public MainMenu MainMenu { get; private set; }
    ///     public SingleplayerMenu SinglePlayer { get; private set; }
    ///     public MultiplayerMenu MultiPlayer { get; private set; }
    ///     public GuiMenu Gui { get; private set; }
    ///     public LoadingMenu Loading { get; private set; }
    ///     public OptionsMenu Options { get; private set; }
    /// 
    ///     // The underlying keys for getting a specified menus.
    ///     // This should however be unnessesary because
    ///     // the menus are more easely obtained by using the properties specified above.
    ///     private const string MAIN = "Main";
    ///     private const string SINGLE = "SinglePlayer";
    ///     private const string MULTI = "MultiPlayer";
    ///     private const string GUI = "Gui";
    ///     private const string LOAD = "Loading";
    ///     private const string OPT = "Options";
    /// 
    ///     public ClientMenus(MainGame game)
    ///         : base(game)
    ///     { }
    /// 
    ///     // Initializes the underlying menus with the specified game and name.
    ///     public override void Initialize()
    ///     {
    ///         Add(MainMenu = new MainMenu(Game), MAIN);
    ///         Add(SinglePlayer = new SingleplayerMenu(Game), SINGLE);
    ///         Add(MultiPlayer = new MultiplayerMenu(Game), MULTI);
    ///         Add(Gui = new GuiMenu(Game), GUI);
    ///         Add(Loading = new LoadingMenu(Game), LOAD);
    ///         Add(Options = new OptionsMenu(Game), OPT);
    /// 
    ///         base.Initialize();
    ///     }
    ///     
    ///     // Shows a specified menu with a given GameState.
    ///     public void Show(GameState state)
    ///     {
    ///         Show(FromGameState(state));
    ///     }
    /// 
    ///     // Converts the GameState to the string key that represents the actual menu.
    ///     private static string FromGameState(GameState state)
    ///     {
    ///         switch (state)
    ///         {
    ///             case GameState.MainMenu: return MAIN;
    ///             case GameState.SingleplayerMenu: return SINGLE;
    ///             case GameState.MultiplayerMenu: return MULTI;
    ///             case GameState.OptionsMenu: return OPT;
    ///             case GameState.Loading: return LOAD;
    ///             case GameState.Game: return GUI;
    ///             default: return string.Empty;
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class MenuCollection<TGame> : MentulaGameComponent<TGame>, IDrawable
        where TGame : Game
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
        /// Occures when the value of <see cref="DrawOrder"/> is changed.
        /// </summary>
        public event EventHandler<EventArgs> DrawOrderChanged;
        /// <summary>
        /// Occures when the value of <see cref="Visible"/> is changed.
        /// </summary>
        public event EventHandler<EventArgs> VisibleChanged;

        /// <summary>
        /// Gets a <see cref="IMenu"/> with a specified name.
        /// </summary>
        /// <param name="key"> The name of the <see cref="IMenu"/>. </param>
        /// <exception cref="ArgumentException"> A <see cref="IMenu"/> with the specified name could not be found. </exception>
        public IMenu this[string key] { get { return Get(key); } }

        private KVP[] underlying;
        private bool visible;
        private int drawOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuCollection{T}"/> class.
        /// </summary>
        /// <param name="game"> The game to associate with this <see cref="MenuCollection{T}"/>. </param>
        public MenuCollection(TGame game)
            : base(game)
        {
            underlying = new KVP[0];
        }

        /// <summary>
        /// Adds a <see cref="IMenu"/> to the collection.
        /// </summary>
        /// <param name="menu"> The <see cref="IMenu"/> to add. </param>
        /// <param name="key"> The name of the <see cref="IMenu"/>. </param>
        public void Add(IMenu menu, string key)
        {
            int i = underlying.Length;
            Array.Resize(ref underlying, i + 1);
            underlying[i] = new KVP(key.ToUpper(), menu);
        }

        /// <summary>
        /// Adds multiple <see cref="IMenu"/> to the collection.
        /// </summary>
        /// <param name="menus"> The <see cref="IMenu"/> to add with there given names. </param>
        public void AddRange(params KVP[] menus)
        {
            int i = underlying.Length;
            Array.Resize(ref underlying, i + menus.Length);

            for (int j = 0; i < underlying.Length; i++, j++)
            {
                KVP cur = menus[j];
                underlying[i] = new KVP(cur.Key.ToUpper(), cur.Value);
            }
        }

        /// <summary>
        /// Initializes the <see cref="MenuCollection{T}"/> and its child <see cref="Menu{T}"/>'s.
        /// </summary>
        public override void Initialize()
        {
            for (int i = 0; i < underlying.Length; i++)
            {
                underlying[i].Value.Initialize();
            }

            base.Initialize();
        }

        /// <summary>
        /// Updates the <see cref="MenuCollection{T}"/> and its underlying <see cref="Menu{T}"/>'s.
        /// </summary>
        /// <param name="gameTime"> Time elapsed since the last call to Update. </param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                for (int i = 0; i < underlying.Length; i++)
                {
                    underlying[i].Value.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the <see cref="MenuCollection{T}"/>'s <see cref="Menu{T}"/>'s.
        /// </summary>
        /// <param name="gameTime"> Time elapsed since the last call to Draw. </param>
        public void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                for (int i = 0; i < underlying.Length; i++)
                {
                    underlying[i].Value.Draw(gameTime);
                }
            }
        }

        /// <summary>
        /// Shows a specified <see cref="Menu{T}"/> and hides all others.
        /// </summary>
        /// <param name="menu"> The name of the <see cref="Menu{T}"/> to show. </param>
        public void Show(string menu)
        {
            Visible = true;
            menu = menu.ToUpper();

            for (int i = 0; i < underlying.Length; i++)
            {
                KVP cur = underlying[i];
                if (cur.Key == menu) cur.Value.Show();
                else cur.Value.Hide();
            }
        }

        /// <summary>
        /// Hides all <see cref="Menu{T}"/>'s.
        /// </summary>
        public void Hide()
        {
            Visible = false;

            for (int i = 0; i < underlying.Length; i++)
            {
                underlying[i].Value.Hide();
            }
        }

        /// <summary>
        /// Gets a <see cref="IMenu"/> with a specified name.
        /// </summary>
        /// <param name="name"> The name of the <see cref="IMenu"/>. </param>
        /// <typeparam name="TMenu"> The type of <see cref="IMenu"/> to cast to. </typeparam>
        /// <returns> The found menu casted to the specified type. </returns>
        /// <exception cref="ArgumentException"> A <see cref="IMenu"/> with the specified name could not be found. </exception>
        /// <exception cref="InvalidCastException"> Cannot cast found menu to specified menu type. </exception>
        public TMenu Get<TMenu>(string name)
            where TMenu : IMenu
        {
            return (TMenu)Get(name);
        }

        /// <summary>
        /// Gets a <see cref="IMenu"/> with a specified name.
        /// </summary>
        /// <param name="name"> The name of the <see cref="IMenu"/>. </param>
        /// <returns> The found menu. </returns>
        /// <exception cref="ArgumentException"> A <see cref="IMenu"/> with the specified name could not be found. </exception>
        public IMenu Get(string name)
        {
            name = name.ToUpper();

            for (int i = 0; i < underlying.Length; i++)
            {
                KVP cur = underlying[i];
                if (cur.Key == name) return cur.Value;
            }

            throw new ArgumentException($"Cannot find menu with key: {name}!");
        }
    }
}
