using Mentula.GuiItems.Core;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using static Mentula.GuiItems.Core.GuiItem;

namespace Mentula.GuiItems.Containers
{
    /// <summary>
    /// A class for grouping GuiItems.
    /// </summary>
    public class Menu : GameComponent, IUpdateable, IDrawable
    {
        public int DrawOrder { get; set; }
        public bool Visible { get; set; }

        /// <summary>
        /// The center width of the viewport.
        /// </summary>
        public int ScreenWidthMiddle { get { return device.Viewport.Width >> 1; } }
        /// <summary>
        /// The center height of the viewport.
        /// </summary>
        public int ScreenHeightMiddle { get { return device.Viewport.Height >> 1; } }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        protected GuiItemCollection controlls;
        protected SpriteFont font;

        private GraphicsDevice device;
        private SpriteBatch batch;

        /// <summary>
        /// Initializes a new instance of the Mentula.GuiItems.Containers.Menu class.
        /// </summary>
        /// <param name="game"></param>
        public Menu(Game game)
            : base(game)
        {
            device = game.GraphicsDevice;
            controlls = new GuiItemCollection(null);
        }

        /// <summary>
        /// Initializes the spritebatch used for drawing.
        /// </summary>
        public override void Initialize()
        {
            batch = new SpriteBatch(device);
            base.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                controlls.ForEach(i => i.Dispose());
            }

            base.Dispose(disposing);
        }

        public GuiItem AddGuiItem(
            bool? AllowDrop = null,
            Color? BackColor = null,
            Texture2D BackgroundImage = null,
            Rectangle? Bounds = null,
            bool? Enabled = null,
            Color? ForeColor = null,
            int? Height = null,
            string Name = null,
            Vector2? Position = null,
            GuiItem Parent = null,
            bool? Visible = null,
            int? Width = null)
        {
            GuiItem gI = new GuiItem(device);

            if (AllowDrop != null) gI.AllowDrop = AllowDrop.Value;
            if (BackColor != null) gI.BackColor = BackColor.Value;
            if (BackgroundImage != null) gI.BackgroundImage = BackgroundImage;
            if (Bounds != null) gI.Bounds = Bounds.Value;
            if (Enabled != null) gI.Enabled = Enabled.Value;
            if (ForeColor != null) gI.ForeColor = ForeColor.Value;
            if (Height != null) gI.Height = Height.Value;
            if (Name != null) gI.Name = Name;
            if (Position != null) gI.Position = Position.Value;
            if (Parent != null) gI.Parent = Parent;
            if (Visible != null) gI.Visible = Visible.Value;
            if (Width != null) gI.Width = Width.Value;

            controlls.Add(gI);
            return gI;
        }

        public Button AddButton(
            bool? AllowDrop = null,
            Color? BackColor = null,
            Texture2D BackgroundImage = null,
            Rectangle? Bounds = null,
            bool? Enabled = null,
            Color? ForeColor = null,
            int? Height = null,
            string Name = null,
            Vector2? Position = null,
            GuiItem Parent = null,
            bool? Visible = null,
            int? Width = null,
            bool? AutoSize = null,
            BorderStyle? BorderStyle = null,
            string Text = null,
            SpriteFont Font = null,
            Rectangle? ForegroundRectangle = null)
        {
            Button btn = new Button(device, font);

            if (AllowDrop != null) btn.AllowDrop = AllowDrop.Value;
            if (BackColor != null) btn.BackColor = BackColor.Value;
            if (BackgroundImage != null) btn.BackgroundImage = BackgroundImage;
            if (Bounds != null) btn.Bounds = Bounds.Value;
            if (Enabled != null) btn.Enabled = Enabled.Value;
            if (ForeColor != null) btn.ForeColor = ForeColor.Value;
            if (Height != null) btn.Height = Height.Value;
            if (Name != null) btn.Name = Name;
            if (Position != null) btn.Position = Position.Value;
            if (Parent != null) btn.Parent = Parent;
            if (Visible != null) btn.Visible = Visible.Value;
            if (Width != null) btn.Width = Width.Value;
            if (AutoSize != null) btn.AutoSize = AutoSize.Value;
            if (BorderStyle != null) btn.BorderStyle = BorderStyle.Value;
            if (Text != null) btn.Text = Text;
            if (Font != null) btn.Font = font;
            if (ForegroundRectangle != null) btn.ForegroundRectangle = ForegroundRectangle.Value;

            controlls.Add(btn);
            return btn;
        }

