namespace Mentula.GuiItems.Containers
{
    using Mentula.GuiItems.Core;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Runtime.CompilerServices;

    public partial class Menu<T> : DrawableMentulaGameComponent<T>
        where T : Game
    {
        /// <summary>
        /// The center width of the viewport.
        /// </summary>
        public int ScreenWidthMiddle { get { return ScreenWidth >> 1; } }
        /// <summary>
        /// The center height of the viewport.
        /// </summary>
        public int ScreenHeightMiddle { get { return ScreenHeight >> 1; } }
        /// <summary>
        /// The width of the viewport.
        /// </summary>
        public int ScreenWidth { get { return batch.GraphicsDevice.Viewport.Width; } }
        /// <summary>
        /// The Height of the viewport.
        /// </summary>
        public int ScreenHeight { get { return batch.GraphicsDevice.Viewport.Height; } }

        /// <summary>
        /// Loads a specified <see cref="SpriteFont"/> from <see cref="Game.Content"/>.
        /// </summary>
        /// <param name="assetName"> Asset name, relative to the loader root directory, and not including the .xnb extension. </param>
        /// <returns> The specified <see cref="SpriteFont"/>. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpriteFont LoadFont(string assetName) => Game.Content.Load<SpriteFont>(assetName);

        /// <summary>
        /// Sets the default <see cref="SpriteFont"/>.
        /// </summary>
        /// <param name="assetName"> Asset name, relative to the loader root directory, and not including the .xnb extension. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetDefaultFont(string assetName) => font = LoadFont(assetName);

        /// <summary>
        /// Loads a specified <see cref="Texture2D"/> from <see cref="Game.Content"/>.
        /// </summary>
        /// <param name="assetName"> Asset name, relative to the loader root directory, and not including the .xnb extension. </param>
        /// <returns> The specified <see cref="Texture2D"/>. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Texture2D LoadTexture(string assetName) => Game.Content.Load<Texture2D>(assetName);

        /// <summary>
        /// Draws the specified string with the default font.
        /// </summary>
        /// <param name="text"> The string to be displayed. </param>
        /// <param name="position"> The upper left corner of the strings position. </param>
        /// <param name="color"> The color of the string. </param>
        /// <remarks>
        /// This method is used for drawing debug values.
        /// Drawing with the underlying spritebatch in menu's is not recomended.
        /// </remarks>
        protected void DrawString(string text, Vector2 position, Color color)
        {
            batch.Begin();
            batch.DrawString(font, text, position, color);
            batch.End();
        }

        /// <summary>
        /// Gets a specific <see cref="Color"/> from red, green and blue values.
        /// </summary>
        /// <param name="r"> The red component of the <see cref="Color"/>. </param>
        /// <param name="g"> The green component of the <see cref="Color"/>. </param>
        /// <param name="b"> The blue component of the <see cref="Color"/>. </param>
        /// <returns> The specified premultiplied <see cref="Color"/>. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color RGB(int r, int g, int b) => Color.FromNonPremultiplied(r, g, b, 255);
    }
}
