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
            :base(game)
        {
            Visible = true;
        }

        public override void Initialize()
        {
            const string DD_OPTIONS = "DD_Options";
            AddDropDown(
                Name: DD_OPTIONS,
                Position: new Vector2(10),
                AutoSize: true);

            FindControl<DropDown>(DD_OPTIONS).AddOption(new LABEL("Attack", null), new LABEL("Chaos dwarf hand cannoneer", Color.Yellow), new LABEL("(Level: 100)", Color.LimeGreen));
            FindControl<DropDown>(DD_OPTIONS).AddOption("Walk here");
            FindControl<DropDown>(DD_OPTIONS).AddOption(new LABEL("Examine", null), new LABEL("Chaos dwarf hand cannoneer", Color.Yellow), new LABEL("(Level: 100)", Color.LimeGreen));
            FindControl<DropDown>(DD_OPTIONS).AddOption("Cancel");
            FindControl<DropDown>(DD_OPTIONS).Refresh();

            AddTextBox(
                Name: "TxTName",
                AutoSize: true,
                Position: new Vector2(10, 250));

            AddTextBox(
                Name: "TxtIp",
                AutoSize: true,
                Position: new Vector2(10, 350));

            base.Initialize();
        }

        public void LoadFont(ContentManager content, string name)
        {
            font = content.Load<SpriteFont>(name);
        }
    }
}