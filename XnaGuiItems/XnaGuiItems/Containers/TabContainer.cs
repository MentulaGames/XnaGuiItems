using Mentula.GuiItems.Core;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mentula.GuiItems.Containers
{
    /// <summary>
    /// A <see cref="GuiItem"/> container with tabs.
    /// </summary>
    public class TabContainer : GuiItem
    {
        /// <summary>
        /// Gets or sets a <see cref="Rectangle"/> indicating the size of the tab.
        /// </summary>
        public virtual Rectangle TabRectangle { get { return tabRect; } set { tabRect = value; Refresh(); } }
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
        public static Rectangle DefaultTabSize { get { return new Rectangle(0, 0, 250, 150); } }

        protected Rectangle tabRect;

        private const string TAB_PREFIX = "Lbl";
        private KeyValuePair<Label, GuiItemCollection>[] tabs;
        private Texture2D tabtexture;
        private SpriteFont font;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabContainer"/> class with default settings.
        /// </summary>
        /// <param name="device"> The <see cref="GraphicsDevice"/> to display the <see cref="TabContainer"/> to. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public TabContainer(GraphicsDevice device, SpriteFont font)
            : base(device)
        {
            this.font = font;
            tabs = new KeyValuePair<Label, GuiItemCollection>[0];
            TabRectangle = DefaultTabSize;
            tabtexture = Drawing.FromColor(Color.White, TabRectangle.Width, TabRectangle.Height, device);
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

            int prevL = 0;
            for (int i = 0; i < index; i++)
            {
                prevL += tabs[i].Key.Width;
            }

            Label lbl = new Label(device, font)
            {
                AutoSize = true,
                Text = $" {name} ",
                Name = $"{TAB_PREFIX}{name}",
                Position = Position + new Vector2(prevL, 0),
                Height = 25,
                BorderStyle = BorderStyle.FixedSingle
            };

            if (color != null) lbl.BackColor = color.Value;

            lbl.Click += (sender, args) =>
            {
                for (int i = 0; i < tabs.Length; i++)
                {
                    KeyValuePair<Label, GuiItemCollection> tab = tabs[i];
                    tab.Key.BorderStyle = BorderStyle.None;

                    if (tab.Key.Name == sender.Name)
                    {
                        SelectedTab = i;
                        tab.Key.BorderStyle = BorderStyle.FixedSingle;
                    }

                    tab.Key.Refresh();
                }
            };

            tabs[index] = new KeyValuePair<Label, GuiItemCollection>(lbl, new GuiItemCollection(this));
            Refresh();
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
                        if ((txt = items[j] as TextBox) != null) txt.Click += TextBox_Click;
                        items[j].Move += GuiItem_Move;

                        items[j].Refresh();
                        cur.Value.Add(items[j]);
                        GuiItem_Move(items[j], items[j].Position);
                    }
                }
            }
        }

        /// <summary>
        /// Refreshes the <see cref="TabContainer"/>, recalculating the <see cref="TabRectangle"/> and the underlying texture.
        /// </summary>
        public override void Refresh()
        {
            int length = 0;
            for (int i = 0; i < tabs.Length; i++)
            {
                length += tabs[i].Key.Width;
            }

            if (length > tabRect.Width) tabRect = new Rectangle(tabRect.X, tabRect.Y, length, tabRect.Height);
            tabtexture = Drawing.FromColor(Color.White, TabRectangle.Width, TabRectangle.Height, device);
        }

        /// <summary>
        /// This method cannot be used withing a <see cref="TabContainer"/>.
        /// </summary>
        /// <param name="mState"></param>
        [Obsolete("Use Update(MouseState, KeyboardState, float) instead", true)]
        new public void Update(MouseState mState) { }

        /// <summary>
        /// Updates the <see cref="TabContainer"/>.
        /// </summary>
        /// <param name="mState"> The specified <see cref="MouseState"/> to use. </param>
        /// <param name="kState"> The specified <see cref="KeyboardState"/> to use. </param>
        /// <param name="delta"> The deltaTime. </param>
        public void Update(MouseState mState, KeyboardState kState, float delta)
        {
            base.Update(mState);

            if (Enabled)
            {
                for (int i = 0; i < tabs.Length; i++)
                {
                    tabs[i].Key.Update(mState);
                    GuiItemCollection controlls = tabs[i].Value;

                    for (int j = 0; j < controlls.Count; j++)
                    {
                        GuiItem control = controlls[j];
                        if (i == SelectedTab) control.Update_S(mState, kState, delta);

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
                        spriteBatch.Draw(tabtexture, Position + new Vector2(0, tab.Key.Height), null, tab.Key.BackColor, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

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

        private void TextBox_Click(GuiItem sender, MouseState state)
        {
            if (AutoFocus)
            {
                for (int i = 0; i < tabs.Length; i++)
                {
                    GuiItemCollection controlls = tabs[i].Value;
                    for (int j = 0; j < controlls.Count; j++)
                    {
                        TextBox txt;
                        if ((txt = controlls[j] as TextBox) != null)
                        {
                            txt.Focused = txt.Name == sender.Name;
                            txt.Refresh();
                        }
                    }
                }
            }
        }

        private void GuiItem_Move(GuiItem sender, Vector2 newPos)
        {
            if (!UseAbsolutePosition)
            {
                Label tab = tabs.FirstOrDefault(t => t.Value.Contains(sender)).Key;
                sender.Bounds = new Rectangle((int)sender.Position.X, (int)sender.Position.Y + tab.Height, sender.Width, sender.Height);
            }
        }
    }
}