        public DropDown AddDropDown(
            bool? AllowDrop = null,
            Color? BackColor = null,
            Texture2D BackgroundImage = null,
            Rectangle? Bounds = null,
            bool? Enabled = null,
            Color? ForeColor = null,
            int? Height = null,
            string Name = null,
            Vector2? Position = null,
            GuiItem Parent = null,
            bool? Visible = null,
            int? Width = null,
            bool? AutoSize = null,
            BorderStyle? BorderStyle = null,
            string HeaderText = null,
            Color? HeaderBackgroundColor = null)
        {
            DropDown dd = new DropDown(device, font);

            if (AllowDrop != null) dd.AllowDrop = AllowDrop.Value;
            if (BackColor != null) dd.BackColor = BackColor.Value;
            if (BackgroundImage != null) dd.BackgroundImage = BackgroundImage;
            if (Bounds != null) dd.Bounds = Bounds.Value;
            if (Enabled != null) dd.Enabled = Enabled.Value;
            if (ForeColor != null) dd.ForeColor = ForeColor.Value;
            if (Height != null) dd.Height = Height.Value;
            if (Name != null) dd.Name = Name;
            if (Position != null) dd.Position = Position.Value;
            if (Parent != null) dd.Parent = Parent;
            if (Visible != null) dd.Visible = Visible.Value;
            if (Width != null) dd.Width = Width.Value;
            if (AutoSize != null) dd.AutoSize = AutoSize.Value;
            if (BorderStyle != null) dd.BorderStyle = BorderStyle.Value;
            if (HeaderText != null) dd.HeaderText = HeaderText;
            if (HeaderBackgroundColor != null) dd.HeaderBackgroundColor = HeaderBackgroundColor.Value;

            controlls.Add(dd);
            return dd;
        }

        public Label AddLabel(
            bool? AllowDrop = null,
            Color? BackColor = null,
            Texture2D BackgroundImage = null,
            Rectangle? Bounds = null,
            bool? Enabled = null,
            Color? ForeColor = null,
            int? Height = null,
            string Name = null,
            Vector2? Position = null,
            GuiItem Parent = null,
            bool? Visible = null,
            int? Width = null,
            bool? AutoSize = null,
            BorderStyle? BorderStyle = null,
            string Text = null,
            SpriteFont Font = null,
            Rectangle? ForegroundRectangle = null)
        {
            Label lbl = new Label(device, font);

            if (AllowDrop != null) lbl.AllowDrop = AllowDrop.Value;
            if (BackColor != null) lbl.BackColor = BackColor.Value;
            if (BackgroundImage != null) lbl.BackgroundImage = BackgroundImage;
            if (Bounds != null) lbl.Bounds = Bounds.Value;
            if (Enabled != null) lbl.Enabled = Enabled.Value;
            if (ForeColor != null) lbl.ForeColor = ForeColor.Value;
            if (Height != null) lbl.Height = Height.Value;
            if (Name != null) lbl.Name = Name;
            if (Position != null) lbl.Position = Position.Value;
            if (Parent != null) lbl.Parent = Parent;
            if (Visible != null) lbl.Visible = Visible.Value;
            if (Width != null) lbl.Width = Width.Value;
            if (AutoSize != null) lbl.AutoSize = AutoSize.Value;
            if (BorderStyle != null) lbl.BorderStyle = BorderStyle.Value;
            if (Text != null) lbl.Text = Text;
            if (Font != null) lbl.Font = font;
            if (ForegroundRectangle != null) lbl.ForegroundRectangle = ForegroundRectangle.Value;

            controlls.Add(lbl);
            return lbl;
        }

