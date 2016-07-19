using Microsoft.Xna.Framework;

namespace Mentula.GuiItems.Core
{
    /// <summary> Base class for all Mentula game components. </summary>
    /// <typeparam name="T"> The specified game class. </typeparam>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public abstract class MentulaGameComponent<T> : GameComponent
        where T : Game
    {
        /// <summary> Gets the Game associated with this GameComponent. </summary>
        new public T Game { get { return (T)base.Game; } }

        /// <summary> Initializes a new instance of the <see cref="MentulaGameComponent{T}"/> class. </summary>
        /// <param name="game"> The game associated with this component. </param>
        protected MentulaGameComponent(T game)
            : base(game)
        { }
    }
}