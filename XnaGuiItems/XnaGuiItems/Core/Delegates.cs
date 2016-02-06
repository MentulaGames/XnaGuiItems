using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mentula.GuiItems.Core
{
    /// <summary>
    /// Represents the method that will handle the XnaMentula.GuiItems.Core.Mentula.GuiItems.BackColorChanged and XnaMentula.GuiItems.Mentula.GuiItems.ForeColorChanged events of the XnaMentula.GuiItems.GuiItem class.
    /// </summary>
    public delegate void ReColorEventHandler(GuiItem sender, Color newColor);

    /// <summary>
    /// Represents the method that will handle the XnaMentula.GuiItems.Core.Mentula.GuiItems.BackgroundImageChanged event of the XnaMentula.GuiItems.GuiItem class.
    /// </summary>
    public delegate void ReTextureEventhandler(GuiItem sender, Texture2D newtexture);

    /// <summary>
    /// Represents the method that will handle the XnaMentula.GuiItems.Core.Mentula.GuiItems.FontChanged event of the XnaMentula.GuiItems.GuiItem class.
    /// </summary>
    public delegate void ReFontEventHandler(GuiItem sender, SpriteFont newFont);

    /// <summary>
    /// Represents the method that will handle the XnaMentula.GuiItems.Core.Mentula.GuiItems.Move event of the XnaMentula.GuiItems.GuiItem class.
    /// </summary>
    public delegate void MoveEventHandler(GuiItem sender, Vector2 newPosition);

    /// <summary>
    /// Represents the method that will handle the XnaMentula.GuiItems.Core.Mentula.GuiItems.ParentChanged event of the XnaMentula.GuiItems.GuiItem class.
    /// </summary>
    public delegate void ParentChangeEventHandler(GuiItem sender, GuiItem newparent);

    /// <summary>
    /// Represents the method that will handle the XnaMentula.GuiItems.Core.Mentula.GuiItems.Resize event of the XnaMentula.GuiItems.GuiItem class.
    /// </summary>
    public delegate void ReSizeEventhandler(GuiItem sender, Rectangle newSize);

    /// <summary>
    /// Represents the method that will handle the XnaMentula.GuiItems.Core.Mentula.GuiItems.TextChanged event of the XnaMentula.GuiItems.GuiItem class.
    /// </summary>
    public delegate void TextChangedEventHandler(GuiItem sender, string newtext);

    /// <summary>
    /// Represents the method that will handle the XnaMentula.GuiItems.Core.Mentula.GuiItems.VisibilityChanged event of the XnaMentula.GuiItems.GuiItem class.
    /// </summary>
    public delegate void VisibilityChangedEventHandler(GuiItem sender, bool visibility);

    /// <summary>
    /// Represents the method that will handle the XnaMentula.GuiItems.Core.Mentula.GuiItems.Rotated event of the XnaMentula.GuiItems.GuiItem class.
    /// </summary>
    public delegate void RotationChangedEventHandler(GuiItem sender, float newRotaion);

    /// <summary>
    /// Represents the method that will handle the XnaMentula.GuiItems.Core.GuiItem.Click and XnaMentula.GuiItems.GuiItem.MouseHover events of the XnaMentula.GuiItems.GuiItem class.
    /// </summary>
    public delegate void MouseEventHandler(GuiItem sender, MouseState state);

    /// <summary>
    /// Represent the method that will handle the XnaMentula.GuiItems.Items.ValueChanged event of the XnaMentula.GuiItems.Items.Slider class.
    /// </summary>
    public delegate void ValueChangedEventHandler<T>(GuiItem sender, T newValue);
}
