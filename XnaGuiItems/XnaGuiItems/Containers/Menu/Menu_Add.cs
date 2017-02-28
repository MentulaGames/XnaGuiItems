﻿namespace Mentula.GuiItems.Containers
{
    using Core;
    using Items;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public partial class Menu<T> : DrawableMentulaGameComponent<T>
        where T : Game
    {
        /// <summary>
        /// Adds a standard <see cref="GuiItem"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <returns> The <see cref="GuiItem"/> created. </returns>
        public GuiItem AddGuiItem()
        {
            GuiItem gI = new GuiItem(ref batch);
            controlls.Add(gI);
            return gI;
        }

        /// <summary>
        /// Adds a <see cref="Button"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <param name="Font"> A specified font for the control (Optional). </param>
        /// <returns> The <see cref="Button"/> created. </returns>
        public Button AddButton(SpriteFont Font = null)
        {
            if (font == null && Font == null) ThrowNoFont();
            Button btn = new Button(ref batch, Font ?? font);
            controlls.Add(btn);
            return btn;
        }

        /// <summary>
        /// Adds a <see cref="DropDown"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <param name="Font"> A specified font for the control (Optional). </param>
        /// <returns> The <see cref="DropDown"/> created. </returns>
        public DropDown AddDropDown(SpriteFont Font = null)
        {
            if (font == null && Font == null) ThrowNoFont();
            DropDown dd = new DropDown(ref batch, Font ?? font);
            controlls.Add(dd);
            dd.VisibilityChanged += DropDown_VisibilityChanged;
            return dd;
        }

        /// <summary>
        /// Adds a <see cref="Label"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <param name="Font"> A specified font for the control (Optional). </param>
        /// <returns> The <see cref="Label"/> created. </returns>
        public Label AddLabel(SpriteFont Font = null)
        {
            if (font == null && Font == null) ThrowNoFont();
            Label lbl = new Label(ref batch, Font ?? font);
            controlls.Add(lbl);
            return lbl;
        }

        /// <summary>
        /// Adds a <see cref="ProgressBar"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <returns> The <see cref="ProgressBar"/> created. </returns>
        public ProgressBar AddProgresBar()
        {
            ProgressBar prgBr = new ProgressBar(ref batch);
            controlls.Add(prgBr);
            return prgBr;
        }

        /// <summary>
        /// Adds a <see cref="Slider"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <returns> The <see cref="Slider"/> created. </returns>
        public Slider AddSlider()
        {
            Slider sld = new Slider(ref batch);
            controlls.Add(sld);
            return sld;
        }

        /// <summary>
        /// Adds a <see cref="TextBox"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <param name="Font"> A specified font for the control (Optional). </param>
        /// <returns> The <see cref="TextBox"/> created. </returns>
        public TextBox AddTextBox(SpriteFont Font = null)
        {
            if (font == null && Font == null) ThrowNoFont();
            TextBox txt = new TextBox(ref batch, Font ?? font);
            txt.Click += TextBox_Click;
            controlls.Add(txt);
            return txt;
        }

        /// <summary>
        /// Adds a <see cref="TabContainer"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <param name="Font"> A specified font for the control (Optional). </param>
        /// <returns> The <see cref="TabContainer"/> created. </returns>
        public TabContainer AddTabContainer(SpriteFont Font = null)
        {
            if (font == null && Font == null) ThrowNoFont();
            TabContainer tbC = new TabContainer(ref batch, Font ?? font);
            controlls.Add(tbC);
            return tbC;
        }

        /// <summary>
        /// Adds a <see cref="PictureBox"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <returns> The <see cref="PictureBox"/> created. </returns>
        public PictureBox AddPictureBox()
        {
            PictureBox picBox = new PictureBox(ref batch);
            controlls.Add(picBox);
            return picBox;
        }

        private void ThrowNoFont()
        {
            throw new InvalidOperationException("Menu.font must be set before calling this method or a font must be specified!");
        }
    }
}