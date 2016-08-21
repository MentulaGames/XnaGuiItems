using Mentula.GuiItems.Containers;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lbl = System.Collections.Generic.KeyValuePair<string, Microsoft.Xna.Framework.Color?>;

namespace TestGame
{
    public class MainMenu : Menu<Game1>
    {
        public MainMenu(Game1 game)
            : base(game)
        {
            font = game.Content.Load<SpriteFont>("ConsoleFont");
        }

        public override void Initialize()
        {
            DropDown dd = AddDropDown();
            dd.Name = "dd1";
            dd.Position = new Vector2(10, 10);
            dd.AddOption("Test");

            DropDown dd2 = AddDropDown();
            dd.Name = "dd2";
            dd2.Position = new Vector2(100, 10);
            dd2.AddOption("test");


            dd2.Show();

            base.Initialize();
        }
    }
}