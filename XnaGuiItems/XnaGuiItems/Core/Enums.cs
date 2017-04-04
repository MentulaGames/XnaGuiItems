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
    /// Specifies the flicker style for a GuiItem.
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
        CenterWidth = 1,                        //00000001
        /// <summary>
        /// The center of the screen on height.
        /// </summary>
        CenterHeight = 2,                       //00000010
        /// <summary>
        /// The absolute center of the screen (<see cref="CenterWidth"/> | <see cref="CenterHeight"/>).
        /// </summary>
        Center = CenterWidth | CenterHeight,    //00000011
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

    /// <summary>
    /// Specifies how an image is positioned or resized within a <see cref="Items.PictureBox"/>
    /// </summary>
    public enum ResizeMode : byte
    {
        /// <summary>
        /// The image is placed in the upper-left corner of the <see cref="Items.PictureBox"/>.
        /// The image is clipped if its larger than the <see cref="Items.PictureBox"/> it is contained in.
        /// </summary>
        Normal,
        /// <summary>
        /// The <see cref="Items.PictureBox"/> is sized equal to the size of the image it contains.
        /// </summary>
        AutoSize,
        /// <summary>
        /// The image is displayed in the center if the <see cref="Items.PictureBox"/> is larger than the image.
        /// If the image is larger than the <see cref="Items.PictureBox"/>,
        /// the picture is placed in the center of the <see cref="Items.PictureBox"/> and the outside edges are clipped.
        /// </summary>
        CenterImage,
        /// <summary>
        /// The image withing the <see cref="Items.PictureBox"/> is stretched or shrunk to fit the size of the <see cref="Items.PictureBox"/>.
        /// </summary>
        StretchImage,
        /// <summary>
        /// The size of the image is increased or decreased maintaining the size ratio.
        /// </summary>
        Zoom
    }

    /// <summary>
    /// Specifies which <see cref="char"/> the <see cref="Items.TextBox"/> should allow.
    /// </summary>
    public enum InputFlags : byte
    {
        /// <summary>
        /// No flags are set; everything may be added.
        /// </summary>
        NO_FLAGS = 0,
        /// <summary>
        /// No characters can be added.
        /// </summary>
        NO_CHARS = 1,
        /// <summary>
        /// No numbers can be added.
        /// </summary>
        NO_NUMS = 2,
        /// <summary>
        /// No special chars may be added.
        /// </summary>
        NO_SPEC = 4,
        /// <summary>
        /// No space may be added.
        /// </summary>
        NO_SPACE = 8,
        /// <summary>
        /// Only characters may be added.
        /// </summary>
        TEXT = NO_NUMS | NO_SPEC,
        /// <summary>
        /// Only numbers may be added.
        /// </summary>
        NUMS = NO_CHARS | NO_SPEC | NO_SPACE
    }

    internal enum ButtonStyle : byte
    {
        Default,
        Hover,
        Click
    }
}