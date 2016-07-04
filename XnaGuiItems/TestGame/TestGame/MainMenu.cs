using Mentula.GuiItems.Containers;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
    public class MainMenu : Menu<Game1>
    {
        public MainMenu(Game1 game)
            : base(game)
        {
            font = game.Content.Load<SpriteFont>("ConsoleFont");

            TabContainer tc = AddTabContainer();
            tc.AddTab("tab");
            tc.AddToTab("tab", new Label(device, font) { AutoSize = true, Text = "TEST", Position = new Vector2(0, 00) });
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}