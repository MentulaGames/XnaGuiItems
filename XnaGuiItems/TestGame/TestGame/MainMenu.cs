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
        public static readonly Size TxtSize = new Size(150, 25);

        public MainMenu(Game1 game)
            : base(game) { }

        public override void Initialize()
        {
            SetDefaultFont("ConsoleFont");
            base.Initialize(); 
        }
    }
}