using Mentula.GuiItems.Containers;
using Mentula.GuiItems.Core;
using Mentula.GuiItems.Core.Structs;
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

            TabContainer tc = AddTabContainer();
            tc.Size = new Size(ScreenWidth / 4, 100);
            tc.AddTab(new Pair("First"));

            base.Initialize(); 
        }
    }
}