using Microsoft.Xna.Framework;

namespace Mentula.GuiItems.Containers
{
    /// <summary>
    /// Defines an interface for menu's.
    /// </summary>
    public interface IMenu : IGameComponent, IUpdateable, IDrawable
    {
        /// <summary>
        /// Shows the menu.
        /// </summary>
        void Show();
        /// <summary>
        /// Hides the menu.
        /// </summary>
        void Hide();
    }
}