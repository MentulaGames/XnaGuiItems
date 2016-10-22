namespace Mentula.GuiItems.Core
{
    using System;

    /// <summary>
    /// Represents the method that will handle the default <see cref="GuiItem"/> events.
    /// </summary>
    /// <param name="sender"> The GuiItem that raised the event. </param>
    /// <param name="e"> Additional information about the event raised. </param>
    /// <remarks>
    /// The <see cref="EventArgs"/> will almost always be empty if the call is internal.
    /// The reason this parameter is required if for the option of extra data given by classes that modify functions in this framework.
    /// This makes sure that modifications on this framework do not need to hack in additional data for events.
    /// </remarks>
    public delegate void GuiItemEventHandler(GuiItem sender, EventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Items.DropDown.IndexClick"/> event.
    /// </summary>
    /// <param name="sender"> The GuiItem that raised the event. </param>
    /// <param name="e"> Additional information about the event raised, this contains the index clicked. </param>
    public delegate void IndexedClickEventHandler(GuiItem sender, IndexedClickEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="GuiItem.Click"/> and <see cref="GuiItem.Hover"/> events.
    /// </summary>
    /// <param name="sender"> The GuiItem that raised the event. </param>
    /// <param name="e"> Additional information about the event raised, this will contain the state of the mouse at the time of the invokation of the event. </param>
    public delegate void MouseEventHandler(GuiItem sender, MouseEventArgs e);

    /// <summary>
    /// Represent the method that will handle the <see cref="Items.Slider.ValueChanged"/> event.
    /// </summary>
    /// <typeparam name="T"> The type of the value. </typeparam>
    /// <param name="sender"> The GuiItem that raised the event. </param>
    /// <param name="e"> Additional information about the event raised, this will contain the old and new value of the changed field. </param>
    public delegate void ValueChangedEventHandler<T>(GuiItem sender, ValueChangedEventArgs<T> e);
}