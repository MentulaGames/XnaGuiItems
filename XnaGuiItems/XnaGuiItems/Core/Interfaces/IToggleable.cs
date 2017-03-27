namespace Mentula.GuiItems.Core
{
    /// <summary>
    /// Defines an interface for components that can be shown or hiden.
    /// </summary>
    /// <remarks>
    /// This interface adds usefull abstraction to functions that require only to be able to toggle the visible state of an object.
    /// In this framework it is used for all <see cref="GuiItem"/>.
    /// </remarks>
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