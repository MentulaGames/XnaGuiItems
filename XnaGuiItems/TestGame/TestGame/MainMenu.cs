using Mentula.GuiItems.Containers;
using Mentula.GuiItems.Core;
using Mentula.GuiItems.Items;

namespace TestGame
{
    public class MainMenu : Menu<Game1>
    {
        public MainMenu(Game1 game)
            : base(game) { }

        public override void Initialize()
        {
            SetDefaultFont("GuiFont");

            DropDown dd = AddDropDown();
            dd.AutoSize = true;
            dd.MoveRelative(Anchor.Center);
            dd.AddOption("Pickup", "Item");
            dd.AddOption("Examine", "Item");
            dd.AddOption("Walk here");
            dd.AddOption("Cancel");

            base.Initialize(); 
        }
    }
}