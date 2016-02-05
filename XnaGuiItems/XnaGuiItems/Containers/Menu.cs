using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Mentula.GuiItems.Core;
using GuiItemCollection = Mentula.GuiItems.Core.GuiItem.GuiItemCollection;

namespace Mentula.GuiItems.Containers
{
    internal class Menu : GameComponent, IDrawable
    {
        public int DrawOrder { get; set; }
        public bool Visible { get; set; }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        private GraphicsDevice device;
        private GuiItemCollection controlls;

        public Menu(Game game)
            : base(game)
        {
            device = game.GraphicsDevice;
            controlls = new GuiItemCollection(null);
        }

        public override void Initialize()
        {
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
        }
    }
}
