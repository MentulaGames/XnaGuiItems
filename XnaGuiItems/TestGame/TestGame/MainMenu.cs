using Mentula.GuiItems.Containers;
using Mentula.GuiItems.Core;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
    public class MainMenu : Menu<Game1>
    {
        public static readonly Color ButtonBackColor = new Color(150, 150, 130, 150);
        public static readonly Size TxtSize = new Size(150, 25);

        public MainMenu(Game1 game)
            : base(game) { }

        public override void Initialize()
        {
            SetDefaultFont("ConsoleFont");

            DropDown dd = AddDropDown();
            dd.AutoSize = true;
            dd.AddOption(
                new Pair("Take", RGB(172, 160, 130)),
                new Pair("Iron bar", RGB(165, 193, 196)));

            base.Initialize(); 
        }
    }
}