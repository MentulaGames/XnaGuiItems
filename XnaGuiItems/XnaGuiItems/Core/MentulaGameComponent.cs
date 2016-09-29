namespace Mentula.GuiItems.Core
{
    using Microsoft.Xna.Framework;

    /// <summary> Base class for all Mentula game components. </summary>
    /// <typeparam name="TGame"> The specified game class. </typeparam>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public abstract class MentulaGameComponent<TGame> : GameComponent
        where TGame : Game
    {
        /// <summary> Gets the Game associated with this GameComponent. </summary>
        new public TGame Game { get { return (TGame)base.Game; } }

        /// <summary> Initializes a new instance of the <see cref="MentulaGameComponent{T}"/> class. </summary>
        /// <param name="game"> The game associated with this component. </param>
        protected MentulaGameComponent(TGame game)
            : base(game)
        { }
    }
}