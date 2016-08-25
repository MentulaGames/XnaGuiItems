using Microsoft.Xna.Framework;

namespace Mentula.GuiItems.Containers
{
    /// <summary>
    /// A simplified version of <see cref="Menu{T}"/>.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Menu : Menu<Game>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="game"> The game to associate with this <see cref="Menu"/>. </param>
        public Menu(Game game)
            : base(game)
        { }
    }
}