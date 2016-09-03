namespace Mentula.GuiItems.Core
{
    /// <summary>
    /// Specifies the border style for a GuiItem.
    /// </summary>
    public enum BorderStyle : byte
    {
        /// <summary>
        /// No Border.
        /// </summary>
        None = 0,
        /// <summary>
        /// A single-line border.
        /// </summary>
        FixedSingle = 1,
        /// <summary>
        /// A three-dimensional border.
        /// </summary>
        Fixed3D = 2,
    }

    /// <summary>
    /// Specifies the flicker syle for a GuiItem.
    /// </summary>
    public enum FlickerStyle : byte
    {
        /// <summary>
        /// Flickers once every 2 seconds.
        /// </summary>
        Slow,
        /// <summary>
        /// Flickers once every second.
        /// </summary>
        Normal,
        /// <summary>
        /// Flickers twice every second.
        /// </summary>
        Fast,
        /// <summary>
        /// No flickering.
        /// </summary>
        None
    }

    /// <summary>
    /// Indicates a specific mouse click.
    /// </summary>
    public enum MouseClick : byte
    {
        /// <summary>
        /// A default click.
        /// </summary>
        Default,
        /// <summary>
        /// A left click.
        /// </summary>
        Left,
        /// <summary>
        /// A right click.
        /// </summary>
        Right,
        /// <summary>
        /// A double click.
        /// </summary>
        Double
    }

    /// <summary>
    /// Specified the relative position the <see cref="GuiItem"/> should take.
    /// </summary>
    public enum Anchor : byte
    {
        /// <summary>
        /// The default value.
        /// </summary>
        None = 0,                               //00000000
        /// <summary>
        /// The center of the screen on width.
        /// </summary>
        MiddleWidth = 1,                        //00000001
        /// <summary>
        /// The center of the screen on height.
        /// </summary>
        MiddelHeight = 2,                       //00000010
        /// <summary>
        /// The absolute center of the screen (<see cref="MiddleWidth"/> | <see cref="MiddelHeight"/>).
        /// </summary>
        Middle = MiddleWidth | MiddelHeight,    //00000011
        /// <summary>
        /// The left of the screen.
        /// </summary>
        Left = 4,                               //00000100
        /// <summary>
        /// The right of the screen.
        /// </summary>
        Right = 8,                              //00001000
        /// <summary>
        /// The top of the screen.
        /// </summary>
        Top = 16,                               //00010000
        /// <summary>
        /// The bottom of the screen.
        /// </summary>
        Bottom = 32                             //00100000
    }

    internal enum ButtonStyle : byte
    {
        Default,
        Hover,
        Click
    }
}