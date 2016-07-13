using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mentula.GuiItems.Items;

namespace Mentula.GuiItems.Core
{
    /// <summary>
    /// Represents the method that will handle the <see cref="GuiItem.BackColorChanged"/> and <see cref="GuiItem.ForeColorChanged"/> events.
    /// </summary>
    public delegate void ReColorEventHandler(GuiItem sender, Color newColor);

    /// <summary>
    /// Represents the method that will handle the <see cref="GuiItem.BackgroundImageChanged"/> event.
    /// </summary>
    public delegate void ReTextureEventhandler(GuiItem sender, Texture2D newtexture);

    /// <summary>
    /// Represents the method that will handle the <see cref="Label.FontChanged"/> event.
    /// </summary>
    public delegate void ReFontEventHandler(Label sender, SpriteFont newFont);

    /// <summary>
    /// Represents the method that will handle the <see cref="GuiItem.Move"/> event.
    /// </summary>
    public delegate void MoveEventHandler(GuiItem sender, Vector2 newPosition);

    /// <summary>
    /// Represents the method that will handle the <see cref="GuiItem.Resize"/> event.
    /// </summary>
    public delegate void ReSizeEventhandler(GuiItem sender, Rectangle newSize);

    /// <summary>
    /// Represents the method that will handle the <see cref="Label.TextChanged"/> and <see cref="GuiItem.NameChanged"/> events.
    /// </summary>
    public delegate void TextChangedEventHandler(GuiItem sender, string newtext);

    /// <summary>
    /// Represents the method that will handle the <see cref="GuiItem.VisibilityChanged"/> event.
    /// </summary>
    public delegate void VisibilityChangedEventHandler(GuiItem sender, bool visibility);

    /// <summary>
    /// Represents the method that will handle the <see cref="GuiItem.Rotated"/> event.
    /// </summary>
    public delegate void RotationChangedEventHandler(GuiItem sender, float newRotaion);

    /// <summary>
    /// Represents the method that will handle the <see cref="GuiItem.Click"/> and <see cref="GuiItem.Hover"/> events.
    /// </summary>
    public delegate void MouseEventHandler(GuiItem sender, MouseState state);

    /// <summary>
    /// Represent the method that will handle the <see cref="Slider.ValueChanged"/> event.
    /// </summary>
    public delegate void ValueChangedEventHandler<T>(GuiItem sender, T newValue);
}
