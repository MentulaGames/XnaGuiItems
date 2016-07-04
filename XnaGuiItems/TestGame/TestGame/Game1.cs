using Microsoft.Xna.Framework;

namespace TestGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private MainMenu menu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Components.Add(menu = new MainMenu(this));
            base.Initialize();
        }
    }
}