using Mentula.GuiItems.Containers;
using Mentula.GuiItems.Core;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;

namespace TestGame
{
    public class MainMenu : Menu<Game1>
    {
        public MainMenu(Game1 game)
            : base(game) { }

        public override void Initialize()
        {
            SetDefaultFont("GuiFont");

            Label lblError = AddLabel();
            lblError.Name = "LblError";
            lblError.Width = 150;
            lblError.Height = 25;
            lblError.AutoSize = true;
            lblError.BackColor = Color.Transparent;
            lblError.ForeColor = Color.Red;
            lblError.MoveRelative(Anchor.CenterWidth, y: ScreenHeightMiddle + (25 >> 1));

            base.Initialize(); 
        }
    }
}