using Mentula.GuiItems.Core;
using Mentula.GuiItems.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using LABEL = System.Collections.Generic.KeyValuePair<string, Microsoft.Xna.Framework.Color>;

namespace TestGame
{
    public class Game1 : Game
    {
        public MainMenu mm;

        private GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Components.Add(mm = new MainMenu(this));
            mm.LoadFont(Content, "ConsoleFont");
            base.Initialize();
        }
    }
}
