using Mentula.GuiItems.Containers;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using LABEL = System.Collections.Generic.KeyValuePair<string, Microsoft.Xna.Framework.Color?>;

namespace TestGame
{
    public class MainMenu : Menu
    {
        public MainMenu(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            Label lbl = AddLabel(
                Position: new Vector2(10, 10),
                Width: 200,
                Height: 200);

            for (int i = 0; i < 20; i++)
            {
                lbl.Text += i > 0 ? $"\n{i}" : i.ToString();
            }

            lbl.LineStart = 3;

            base.Initialize();
        }

        public void LoadFont(ContentManager content, string name)
        {
            font = content.Load<SpriteFont>(name);
        }
    }
}