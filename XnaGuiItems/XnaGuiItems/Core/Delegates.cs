﻿namespace Mentula.GuiItems.Core
{
    using System;

    /// <summary>
    /// Represents the method that will handle the default <see cref="GuiItem"/> events.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void GuiItemEventHandler(GuiItem sender, EventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Items.DropDown.IndexClick"/> event.
    /// </summary>
    public delegate void IndexedClickEventHandler(GuiItem sender, IndexedClickEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="GuiItem.Click"/> and <see cref="GuiItem.Hover"/> events.
    /// </summary>
    public delegate void MouseEventHandler(GuiItem sender, MouseEventArgs e);

    /// <summary>
    /// Represent the method that will handle the <see cref="Items.Slider.ValueChanged"/> event.
    /// </summary>
    /// <typeparam name="T"> The type of the value. </typeparam>
    public delegate void ValueChangedEventHandler<T>(GuiItem sender, ValueChangedEventArgs<T> e);
}