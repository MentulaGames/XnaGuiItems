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

            TabContainer tbc = AddTabContainer();
            tbc.AddTab("Tab0", Color.Blue);
            tbc.AddTab("Tab1", Color.Red);
            tbc.MoveRelative(Anchor.Center);

            Label lbl = AddLabel();
            lbl.AutoSize = true;
            lbl.Text = "Test Label";
            tbc.AddToTab("Tab1", lbl);

            base.Initialize(); 
        }
    }
}