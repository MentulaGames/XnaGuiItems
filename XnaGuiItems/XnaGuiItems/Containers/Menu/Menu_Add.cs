using Mentula.GuiItems.Core;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Mentula.GuiItems.Containers
{
    public partial class Menu<T> : MentulaGameComponent<T>, IUpdateable, IDrawable
        where T : Game
    {
        private static readonly InvalidOperationException noFont = 
            new InvalidOperationException("Menu.font must be set before calling this method or a font must be specified!");

        /// <summary>
        /// Adds a standard <see cref="GuiItem"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <returns> The <see cref="GuiItem"/> created. </returns>
        public GuiItem AddGuiItem()
        {
            GuiItem gI = new GuiItem(device);
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
            if (font == null && Font == null) throw noFont;
            Button btn = new Button(device, Font ?? font);
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
            if (font == null && Font == null) throw noFont;
            DropDown dd = new DropDown(device, Font ?? font);
            controlls.Add(dd);
            dd.VisibilityChanged += DropDown_VisibilityChanged; ;
            return dd;
        }

        /// <summary>
        /// Adds a <see cref="Label"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <param name="Font"> A specified font for the control (Optional). </param>
        /// <returns> The <see cref="Label"/> created. </returns>
        public Label AddLabel(SpriteFont Font = null)
        {
            if (font == null && Font == null) throw noFont;
            Label lbl = new Label(device, Font ?? font);
            controlls.Add(lbl);
            return lbl;
        }

        /// <summary>
        /// Adds a <see cref="ProgressBar"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <returns> The <see cref="ProgressBar"/> created. </returns>
        public ProgressBar AddProgresBar()
        {
            ProgressBar prgBr = new ProgressBar(device);
            controlls.Add(prgBr);
            return prgBr;
        }

        /// <summary>
        /// Adds a <see cref="Slider"/> to the <see cref="Menu{T}"/>.
        /// </summary>
        /// <returns> The <see cref="Slider"/> created. </returns>
        public Slider AddSlider()
        {
            Slider sld = new Slider(device);
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
            if (font == null && Font == null) throw noFont;
            TextBox txt = new TextBox(device, Font ?? font);
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
            if (font == null && Font == null) throw noFont;
            TabContainer tbC = new TabContainer(device, Font ?? font);
            controlls.Add(tbC);
            return tbC;
        }
    }
}