        public ProgresBar AddprogresBar(
            bool? AllowDrop = null,
            Color? BackColor = null,
            Texture2D BackgroundImage = null,
            Rectangle? Bounds = null,
            bool? Enabled = null,
            Color? ForeColor = null,
            int? Height = null,
            string Name = null,
            Vector2? Position = null,
            GuiItem Parent = null,
            bool? Visible = null,
            int? Width = null,
            bool? Inverted = null,
            BorderStyle? BorderStyle = null,
            int? Value = null)
        {
            ProgresBar prgBr = new ProgresBar(device);

            if (AllowDrop != null) prgBr.AllowDrop = AllowDrop.Value;
            if (BackColor != null) prgBr.BackColor = BackColor.Value;
            if (BackgroundImage != null) prgBr.BackgroundImage = BackgroundImage;
            if (Bounds != null) prgBr.Bounds = Bounds.Value;
            if (Enabled != null) prgBr.Enabled = Enabled.Value;
            if (ForeColor != null) prgBr.ForeColor = ForeColor.Value;
            if (Height != null) prgBr.Height = Height.Value;
            if (Name != null) prgBr.Name = Name;
            if (Position != null) prgBr.Position = Position.Value;
            if (Parent != null) prgBr.Parent = Parent;
            if (Visible != null) prgBr.Visible = Visible.Value;
            if (Width != null) prgBr.Width = Width.Value;
            if (Inverted != null) prgBr.Inverted = Inverted.Value;
            if (BorderStyle != null) prgBr.BorderStyle = BorderStyle.Value;
            if (Value != null) prgBr.Value = Value.Value;

            controlls.Add(prgBr);
            return prgBr;
        }

        public Slider AddSlider(
            bool? AllowDrop = null,
            Color? BackColor = null,
            Texture2D BackgroundImage = null,
            Rectangle? Bounds = null,
            bool? Enabled = null,
            Color? ForeColor = null,
            int? Height = null,
            string Name = null,
            Vector2? Position = null,
            GuiItem Parent = null,
            bool? Visible = null,
            int? Width = null,
            BorderStyle? BorderStyle = null,
            Rectangle? SliderBarDimentions = null,
            int? MaximumValue = null,
            int? Minimumvalue = null,
            int? Value = null)
        {
            Slider sld = new Slider(device);

            if (AllowDrop != null) sld.AllowDrop = AllowDrop.Value;
            if (BackColor != null) sld.BackColor = BackColor.Value;
            if (BackgroundImage != null) sld.BackgroundImage = BackgroundImage;
            if (Bounds != null) sld.Bounds = Bounds.Value;
            if (Enabled != null) sld.Enabled = Enabled.Value;
            if (ForeColor != null) sld.ForeColor = ForeColor.Value;
            if (Height != null) sld.Height = Height.Value;
            if (Name != null) sld.Name = Name;
            if (Position != null) sld.Position = Position.Value;
            if (Parent != null) sld.Parent = Parent;
            if (Visible != null) sld.Visible = Visible.Value;
            if (Width != null) sld.Width = Width.Value;
            if (BorderStyle != null) sld.BorderStyle = BorderStyle.Value;
            if (SliderBarDimentions != null) sld.SlidBarDimentions = SliderBarDimentions.Value;
            if (MaximumValue != null) sld.MaximumValue = MaximumValue.Value;
            if (Minimumvalue != null) sld.MinimumValue = Minimumvalue.Value;
            if (Value != null) sld.Value = Value.Value;

            controlls.Add(sld);
            return sld;
        }

        public TextBox AddTextBox(
            bool? AllowDrop = null,
            Color? BackColor = null,
            Texture2D BackgroundImage = null,
            Rectangle? Bounds = null,
            bool? Enabled = null,
            Color? ForeColor = null,
            int? Height = null,
            string Name = null,
            Vector2? Position = null,
            GuiItem Parent = null,
            bool? Visible = null,
            int? Width = null,
            bool? AutoSize = null,
            BorderStyle? BorderStyle = null,
            string Text = null,
            SpriteFont Font = null,
            Rectangle? ForegroundRectangle = null,
            FlickerStyle? FlickerStyle = null,
            bool? Focused = null,
            bool? Multiline = null,
            char? PasswordChar = null,
            Vector2? MinimumSize = null,
            Vector2? MaximumSize = null)
        {
            TextBox txt = new TextBox(device, font);

            if (AllowDrop != null) txt.AllowDrop = AllowDrop.Value;
            if (BackColor != null) txt.BackColor = BackColor.Value;
            if (BackgroundImage != null) txt.BackgroundImage = BackgroundImage;
            if (Bounds != null) txt.Bounds = Bounds.Value;
            if (Enabled != null) txt.Enabled = Enabled.Value;
            if (ForeColor != null) txt.ForeColor = ForeColor.Value;
            if (Height != null) txt.Height = Height.Value;
            if (Name != null) txt.Name = Name;
            if (Position != null) txt.Position = Position.Value;
            if (Parent != null) txt.Parent = Parent;
            if (Visible != null) txt.Visible = Visible.Value;
            if (Width != null) txt.Width = Width.Value;
            if (AutoSize != null) txt.AutoSize = AutoSize.Value;
            if (BorderStyle != null) txt.BorderStyle = BorderStyle.Value;
            if (Text != null) txt.Text = Text;
            if (Font != null) txt.Font = font;
            if (ForegroundRectangle != null) txt.ForegroundRectangle = ForegroundRectangle.Value;
            if (FlickerStyle != null) txt.FlickerStyle = FlickerStyle.Value;
            if (Focused != null) txt.Focused = Focused.Value;
            if (Multiline != null) txt.MultiLine = Multiline.Value;
            if (PasswordChar != null) txt.PasswordChar = PasswordChar.Value;
            if (MinimumSize != null) txt.MinimumSize = MinimumSize.Value;
            if (MaximumSize != null) txt.MaximumSize = MaximumSize.Value;

            txt.Click += TextBox_Click;
            controlls.Add(txt);
            return txt;
        }

