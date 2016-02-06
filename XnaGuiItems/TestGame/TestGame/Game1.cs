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
using Microsoft.Xna.Framework.Storage;
using Mentula.GuiItems.Items;
using Mentula.GuiItems.Core;
using LABEL = System.Collections.Generic.KeyValuePair<string, Microsoft.Xna.Framework.Color>;
using System.Diagnostics;
using Mentula.Client;

namespace TestGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont font;

        private DropDown dd;
        private FPS fps;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            fps = new FPS();
        }

        protected override void Initialize()
        {
            base.Initialize();

            dd = new DropDown(GraphicsDevice, new Rectangle(10, 10, 100, 50), font) { AutoSize = true };
            dd.AddOption(new LABEL("Attack", dd.ForeColor), new LABEL("Chaos dwarf hand cannoneer", Color.Yellow), new LABEL("(Level: 100)", Color.LimeGreen));
            dd.AddOption("Walk here");
            dd.AddOption(new LABEL("Examine", dd.ForeColor), new LABEL("Chaos dwarf hand cannoneer", Color.Yellow), new LABEL("(Level: 100)", Color.LimeGreen));
            dd.AddOption("Cancel");
            dd.Refresh();
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("ConsoleFont");
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();

            dd.Update(ms);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            fps.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            spriteBatch.Begin();
            dd.Draw(spriteBatch);
            spriteBatch.DrawString(font, fps.Avarage.ToString(), Vector2.Zero, Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
