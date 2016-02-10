using Mentula.GuiItems.Core;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Mentula.GuiItems.Containers
{
    /// <summary>
    /// A GuiItem container with tabs.
    /// </summary>
    public class TabContainer : GuiItem
    {
        /// <summary>
        /// Gets or sets a rectangle indicating the size of the tab.
        /// </summary>
        public virtual Rectangle TabRectangle { get { return tabRect; } set { tabRect = value; Refresh(); } }
        /// <summary>
        /// Gets or sets a selected tab.
        /// </summary>
        public virtual int SelectedTab { get; set; }

        protected Rectangle tabRect;

        private KeyValuePair<Label, GuiItemCollection>[] tabs;
        private Texture2D tabtexture;
        private SpriteFont font;

        /// <summary>
        /// Initializes a new instance of the XnaMentula.GuiItems.Containers.TabContainer class with default settings.
        /// </summary>
        /// <param name="device"> The device to display the XnaMentula.GuiItems.Containers.TabContainer to. </param>
        /// <param name="font"> The font to use while drawing the text. </param>
        public TabContainer(GraphicsDevice device, SpriteFont font)
            : base(device)
        {
            this.font = font;
            Init();
        }

        /// <summary>
        /// Adds a tab with a specified name to the TabContainer.
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
                Name = "Lbl" + name,
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
        /// Adds one or more GuiItems to a specified tab.
        /// </summary>
        /// <param name="tab"> The tab name. </param>
        /// <param name="items"> The items to add to the tabs. </param>
        public void AddToTab(string tab, params GuiItem[] items)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                KeyValuePair<Label, GuiItemCollection> cur = tabs[i];
                if (cur.Key.Name == "Lbl" + tab)
                {
                    for (int j = 0; j < items.Length; j++)
                    {
                        cur.Value.Add(items[j]);
                    }
                }
            }
        }

        /// <summary>
        /// Refreshes the TabContainer recalculating the TabRectangle and the underlying texture.
        /// </summary>
        public void Refresh()
        {
            int length = 0;
            for (int i = 0; i < tabs.Length; i++)
            {
                length += tabs[i].Key.Width;
            }

            if (length > tabRect.Width) tabRect = new Rectangle(tabRect.X, tabRect.Y, length, tabRect.Height);
            tabtexture = Drawing.FromColor(Color.White, TabRectangle.Width, TabRectangle.Height, device);
        }

        [Obsolete("Use Update(MouseState, KeyboardState, float) instead", true)]
        new public void Update(MouseState mState) { }

        /// <summary>
        /// Updates the TabContainer.
        /// </summary>
        /// <param name="mState"> The specified MouseState to use. </param>
        /// <param name="kState"> The specified KeyboardState to use. </param>
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

                        Button btn;
                        TextBox txt;

                        if (i == SelectedTab)
                        {
                            if ((btn = control as Button) != null) btn.Update(mState, delta);
                            else if ((txt = control as TextBox) != null) txt.Update(mState, kState, delta);
                            else control.Update(mState);
                        }

                        control.SuppressUpdate = true;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the TabContainer and its childs.
        /// </summary>
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
                        tab.Value.ForEach(g => g.Draw(spriteBatch));
                    }

                    tab.Key.Draw(spriteBatch);
                    tab.Value.ForEach(g => g.SuppressDraw = true);
                }
            }
        }

        private void Init()
        {
            tabs = new KeyValuePair<Label, GuiItemCollection>[0];
            TabRectangle = new Rectangle(0, 0, 250, 150);
            tabtexture = Drawing.FromColor(Color.White, TabRectangle.Width, TabRectangle.Height, device);
        }
    }
}