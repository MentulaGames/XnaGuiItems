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
        public static readonly Color ButtonBackColor = RGBA(150, 150, 130, 150);
        public static readonly Size TxtSize = new Size(150, 25);

        public MainMenu(Game1 game)
            : base(game) { }

        public override void Initialize()
        {
            SetDefaultFont("ConsoleFont");

            FpsCounter fps = AddFpsCounter();
            fps.MoveRelative(Anchor.Top | Anchor.Left);

            TextBox txt = AddTextBox();
            txt.MoveRelative(Anchor.Center);

            base.Initialize(); 
        }
    }
}