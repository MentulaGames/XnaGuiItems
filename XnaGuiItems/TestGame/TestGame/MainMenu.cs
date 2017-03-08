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
            dd.MoveRelative(Anchor.Center);
            dd.AutoSize = true;
            dd.AddOption(Pair.RunescapeAction("Take"), Pair.RunescapeItem("Item0"));
            dd.AddOption(Pair.RunescapeAction("Take"), Pair.RunescapeItem("Item1"));
            dd.AddOption(Pair.RunescapeAction("Walk here"));
            dd.AddOption(Pair.RunescapeAction("Examine"), Pair.RunescapeItem("Item0"));
            dd.AddOption(Pair.RunescapeAction("Examine"), Pair.RunescapeItem("Item1"));
            dd.AddOption(Pair.RunescapeAction("Cancel"));

            base.Initialize(); 
        }
    }
}