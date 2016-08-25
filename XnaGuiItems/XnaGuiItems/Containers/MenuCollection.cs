using Microsoft.Xna.Framework;
using System;
using KVP = System.Collections.Generic.KeyValuePair<string, Mentula.GuiItems.Containers.IMenu>;
using static Mentula.GuiItems.Utilities;

namespace Mentula.GuiItems.Containers
{
    /// <summary>
    /// A class for grouping <see cref="Menu{T}"/>.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class MenuCollection : GameComponent, IDrawable
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

        private KVP[] underlying;
        private bool visible;
        private int drawOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuCollection"/> class.
        /// </summary>
        /// <param name="game"> The game to associate with this <see cref="MenuCollection"/>. </param>
        public MenuCollection(Game game)
            : base(game)
        {
            underlying = new KVP[0];
        }

        /// <summary>
        /// Adds a <see cref="Menu{T}"/> to the collection.
        /// </summary>
        /// <typeparam name="TGame"> The type of <see cref="Game"/> refrenced by the <see cref="Menu{T}"/>. </typeparam>
        /// <param name="menu"> The <see cref="Menu{T}"/> to add. </param>
        /// <param name="key"> The name of the <see cref="Menu{T}"/>. </param>
        public void Add<TGame>(Menu<TGame> menu, string key)
            where TGame : Game
        {
            AddInternal(menu, key);
        }

        /// <summary>
        /// Initializes the <see cref="MenuCollection"/> and its child <see cref="Menu{T}"/>'s.
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
        /// Updates the <see cref="MenuCollection"/> and its underlying <see cref="Menu{T}"/>'s.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            for (int i = 0; i < underlying.Length; i++)
            {
                underlying[i].Value.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the <see cref="MenuCollection"/>'s <see cref="Menu{T}"/>'s.
        /// </summary>
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
        /// <exception cref="ArgumentException"> A <see cref="IMenu"/> with the specified name could not be found. </exception>
        public IMenu Get(string name)
        {
            return ThisGet(name.ToUpper());
        }

        private void AddInternal(IMenu cmp, string key)
        {
            int i = underlying.Length;
            Array.Resize(ref underlying, i + 1);
            underlying[i] = new KVP(key.ToUpper(), cmp);
        }

        private IMenu ThisGet(string key)
        {
            for (int i = 0; i < underlying.Length; i++)
            {
                KVP cur = underlying[i];
                if (cur.Key == key) return cur.Value;
            }

            throw new ArgumentException($"Cannot find menu with key: {key}!");
        }
    }
}
