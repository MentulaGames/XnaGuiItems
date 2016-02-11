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
            AddTextBox(
                Name: "TxtName",
                AutoSize: true,
                Multiline: true,
                Position: new Vector2(25, 250),
                Height: 150);

            AddSlider(
                Name: "SldName",
                MaximumValue: 1,
                Position: new Vector2(20, 250),
                Rotation: 1.57f,
                Width: 100);

            base.Initialize();
        }

        public void LoadFont(ContentManager content, string name)
        {
            font = content.Load<SpriteFont>(name);
        }

        public override void Update(GameTime gameTime)
        {
            Slider sld = FindControl<Slider>("SldName");
            TextBox txt = FindControl<TextBox>("TxtName");

            sld.MaximumValue = txt.GetLineCount();
            txt.LineStart = sld.Value;

            base.Update(gameTime);
        }
    }
}