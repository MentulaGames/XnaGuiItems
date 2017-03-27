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

            DropDown dd = AddDropDown();
            dd.Name = $"id{1}";
            dd.AutoSize = true;
            dd.AddOption(
                new Pair("Pick up", Color.Red),
                new Pair($"{"TestName"}", Color.Blue));

            base.Initialize();
        }
    }
}