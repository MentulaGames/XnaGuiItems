using Microsoft.Xna.Framework;

namespace Mentula.GuiItems.Containers
{
    /// <summary>
    /// Defines an interface for menu's.
    /// </summary>
    public interface IMenu : IGameComponent, IUpdateable, IDrawable
    {
        /// <summary>
        /// Shows a specified menu and hides all others.
        /// </summary>
        void Show();
        /// <summary>
        /// Hides all menu's.
        /// </summary>
        void Hide();
    }
}