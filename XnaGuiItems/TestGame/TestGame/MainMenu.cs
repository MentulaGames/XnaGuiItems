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
            dd.AutoSize = true;
            dd.IndexClick += OnIndexClick;
            dd.MoveRelative(Anchor.Center);
            dd.AddOption("Pickup", "Item");
            dd.AddOption("Examine", "Item");
            dd.AddOption("Cancel");

            base.Initialize(); 
        }

        private void OnIndexClick(GuiItem sender, IndexedClickEventArgs e)
        {
            switch (e.Index)
            {
                case (2):
                    sender.Hide();
                    break;
            }
        }
    }
}