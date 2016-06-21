using Mentula.GuiItems.Containers;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
    public class MainMenu : Menu
    {
        public MainMenu(Game game)
            : base(game)
        {
            font = game.Content.Load<SpriteFont>("ConsoleFont");
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}