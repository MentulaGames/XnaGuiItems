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
            Button btn = AddButton();
            btn.AutoSize = true;
            btn.Text = "Click me";

            btn.LeftClick += (s, e) => { btn.Text = "Left"; };
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}