        public TabContainer AddTabContainer(
            bool? AllowDrop = null,
            bool? Enabled = null,
            string Name = null,
            Vector2? Position = null,
            GuiItem Parent = null,
            bool? Visible = null,
            Rectangle? TabRectangle = null,
            int? SelectedTab = null)
        {
            TabContainer tbC = new TabContainer(device, font);

            if (AllowDrop != null) tbC.AllowDrop = AllowDrop.Value;
            if (Enabled != null) tbC.Enabled = Enabled.Value;
            if (Name != null) tbC.Name = Name;
            if (Position != null) tbC.Position = Position.Value;
            if (Parent != null) tbC.Parent = Parent;
            if (Visible != null) tbC.Visible = Visible.Value;
            if (TabRectangle != null) tbC.TabRectangle = TabRectangle.Value;
            if (SelectedTab != null) tbC.SelectedTab = SelectedTab.Value;

            controlls.Add(tbC);
            return tbC;
        }

        /// <summary>
        /// Gets a control with a specified name as a specified GuiItem.
        /// </summary>
        /// <typeparam name="T"> The GuiItem to cast to. </typeparam>
        /// <param name="name"> The specified name to search for. </param>
        public T FindControl<T>(string name)
            where T : GuiItem
        {
            return (T)FindControl(name);
        }

        /// <summary>
        /// Gets a control with a specified name.
        /// </summary>
        /// <param name="name"> The specified name to search for. </param>
        public GuiItem FindControl(string name)
        {
            return controlls.FirstOrDefault(c => c.Name == name);
        }

        /// <summary>
        /// Disables and hides the menu.
        /// </summary>
        public void Hide()
        {
            Enabled = false;
            Visible = false;
        }

        /// <summary>
        /// Enables and shows the menu.
        /// </summary>
        public void Show()
        {
            Enabled = true;
            Visible = true;
        }

        /// <summary>
        /// Updates the menu and its controlls.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            MouseState mState = Mouse.GetState();
            KeyboardState kState = Keyboard.GetState();
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < controlls.Count; i++)
            {
                GuiItem control = controlls[i];
                if (control.SuppressUpdate)
                {
                    control.SuppressUpdate = false;
                    continue;
                }

                Button btn;
                TextBox txt;
                TabContainer tb;

                if ((btn = control as Button) != null) btn.Update(mState, delta);
                else if ((txt = control as TextBox) != null) txt.Update(mState, kState, delta);
                else if ((tb = control as TabContainer) != null) tb.Update(mState, kState, delta);
                else control.Update(mState);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the menu and its controlls.
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            batch.Begin();

            for (int i = 0; i < controlls.Count; i++)
            {
                if (controlls[i].SuppressDraw)
                {
                    controlls[i].SuppressDraw = false;
                    continue;
                }
                controlls[i].Draw(batch);
            }

            batch.End();
        }

        private void TextBox_Click(GuiItem sender, MouseState state)
        {
            for (int i = 0; i < controlls.Count; i++)
            {
                TextBox txt;
                if ((txt = controlls[i] as TextBox) != null)
                {
                    txt.Focused = txt.Name == sender.Name ? true : false;
                    txt.Refresh();
                }
            }
        }
    }
}
