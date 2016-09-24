using Mentula.GuiItems.Containers;
using Mentula.GuiItems.Core;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lbl = System.Collections.Generic.KeyValuePair<string, Microsoft.Xna.Framework.Color?>;

namespace TestGame
{
    public class MainMenu : Menu<Game1>
    {
        public static readonly Color ButtonBackColor = new Color(150, 150, 130, 150);
        public static readonly int TxtW = 150, TxtH = 25;

        public MainMenu(Game1 game)
            : base(game)
        {
            font = game.Content.Load<SpriteFont>("ConsoleFont");
        }

        public override void Initialize()
        {
            int txtHM = TxtH >> 1;

            DropDown dd = AddDropDown();
            dd.MoveRelative(Anchor.Middle);
            dd.AutoSize = true;

            dd.HeaderText = "Corpse";
            dd.AddOption("Test1");
            dd.AddOption("Test2");

            base.Initialize();
        }
    }
}