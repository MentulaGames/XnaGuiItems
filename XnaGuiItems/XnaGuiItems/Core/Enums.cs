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

    internal enum ButtonStyle : byte
    {
        Default,
        Hover,
        Click
    }
}