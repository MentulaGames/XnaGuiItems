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
    using Mono::Microsoft.Xna.Framework.Input;
#else
    using Xna::Microsoft.Xna.Framework;
    using Xna::Microsoft.Xna.Framework.Graphics;
    using Xna::Microsoft.Xna.Framework.Input;
#endif
    using Core;
    using Core.EventHandlers;
    using Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Utilities;
    using Core.Structs;

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
        /// Gets or sets a <see cref="Rectangle"/> indicating the size of the tab.
        /// </summary>
        public virtual Rect TabRectangle { get; set; }
        /// <summary>
        /// Gets or sets a selected tab.
        /// </summary>
        public virtual int SelectedTab { get; set; }
        /// <summary>
        /// Gets or sets a value indicating if the <see cref="TabContainer"/> should handle <see cref="TextBox"/> focusing.
        /// </summary>
        public virtual bool AutoFocus { get; set; }
        /// <summary>
        /// Gets or sets a value indicating if the <see cref="TabContainer"/> should not atler the position of added tab items to the position of the <see cref="TabContainer"/>.
        /// </summary>
        public virtual bool UseAbsolutePosition { get; set; }
        /// <summary>
        /// Gets the default tab size of the <see cref="TabContainer"/>
        /// </summary>
        public static Rect DefaultTabSize { get { return new Rect(0, 0, 250, 150); } }

        private const string TAB_PREFIX = "Lbl";
        private KeyValuePair<Label, GuiItemCollection>[] tabs;
        private SpriteFont font;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabContainer"/> class with default settings.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public TabContainer(ref SpriteBatch sb, SpriteFont font)
            : base(ref sb)
        {
#if DEBUG
            ctorCall = true;
#endif

            this.font = font;
            tabs = new KeyValuePair<Label, GuiItemCollection>[0];
            TabRectangle = DefaultTabSize;

#if DEBUG
            ctorCall = false;
#endif
        }

        /// <summary>
        /// Adds a tab with a specified name to the <see cref="TabContainer"/>.
        /// </summary>
        /// <param name="name"> The name for the tab. </param>
        /// <param name="color"> The background color for the tab. </param>
        public void AddTab(string name, Color? color = null)
        {
            int index = tabs.Length;
            Array.Resize(ref tabs, index + 1);

            Label lbl = new Label(ref batch, font) { Name = $"{TAB_PREFIX}{name}" };
            lbl.AutoSize = true;
            lbl.Text = $" {name} ";
            lbl.BorderStyle = BorderStyle.FixedSingle;
            lbl.Click += TabContainer_TabSelect;
            if (color.HasValue) lbl.BackColor = color.Value;

            tabs[index] = new KeyValuePair<Label, GuiItemCollection>(lbl, new GuiItemCollection(this));
        }

        /// <summary>
        /// Adds one or more <see cref="GuiItem"/> to a specified tab.
        /// </summary>
        /// <param name="tab"> The tab name. </param>
        /// <param name="items"> The items to add to the tabs. </param>
        public void AddToTab(string tab, params GuiItem[] items)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                KeyValuePair<Label, GuiItemCollection> cur = tabs[i];
                if (cur.Key.Name == $"{TAB_PREFIX}{tab}")
                {
                    for (int j = 0; j < items.Length; j++)
                    {
                        TextBox txt;
                        if ((txt = items[j] as TextBox) != null) txt.Click += TabContainer_TextBox_Click;
                        items[j].Move += TabContainer_GuiItem_Move;

                        items[j].Refresh();
                        cur.Value.Add(items[j]);
                        TabContainer_GuiItem_Move(items[j], ValueChangedEventArgs<Vector2>.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the <see cref="TabContainer"/>.
        /// </summary>
        /// <param name="delta"> The deltaTime. </param>
        public override void Update(float delta)
        {
            base.Update(delta);

            if (Enabled)
            {
                MouseState mState = Mouse.GetState();
                KeyboardState kState = Keyboard.GetState();

                for (int i = 0; i < tabs.Length; i++)
                {
                    tabs[i].Key.Update(delta);
                    GuiItemCollection controlls = tabs[i].Value;
                    if (i != SelectedTab) continue;

                    for (int j = 0; j < controlls.Count; j++)
                    {
                        GuiItem control = controlls[j];
                        control.Update(delta);

                        control.SuppressUpdate = true;
                    }
                }
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
                for (int i = 0; i < tabs.Length; i++)
                {
                    KeyValuePair<Label, GuiItemCollection> tab = tabs[i];
                    if (i == SelectedTab)
                    {
                        spriteBatch.Draw(textures.Background, Position + new Vector2(0, tab.Key.Height), null, tab.Key.BackColor, Rotation, Origin, Vector2.One, SpriteEffects.None, 0f);

                        for (int j = 0; j < tab.Value.Count; j++)
                        {
                            tab.Value[j].Draw(spriteBatch);
                        }
                    }

                    tab.Key.Draw(spriteBatch);

                    for (int j = 0; j < tab.Value.Count; j++)
                    {
                        tab.Value[j].SuppressDraw = true;
                    }
                }
            }
        }

        /// <summary>
        /// Refreshes the <see cref="TabContainer"/>, recalculating the <see cref="TabRectangle"/> and the underlying texture.
        /// </summary>
        public override void Refresh()
        {
            if (!suppressRefresh)
            {
                for (int i = 0; i < tabs.Length; i++)
                {
                    tabs[i].Key.Refresh();
                    tabs[i].Key.Position = Position + new Vector2(GetHeaderWidth(i), 0);
                }

                int length = GetHeaderWidth();
                if (length != TabRectangle.Width) TabRectangle = new Rect(TabRectangle.X, TabRectangle.Y, length, TabRectangle.Height);
            }

            base.Refresh();
        }

        /// <summary>
        /// This method is called when the <see cref="GuiItem.Move"/> event is raised.
        /// </summary>
        /// <param name="sender"> The <see cref="GuiItem"/> that raised the event. </param>
        /// <param name="e"> The new position of the <see cref="GuiItem"/>. </param>
        protected override void OnMove(GuiItem sender, ValueChangedEventArgs<Vector2> e)
        {
            base.OnMove(sender, e);
            for (int i = 0; i < tabs.Length; i++)
            {
                KeyValuePair<Label, GuiItemCollection> tab = tabs[i];
                tab.Key.Position = Position + new Vector2(GetHeaderWidth(i), 0);

                for (int j = 0; j < tab.Value.Count; j++)
                {
                    TabContainer_GuiItem_Move(tab.Value[j], ValueChangedEventArgs<Vector2>.Empty);
                }
            }
        }

        /// <summary>
        /// Sets the background texture for the <see cref="TabContainer"/>.
        /// </summary>
        protected override void SetBackgroundTexture()
        {
            textures.SetBackFromClr(BackColor, TabRectangle.Size, batch.GraphicsDevice);
        }

        private int GetHeaderWidth()
        {
            return GetHeaderWidth(tabs.Length);
        }

        private int GetHeaderWidth(int stopIndex)
        {
            int result = 0;
            for (int i = 0; i < stopIndex; i++) result += tabs[i].Key.Width;
            return result;
        }

        private void TabContainer_TabSelect(GuiItem sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                KeyValuePair<Label, GuiItemCollection> tab = tabs[i];

                if (tab.Key.Name == sender.Name)
                {
                    SelectedTab = i;
                    tab.Key.BorderStyle = BorderStyle.FixedSingle;
                }
                else tab.Key.BorderStyle = BorderStyle.None;
            }
        }

        private void TabContainer_TextBox_Click(GuiItem sender, MouseEventArgs e)
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

        private void TabContainer_GuiItem_Move(GuiItem sender, ValueChangedEventArgs<Vector2> e)
        {
            if (!UseAbsolutePosition)
            {
                Label tab = tabs.FirstOrDefault(t => t.Value.Contains(sender)).Key;
                sender.Bounds = new Rect((int)sender.Position.X, (int)sender.Position.Y + tab.Height, sender.Width, sender.Height);
            }
        }
    }
}