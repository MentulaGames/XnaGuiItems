#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Containers
{
#if MONO
    using Mono::Microsoft.Xna.Framework;
    using Mono::Microsoft.Xna.Framework.Graphics;
#else
    using Xna::Microsoft.Xna.Framework;
    using Xna::Microsoft.Xna.Framework.Graphics;
#endif
    using Core;
    using Core.EventHandlers;
    using Items;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DeJong.Utilities.Core;
    using static Utilities;
    using static DeJong.Utilities.Core.EventInvoker;

    /// <summary>
    /// A <see cref="GuiItem"/> container with tabs.
    /// </summary>
    /// <remarks>
    /// This object is usefull for interfaces like inventory and skills.
    /// It will handle the updating and drawing of all its child controlls.
    /// </remarks>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class TabContainer : GuiItem
    {
        /// <summary>
        /// Gets or sets a value indicating if the <see cref="TabContainer"/> should handle <see cref="TextBox"/> focusing.
        /// </summary>
        public virtual bool AutoFocus { get; set; }
        /// <summary>
        /// Gets the default tab size of the <see cref="TabContainer"/>
        /// </summary>
        new public static Rect DefaultBounds { get { return new Rect(0, 0, 250, 150); } }
        /// <summary>
        /// Gets or sets a selected tab.
        /// </summary>
        public virtual int SelectedTab { get { return selectedTab; } set { Invoke(TabSwitch, this, new ValueChangedEventArgs<int>(selectedTab, value)); } }
        /// <summary>
        /// Gets or sets a value indicating if the <see cref="TabContainer"/> should not atler the position of added tab items to the position of the <see cref="TabContainer"/>.
        /// </summary>
        public virtual bool UseAbsolutePosition { get; set; }

        /// <summary>
        /// Occurs when the value of the <see cref="SelectedTab"/> property changes.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_VALUE)]
        public event StrongEventHandler<TabContainer, ValueChangedEventArgs<int>> TabSwitch;

        private const string TAB_PREFIX = "TabHLbl_";
        private KeyValuePair<Label, GuiItemCollection>[] tabs;
        private SpriteFont font;
        private int selectedTab;
        private bool isAbsPosCall;
        private Vector2 oldPos;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabContainer"/> class with default settings.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public TabContainer(ref SpriteBatch sb, SpriteFont font)
            : base(ref sb)
        {
            this.font = font;
            tabs = new KeyValuePair<Label, GuiItemCollection>[0];
            Bounds = DefaultBounds;
            BackColor = Color.White;
        }

        /// <summary>
        /// Adds a tab with a specified name to the <see cref="TabContainer"/>.
        /// </summary>
        /// <param name="args"> The name and background color of the tab. </param>
        public void AddTab(Pair args)
        {
            int index = tabs.Length;
            Array.Resize(ref tabs, index + 1);
            tabs[index] = new KeyValuePair<Label, GuiItemCollection>(CreateHeaderLabel(args), new GuiItemCollection(this));
        }

        /// <summary>
        /// Adds one or more <see cref="GuiItem"/> to a specified tab.
        /// </summary>
        /// <param name="tab"> The tab name. </param>
        /// <param name="items"> The items to add to the tabs. </param>
        public void AddControll(string tab, params GuiItem[] items)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                if (tabs[i].Key.Name != $"{TAB_PREFIX}{tab}") continue;
                AddControll(i, items);
                break;
            }
        }

        /// <summary>
        /// Adds one or more <see cref="GuiItem"/> to a specified tab.
        /// </summary>
        /// <param name="tab"> The tab index. </param>
        /// <param name="items"> The items to add to the tabs. </param>
        public void AddControll(int tab, params GuiItem[] items)
        {
            GuiItemCollection controlls = tabs[tab].Value;
            for (int i = 0; i < items.Length; i++)
            {
                TextBox txt = items[i] as TextBox;
                if (txt != null) txt.Clicked += OnTabTextboxClick;
                items[i].Moved += OnTabControllMove;
                controlls.Add(items[i]);
                OnTabControllMove(items[i], ValueChangedEventArgs<Vector2>.Empty);
            }
        }

        /// <summary>
        /// Updates the <see cref="TabContainer"/>.
        /// </summary>
        /// <param name="deltaTime"> The deltaTime. </param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Enabled)
            {
                for (int i = 0; i < tabs.Length; i++) tabs[i].Key.Update(deltaTime);
            }
        }

        /// <summary>
        /// Draws the <see cref="TabContainer"/> and its childs.
        /// </summary>
        /// <param name="spriteBatch"> The <see cref="SpriteBatch"/> to use. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                Label hdr = tabs[SelectedTab].Key;

                spriteBatch.Draw(textures.DrawTexture.Texture, new Vector2(Position.X, Position.Y + hdr.Height), textures.DrawTexture[0], textures.userset_background ? BackColor : hdr.BackColor, Rotation, Origin, Vector2.One, SpriteEffects.None, 1f);
                for (int i = 0; i < tabs.Length; i++) tabs[i].Key.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Refreshes the <see cref="TabContainer"/>, recalculating the <see cref="GuiItem.Width"/> and the underlying texture.
        /// </summary>
        public override void Refresh()
        {
            if (!suppressRefresh)
            {
                CheckTabCount();
                OnTabHeaderClick(tabs[0].Key, MouseEventArgs.Empty);

                for (int i = 0; i < tabs.Length; i++)
                {
                    tabs[i].Key.Refresh();
                }

                int minWidth = GetHeaderWidth(tabs.Length);
                if (Width < minWidth) Width = GetHeaderWidth(tabs.Length);
            }

            base.Refresh();
        }

        /// <summary>
        /// Sets the background texture for the <see cref="TabContainer"/>.
        /// </summary>
        protected override void SetBackgroundTexture()
        {
            textures.SetBackFromClr(BackColor, new Size(Width, Height - tabs[0].Key.Height), batch.GraphicsDevice);
        }

        /// <summary>
        /// This method is called when the <see cref="GuiItem.Moved"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new position of the <see cref="GuiItem"/>. </param>
        protected override void OnMove(GuiItem sender, ValueChangedEventArgs<Vector2> e)
        {
            oldPos = Position;
            base.OnMove(sender, e);

            for (int i = 0; i < tabs.Length; i++)
            {
                tabs[i].Key.Position = Position + new Vector2(GetHeaderWidth(i), 0);

                GuiItemCollection controlls = tabs[i].Value;
                for (int j = 0; j < controlls.Count; j++) OnTabControllMove(controlls[j], ValueChangedEventArgs<Vector2>.Empty);
            }
        }

        /// <summary>
        /// Handles the initializing of the events.
        /// </summary>
        protected override void InitEvents()
        {
            base.InitEvents();
            TabSwitch += OnTabSwitch;
        }

        /// <summary>
        /// This method is called when the <see cref="TabSwitch"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new selected tab. </param>
        protected void OnTabSwitch(GuiItem sender, ValueChangedEventArgs<int> e)
        {
            selectedTab = e.NewValue;
        }

        private int GetHeaderWidth(int stopIndex)
        {
            int result = 0;
            for (int i = 0; i < stopIndex; i++) result += tabs[i].Key.Width;
            return result;
        }

        private Label CreateHeaderLabel(Pair args)
        {
            Label hdr = new Label(ref batch, font) { Name = $"{TAB_PREFIX}{args.Text}" };
            hdr.AutoSize = true;
            hdr.Text = $" {args.Text} ";
            hdr.Clicked += OnTabHeaderClick;
            hdr.Position = Position + new Vector2(GetHeaderWidth(tabs.Length - 1), 0);
            if (args.Color.HasValue) hdr.BackColor = args.Color.Value;
            hdr.HandleAutoSize();
            return hdr;
        }

        private void OnTabHeaderClick(GuiItem sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                KeyValuePair<Label, GuiItemCollection> tab = tabs[i];
                if (tab.Key.Name == sender.Name)
                {
                    SelectedTab = i;
                    tab.Key.BorderStyle = BorderStyle.FixedSingle;

                    for (int j = 0; j < tab.Value.Count; j++) tab.Value[j].Show();
                }
                else
                {
                    tab.Key.BorderStyle = BorderStyle.None;
                    for (int j = 0; j < tab.Value.Count; j++) tab.Value[j].Hide();
                }

                tab.Key.Refresh();
            }
        }

        private void OnTabControllMove(GuiItem sender, ValueChangedEventArgs<Vector2> e)
        {
            if (!(UseAbsolutePosition || isAbsPosCall))
            {
                isAbsPosCall = true;
                sender.Position += -oldPos + Position;
                if (oldPos == Vector2.Zero) sender.Position += new Vector2(0, tabs[0].Key.Height);
                isAbsPosCall = false;
            }
        }

        private void OnTabTextboxClick(GuiItem sender, MouseEventArgs e)
        {
            if (AutoFocus)
            {
                GuiItemCollection controlls = tabs[SelectedTab].Value;
                for (int i = 0; i < controlls.Count; i++)
                {
                    TextBox txt = controlls[i] as TextBox;
                    if (txt != null) txt.Focused = txt.Name == sender.Name;
                }
            }
        }

        private void CheckTabCount()
        {
            if (tabs.Length < 1) throw new ApplicationException($"{this} must have at least 1 tab!");
        }
    }
}