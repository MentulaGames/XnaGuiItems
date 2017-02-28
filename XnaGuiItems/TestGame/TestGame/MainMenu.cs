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

            Label lbl = AddLabel();
            lbl.AutoSize = true;
            lbl.BackColor = RGB(255, 0, 0);
            lbl.Text = "TestLabel";
            lbl.MoveRelative(Anchor.Middle);

            base.Initialize(); 
        }
    }
}