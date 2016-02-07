using Microsoft.Xna.Framework;

namespace TestGame
{
    public class Game1 : Game
    {
        public MainMenu mm;

        private GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Components.Add(mm = new MainMenu(this));
            mm.Show();
            mm.LoadFont(Content, "ConsoleFont");
            base.Initialize();
        }
    }
}