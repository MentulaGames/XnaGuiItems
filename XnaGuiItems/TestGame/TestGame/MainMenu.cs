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
                Position: new Vector2(10));

            Slider sld = AddSlider(
                Position: new Vector2(100),
                Width: 200,
                MaximumValue: 25);

            sld.ValueChanged += (sender, args) => lbl.Text = args.ToString();

            base.Initialize();
        }

        public void LoadFont(ContentManager content, string name)
        {
            font = content.Load<SpriteFont>(name);
        }
    }
}