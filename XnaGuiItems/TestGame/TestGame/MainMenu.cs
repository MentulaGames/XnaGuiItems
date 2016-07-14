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

            DropDown dd = AddDropDown(AutoSize: true);
            dd.AddOption(new Lbl("Pickup", null), new Lbl("Item", Color.Yellow));
            dd.Refresh();
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}