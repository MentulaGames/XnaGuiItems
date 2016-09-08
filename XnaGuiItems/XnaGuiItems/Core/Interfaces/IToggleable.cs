namespace Mentula.GuiItems.Core.Interfaces
{
    /// <summary>
    /// Defines an interface for components that can be shown or hiden.
    /// </summary>
    public interface IToggleable
    {
        /// <summary>
        /// Shows the component.
        /// </summary>
        void Show();
        /// <summary>
        /// Hides the component.
        /// </summary>
        void Hide();
    }
}