using Microsoft.Xna.Framework;

namespace Mentula.GuiItems.Core
{
    public abstract class MentulaGameComponent<T> : GameComponent
        where T : Game
    {
        new public T Game { get { return (T)base.Game; } }

        protected MentulaGameComponent(T game)
            : base(game)
        { }
    }
}