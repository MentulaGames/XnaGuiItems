using Mentula.GuiItems.Containers;
using Mentula.GuiItems.Core;
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
            TextBox txt = AddTextBox();
            txt.Size = new Size(150, 25);
            txt.Text = "UserName";
            txt.MultiLine = true;
            txt.Name = "TxtName";
            txt.MoveRelative(Anchor.Bottom | Anchor.Left);

            base.Initialize();
        }
    }
}