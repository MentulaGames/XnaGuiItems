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

            base.Initialize(); 
        }

        private void OnIndexClick(GuiItem sender, IndexedClickEventArgs e)
        {
            switch (e.Index)
            {
                case 5:
                    sender.Hide();
                    break;
            }
        }
    }
}